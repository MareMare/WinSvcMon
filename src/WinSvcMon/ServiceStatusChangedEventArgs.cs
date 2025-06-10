// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatusChangedEventArgs.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ServiceProcess;

namespace WinSvcMon;

/// <summary>
/// サービスのステータス変更イベントのデータを提供するクラスを表します。
/// </summary>
public class ServiceStatusChangedEventArgs : EventArgs
{
    /// <summary>
    /// <see cref="ServiceStatusChangedEventArgs" /> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="serviceName">ステータスが変更された Windows サービスの名前。</param>
    /// <param name="status">サービスの新しいステータス。</param>
    internal ServiceStatusChangedEventArgs(string serviceName, ServiceControllerStatus? status)
    {
        this.ServiceName = serviceName;
        this.Status = status;
    }

    /// <summary>
    /// ステータスが変更された Windows サービスの名前を取得します。
    /// </summary>
    /// <value>サービス名を表す文字列。</value>
    public string ServiceName { get; }

    /// <summary>
    /// サービスの現在のステータスを取得します。
    /// </summary>
    /// <value>サービスのステータスを表す <see cref="ServiceControllerStatus" /> 値。</value>
    public ServiceControllerStatus? Status { get; }
}
