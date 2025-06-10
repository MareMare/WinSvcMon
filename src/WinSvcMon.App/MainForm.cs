// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;

namespace WinSvcMon.App;

/// <summary>
/// Windows サービスの監視と制御を行うメインフォームを表します。
/// </summary>
public partial class MainForm : Form
{
    /// <summary>サービス監視を行うインスタンスを表します。</summary>
    private readonly IServiceMonitor _serviceMonitor;

    /// <summary>監視設定のインスタンスを表します。</summary>
    private readonly MonitoringConfiguration _config;

    /// <summary>サービス情報のリストを表します。</summary>
    private readonly BindingList<ServiceInfo> _serviceList;

    /// <summary>UI スレッドとの連携を管理するタスクコンテキストを表します。</summary>
    private readonly JoinableTaskContext _joinableTaskContext;

    /// <summary>監視タスクのキャンセルを制御するトークンソースを表します。</summary>
    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// <see cref="MainForm" /> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public MainForm()
    {
        this.InitializeComponent();
        this._joinableTaskContext = new JoinableTaskContext();
        this._serviceList = [];

        // DataGridView の設定
        this.servicesGridView.AutoGenerateColumns = false;
        this.ColumnOfDisplayName.DataPropertyName = nameof(ServiceInfo.DisplayName);
        this.ColumnOfServiceName.DataPropertyName = nameof(ServiceInfo.ServiceName);
        this.ColumnOfServiceState.DataPropertyName = nameof(ServiceInfo.StatusText);
        this.servicesGridView.DataSource = this._serviceList;

        // 定義ファイルの読み込み
        this._config = MonitoringConfiguration.Load();

        // サービスモニターの初期化
        var serviceNames = this._config.Services.Select(pair => pair.ServiceName).ToArray();
        this._serviceMonitor = ServiceMonitorFactory.Create(serviceNames);
        this._serviceMonitor.ServiceStatusChanged += this.OnServiceStatusChanged;
    }

    /// <summary>
    /// フォームが閉じられる際に発生するイベントを処理します。
    /// </summary>
    /// <param name="sender">イベントのソースを表すオブジェクト。</param>
    /// <param name="e">イベントデータを格納した <see cref="FormClosingEventArgs" />。</param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => this._cancellationTokenSource?.Cancel();

    /// <summary>
    /// フォームがロードされた際に発生するイベントを処理します。
    /// </summary>
    /// <param name="sender">イベントのソースを表すオブジェクト。</param>
    /// <param name="e">イベントデータを格納した <see cref="EventArgs" />。</param>
    // ReSharper disable once AsyncVoidMethod
#pragma warning disable VSTHRD100
    private async void MainForm_Load(object sender, EventArgs e)
#pragma warning restore VSTHRD100
    {
        // サービスリストを初期化
        foreach (var pair in this._config.Services)
        {
            this._serviceList.Add(new ServiceInfo(pair.ServiceName, pair.DisplayName, null));
        }

        // サービス監視の開始
        this._cancellationTokenSource = new CancellationTokenSource();
        await Task.Run(() =>
            this._serviceMonitor.MonitorServicesAsync(
                monitoringTimeSpan: TimeSpan.FromSeconds(this._config.MonitoringIntervalSeconds),
                cancellationToken: this._cancellationTokenSource.Token));
    }

    /// <summary>
    /// サービスのステータスが変更された際に発生するイベントを処理します。
    /// </summary>
    /// <param name="sender">イベントのソースを表すオブジェクト。</param>
    /// <param name="e">イベントデータを格納した <see cref="ServiceStatusChangedEventArgs" />。</param>
#pragma warning disable VSTHRD100
    // ReSharper disable once AsyncVoidMethod
    private async void OnServiceStatusChanged(object sender, ServiceStatusChangedEventArgs e)
#pragma warning restore VSTHRD100
    {
        // メインスレッドで UI を更新
        await this._joinableTaskContext.Factory.SwitchToMainThreadAsync();
        var serviceInfo = this._serviceList.FirstOrDefault(s => s.ServiceName == e.ServiceName);
        if (serviceInfo != null)
        {
            serviceInfo.Status = e.Status;
            this.servicesGridView.Refresh(); // DataGridView を更新
        }
    }

