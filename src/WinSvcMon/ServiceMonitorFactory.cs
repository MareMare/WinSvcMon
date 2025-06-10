// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceMonitorFactory.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinSvcMon;

/// <summary>
/// <see cref="IServiceMonitor" /> インターフェイスのインスタンスを生成するためのファクトリクラスを表します。
/// </summary>
public static class ServiceMonitorFactory
{
    /// <summary>
    /// 指定された Windows サービス名のコレクションを監視する <see cref="IServiceMonitor" /> インスタンスを生成します。
    /// </summary>
    /// <param name="serviceNames">監視対象の Windows サービス名のコレクション。</param>
    /// <returns>生成された <see cref="IServiceMonitor" /> インスタンス。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serviceNames" /> が <see langword="null" /> の場合にスローされます。</exception>
    public static IServiceMonitor Create(IEnumerable<string> serviceNames) =>
        new ServiceMonitor(serviceNames);
}
