// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceMonitor.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinSvcMon;

/// <summary>
/// Windows サービスの監視と制御を行うインターフェイスを表します。
/// </summary>
public interface IServiceMonitor
{
    /// <summary>
    /// サービスのステータスが変更されたときに発生するイベントを表します。
    /// </summary>
    event EventHandler<ServiceStatusChangedEventArgs>? ServiceStatusChanged;

    /// <summary>
    /// Windows サービスの名前のコレクションを取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="string" /> 型。
    /// <para>Windows サービスの名前のコレクション。既定値は要素数 0 です。</para>
    /// </value>
    ICollection<string> ServiceNames { get; }

    /// <summary>
    /// 指定された Windows サービス名が存在するかどうかを判定します。
    /// </summary>
    /// <param name="serviceName">判定する Windows サービスの名前。</param>
    /// <returns>サービスが存在する場合は <see langword="true" />、それ以外の場合は <see langword="false" />。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serviceName" /> が <see langword="null" /> または空白の場合にスローされます。</exception>
    bool IsServiceNameValid(string serviceName);

    /// <summary>
    /// 非同期操作として、指定された間隔でサービスの監視を開始します。
    /// </summary>
    /// <param name="monitoringTimeSpan">監視間隔。指定されない場合は 5 秒間隔で監視します。</param>
    /// <param name="cancellationToken">監視を中止するためのキャンセレーショントークン。</param>
    /// <returns>監視タスクを表す <see cref="Task" />。</returns>
    Task MonitorServicesAsync(
        TimeSpan? monitoringTimeSpan = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 非同期操作として、指定された Windows サービスを開始します。
    /// </summary>
    /// <param name="serviceName">開始する Windows サービスの名前。</param>
    /// <param name="cancellationToken">監視を中止するためのキャンセレーショントークン。</param>
    /// <returns>サービス開始操作を表す <see cref="Task" />。</returns>
    Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default);

    /// <summary>
    /// 非同期操作として、指定された Windows サービスを停止します。
    /// </summary>
    /// <param name="serviceName">停止する Windows サービスの名前。</param>
    /// <param name="cancellationToken">監視を中止するためのキャンセレーショントークン。</param>
    /// <returns>サービス停止操作を表す <see cref="Task" />。</returns>
    Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default);
}
