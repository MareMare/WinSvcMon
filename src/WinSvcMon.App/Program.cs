// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MareMare">
// Copyright © 2025 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSvcMon.App;

/// <summary>
/// アプリケーションのエントリポイントを提供するクラスを表します。
/// </summary>
internal static class Program
{
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    /// <param name="args">コマンドライン引数。</param>
    /// <returns>正常終了は0。それ以外は -1。</returns>
    [STAThread]
    private static int Main(string[] args)
    {
        // Mutex 名を決定します。
        var mutexName = $"Mutex_{AppDomain.CurrentDomain.FriendlyName}";
        using var mutex = new Mutex(true, mutexName, out var createdNew);
        if (!createdNew)
        {
            // Mutex の初期所有者でなかった場合は、多重起動として処理を中断します。(NLog は出力できない)
            return -1;
        }

        try
        {
            // Mutex の初期所有者の場合は、唯一の起動として処理を継続します。
            Program.Execute(args);
        }
        finally
        {
            // Mutex の初期所有者なので Mutex を解放します。
            mutex.ReleaseMutex();
        }

        return 0;
    }

    /// <summary>
    /// 唯一の起動としてアプリケーションを開始します。
    /// </summary>
    /// <param name="args">コマンドライン引数。</param>
    private static void Execute(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.ThreadException += ApplicationOnThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        try
        {
            using var mainForm = new MainForm();
            Application.Run(mainForm);
        }
        finally
        {
            Application.ThreadException -= ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException -= TaskSchedulerOnUnobservedTaskException;
        }

        return;

        void ApplicationOnThreadException(object? sender, ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;
            Console.WriteLine(@"致命的な例外を検出しました。{0}", exception);
        }

        void CurrentDomainOnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception ?? new InvalidProgramException("ハンドルされていない例外を検出しました。");
            Console.WriteLine(@"致命的な例外を検出しました。{0}", exception);
        }

        void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception;
            Console.WriteLine(@"致命的な例外を検出しました。{0}", exception);
            e.SetObserved();
        }
    }
}