    /// <summary>
    /// サービス一覧のセルがクリックされた際に発生するイベントを処理します。
    /// </summary>
    /// <param name="sender">イベントのソースを表すオブジェクト。</param>
    /// <param name="e">イベントデータを格納した <see cref="DataGridViewCellEventArgs" />。</param>
#pragma warning disable VSTHRD100
    // ReSharper disable once AsyncVoidMethod
    private async void OnServicesGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
#pragma warning restore VSTHRD100
    {
        if (e.ColumnIndex != this.ColumnToChangeState.Index || e.RowIndex < 0)
        {
            return;
        }

        var serviceInfo = this._serviceList[e.RowIndex];
        if (serviceInfo.Status == ServiceControllerStatus.Running)
        {
            await this._serviceMonitor.StopServiceAsync(serviceInfo.ServiceName);
        }
        else
        {
            await this._serviceMonitor.StartServiceAsync(serviceInfo.ServiceName);
        }
    }

    /// <summary>
    /// サービス情報を格納するクラスを表します。
    /// </summary>
    private sealed class ServiceInfo : INotifyPropertyChanged
    {
        /// <summary>サービスの現在のステータスを表します。</summary>
        private ServiceControllerStatus? _status;

        /// <summary>
        /// <see cref="ServiceInfo" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceName">Windows サービスの名前。</param>
        /// <param name="displayName">表示名称。</param>
        /// <param name="status">サービスの初期ステータス。</param>
        public ServiceInfo(string serviceName, string displayName, ServiceControllerStatus? status)
        {
            this.ServiceName = serviceName;
            this.DisplayName = displayName;
            this._status = status;
        }

        /// <summary>
        /// プロパティ値が変更されたときに発生するイベントを表します。
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Windows サービスの名前を取得します。
        /// </summary>
        /// <value>サービス名を表す文字列。</value>
        public string ServiceName { get; }

        /// <summary>
        /// 表示名称を取得します。
        /// </summary>
        /// <value>表示名称を表す文字列。</value>
        public string DisplayName { get; }

        /// <summary>
        /// サービスの現在のステータスを取得または設定します。
        /// </summary>
        /// <value>サービスのステータスを表す <see cref="ServiceControllerStatus" /> 値。</value>
        public ServiceControllerStatus? Status
        {
            get => this._status;
            set
            {
                if (this._status != value)
                {
                    this._status = value;
                    this.OnPropertyChanged(nameof(ServiceInfo.Status));
                }
            }
        }

        /// <summary>
        /// サービスの現在のステータスを表示用のテキストとして取得します。
        /// </summary>
        /// <value>ステータスを表す文字列。</value>
        public string StatusText =>
            this.Status switch
            {
                ServiceControllerStatus.Running => "Running",
                ServiceControllerStatus.Stopped => "Stopped",
                ServiceControllerStatus.Paused => "Paused",
                ServiceControllerStatus.StartPending => "Starting...",
                ServiceControllerStatus.StopPending => "Stopping...",
                _ => "Unknown",
            };

        /// <summary>
        /// プロパティ変更イベントを発生させます。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名前。</param>
        private void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// サービス名と表示名のペアを表します。
    /// </summary>
    [UsedImplicitly]
    private sealed class ServiceNameDisplayPair
    {
        /// <summary>
        /// サービス名を取得または設定します。
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// サービスの表示名を取得または設定します。
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
    }

    /// <summary>
    /// 監視設定を格納するクラスを表します。
    /// </summary>
    private sealed class MonitoringConfiguration
    {
        /// <summary>
        /// 監視対象の Windows サービス名と表示名のペアのコレクションを取得または設定します。
        /// </summary>
        /// <value>サービス名と表示名のペアのコレクション。</value>
        public ServiceNameDisplayPair[] Services { get; set; } = [];

        /// <summary>
        /// 監視間隔を秒単位で取得または設定します。
        /// </summary>
        /// <value>監視間隔の秒数。既定値は 5 秒です。</value>
        public int MonitoringIntervalSeconds { get; set; } = 5;

        /// <summary>
        /// 設定ファイルから監視設定を読み込みます。
        /// </summary>
        /// <returns>読み込まれた <see cref="MonitoringConfiguration" /> インスタンス。</returns>
        /// <exception cref="FileNotFoundException">設定ファイルが見つからない場合にスローされます。</exception>
        /// <exception cref="InvalidOperationException">設定ファイルのデシリアライズに失敗した場合にスローされます。</exception>
        public static MonitoringConfiguration Load()
        {
            var filePath = $"{nameof(MonitoringConfiguration)}.json";
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {filePath}");
            }

            var json = File.ReadAllText(filePath);
            try
            {
                return JsonConvert.DeserializeObject<MonitoringConfiguration>(json)
                       ?? throw new InvalidOperationException("Failed to deserialize configuration.");
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize configuration.", ex);
            }
        }
    }
}
