// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceMonitor.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.ServiceProcess;

namespace WinSvcMon;

/// <summary>
/// Windows サービスの監視と制御を行うクラスを表します。
/// </summary>
internal sealed class ServiceMonitor : IServiceMonitor
{
    /// <summary>監視対象の Windows サービス名のリストを表します。</summary>
    private readonly List<string> _serviceNames;

    /// <summary>
    /// <see cref="ServiceMonitor" /> クラスの新しいインスタンスを生成します。
    /// </summary>
    /// <param name="serviceNames">Windows サービス名のコレクション。</param>
    public ServiceMonitor(IEnumerable<string> serviceNames)
    {
        this._serviceNames = new List<string>(serviceNames);
        this.ServiceNames = this._serviceNames.ToArray();
    }

    /// <inheritdoc />
    public event EventHandler<ServiceStatusChangedEventArgs>? ServiceStatusChanged;

    /// <inheritdoc />
    public ICollection<string> ServiceNames { get; }

    /// <inheritdoc />
    public bool IsServiceNameValid(string serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
        {
            throw new ArgumentNullException(nameof(serviceName), "サービス名が null または空白です。");
        }

        try
        {
            // サービス名が有効かどうかを確認
            using var serviceController = new ServiceController(serviceName);
            _ = serviceController.Status; // ステータスにアクセスして存在確認
            return true;
        }
        catch (InvalidOperationException)
        {
            // サービスが存在しない場合
            return false;
        }
        catch (Win32Exception)
        {
            // サービス名が不適切な場合
            return false;
        }
    }

    /// <inheritdoc />
    public async Task MonitorServicesAsync(
        TimeSpan? monitoringTimeSpan = null,
        CancellationToken cancellationToken = default)
    {
        var previousStatuses = new Dictionary<string, ServiceControllerStatus?>();
        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var serviceName in this._serviceNames)
            {
                var currentStatus = ServiceMonitor.GetServiceStatus(serviceName);

                // 前回の状態と比較して変化があった場合のみイベントを発行します。
                if (previousStatuses.TryGetValue(serviceName, out var previousStatus) &&
                    previousStatus == currentStatus)
                {
                    continue;
                }

                previousStatuses[serviceName] = currentStatus;
                this.OnServiceStatusChanged(new ServiceStatusChangedEventArgs(serviceName, currentStatus));
            }

            // 監視間隔を設定 (既定値: 5秒)
            var interval = monitoringTimeSpan ?? TimeSpan.FromSeconds(5);
            await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        if (!this.IsServiceNameValid(serviceName))
        {
            return;
        }

        cancellationToken.ThrowIfCancellationRequested();
        using var serviceController = new ServiceController(serviceName);
        if (serviceController.Status != ServiceControllerStatus.Running)
        {
            serviceController.Start();
            await Task.Run(
                    () =>
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        ServiceMonitor.WaitForStatus(serviceName, ServiceControllerStatus.Running);
                    },
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        if (!this.IsServiceNameValid(serviceName))
        {
            return;
        }

        cancellationToken.ThrowIfCancellationRequested();
        using var serviceController = new ServiceController(serviceName);
        if (serviceController.Status != ServiceControllerStatus.Stopped)
        {
            serviceController.Stop();
            await Task.Run(
                    () =>
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        ServiceMonitor.WaitForStatus(serviceName, ServiceControllerStatus.Stopped);
                    },
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    /// <summary>
    /// 指定された Windows サービスの現在のステータスを取得します。
    /// </summary>
    /// <param name="serviceName">ステータスを取得する Windows サービスの名前。</param>
    /// <returns>
    /// サービスの現在のステータスを表す <see cref="ServiceControllerStatus" />。
    /// サービス名が不適切または存在しない場合は <see langword="null" /> を返します。
    /// </returns>
    private static ServiceControllerStatus? GetServiceStatus(string serviceName)
    {
        try
        {
            using var serviceController = new ServiceController(serviceName);
            return serviceController.Status;
        }
        catch (InvalidOperationException)
        {
            // サービスが存在しない場合
            return null;
        }
        catch (Win32Exception)
        {
            // サービス名が不適切な場合
            return null;
        }
    }

    /// <summary>
    /// 指定された Windows サービスが指定された状態になるまで待機します。
    /// </summary>
    /// <param name="serviceName">待機対象の Windows サービスの名前。</param>
    /// <param name="status">待機するサービスの目標ステータスを表す <see cref="ServiceControllerStatus" />。</param>
    /// <remarks>
    /// このメソッドは、サービスが指定された状態に到達するまでブロッキングします。
    /// サービスの状態が変化するまでの間、呼び出し元のスレッドはブロックされます。
    /// </remarks>
    private static void WaitForStatus(string serviceName, ServiceControllerStatus status)
    {
        using var serviceController = new ServiceController(serviceName);
        serviceController.WaitForStatus(status);
    }

    /// <summary>
    /// サービスのステータス変更イベントを発生させます。
    /// </summary>
    /// <param name="e">イベントデータを格納した <see cref="ServiceStatusChangedEventArgs" />。</param>
    private void OnServiceStatusChanged(ServiceStatusChangedEventArgs e) =>
        this.ServiceStatusChanged?.Invoke(this, e);
}
