// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Worker.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MyService.App.SdkStyle;

/// <summary>
/// バックグラウンドサービスの実装を提供するクラスを表します。
/// </summary>
public class Worker : BackgroundService
{
    /// <summary><see cref="ILogger{TCategory} "/> を表します。</summary>
    private readonly ILogger<Worker> _logger;

    /// <summary>
    /// <see cref="Worker"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="logger"><see cref="ILogger{TCategory} "/> インスタンス。</param>
    public Worker(ILogger<Worker> logger)
    {
        this._logger = logger;
    }

    /// <summary>
    /// サービスを開始します。
    /// </summary>
    /// <param name="cancellationToken">操作がキャンセルされたかどうかを確認するために使用されるキャンセレーショントークン。</param>
    /// <returns>非同期操作を表すタスク。</returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("サービスが開始されました: {time}", DateTimeOffset.Now);
        await base.StartAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// サービスを停止します。
    /// </summary>
    /// <param name="cancellationToken">操作がキャンセルされたかどうかを確認するために使用されるキャンセレーショントークン。</param>
    /// <returns>非同期操作を表すタスク。</returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("サービスが停止されます: {time}", DateTimeOffset.Now);
        await base.StopAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// バックグラウンドサービスの主要な処理を実行します。
    /// </summary>
    /// <param name="stoppingToken">操作がキャンセルされたかどうかを確認するために使用されるキャンセレーショントークン。</param>
    /// <returns>非同期操作を表すタスク。</returns>
    /// <exception cref="OperationCanceledException">操作がキャンセルされた場合にスローされます。</exception>
    /// <exception cref="Exception">サービス実行中に予期しないエラーが発生した場合にスローされます。</exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("サービス実行中: {time}", DateTimeOffset.Now);
                
                await this.DoWorkAsync(stoppingToken).ConfigureAwait(false);

                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("サービスがキャンセルされました");
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "サービス実行中にエラーが発生しました");

            // 重要: サービスを停止させる
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// 実際の業務ロジックを実行します。
    /// </summary>
    /// <param name="cancellationToken">操作がキャンセルされたかどうかを確認するために使用されるキャンセレーショントークン。</param>
    /// <returns>非同期操作を表すタスク。</returns>
    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        // ここに実際の業務ロジックを実装
        this._logger.LogDebug("業務処理を実行中...");

        // 例: ファイル処理、データベース操作、API呼び出しなど
        await Task.CompletedTask;
    }
}
