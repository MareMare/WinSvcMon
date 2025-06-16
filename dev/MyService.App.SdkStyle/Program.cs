// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="TemplateCompany">
// Copyright © 2025 TemplateCompany.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using MyService.App.SdkStyle;
using NLog.Config;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var appAssemblyName = Assembly.GetExecutingAssembly().GetName();
// アプリケーション名に基づいた NLog 設定ファイル名を指定し、実行ディレクトリを基準に NLog.config を読み込みます。
var nlogConfigFileName = $"{appAssemblyName.Name}.NLog.config";
var nlogConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nlogConfigFileName);
NLog.LogManager.Configuration = new XmlLoggingConfiguration(nlogConfigFilePath);

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration
    // 実行ディレクトリとアセンブリディレクトリが異なる場合、SetBasePath メソッドで既定のディレクトリを明示して appsettings.json が正しく読み込まれるようにします。
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddEnvironmentVariables();
builder.Services.AddWindowsService(options => options.ServiceName = appAssemblyName.Name);
builder.Services.AddHostedService<Worker>();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddEventLog();
    logging.AddNLog();
});

var logger = NLog.LogManager.GetCurrentClassLogger();
try
{
    logger.Info($"起動します。{appAssemblyName.Name}({builder.Environment.EnvironmentName}) v{appAssemblyName.Version}");
    var host = builder.Build();
    await host.RunAsync();
    logger.Info($"終了しました。{appAssemblyName.Name}({builder.Environment.EnvironmentName}) v{appAssemblyName.Version}");
}
catch (Exception ex)
{
    logger.Error(
        ex,
        $"例外が発生しました。{appAssemblyName.Name}({builder.Environment.EnvironmentName}) v{appAssemblyName.Version}");
    Environment.Exit(1);
}
finally
{
    NLog.LogManager.Shutdown();
}
