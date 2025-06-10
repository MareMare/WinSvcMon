# WinSvcMon

Windows サービスの監視と操作を行うアプリケーションです。

## プロジェクト概要

WinSvcMon（Windows Service Monitor）は、Windows サービスの状態を監視し、操作するためのグラフィカルユーザーインターフェイスを提供するアプリケーションです。このアプリケーションを使用することで、複数の Windows サービスの状態をリアルタイムで監視し、必要に応じてサービスの開始や停止などの操作を行うことができます。

### 主な機能

- 複数の Windows サービスを同時に監視
- サービスの状態をリアルタイムで表示
- サービスの開始・停止操作
- カスタマイズ可能な監視間隔
- サービス表示名のカスタマイズ

### 対象ユーザー

- システム管理者
- IT プロフェッショナル
- Windows サーバー管理者
- 開発者（開発・テスト環境でのサービス監視用）

## 前提条件

### 動作環境

- .NET Framework 4.8.1
- Windows 10 以降または Windows Server 2016 以降
- 管理者権限（サービスの操作に必要）

### 必要なソフトウェア

- Visual Studio 2022 以降（開発・ビルド時）

## インストール方法

### ソースコードからのビルド

1. リポジトリをクローンまたはダウンロードします。
   ```
   git clone https://github.com/MareMare/WinSvcMon.git
   ```

2. Visual Studio で `WinSvcMon.sln` ソリューションファイルを開きます。

3. ソリューションをビルドします。
   - メニューから「ビルド」→「ソリューションのビルド」を選択
   - または F6 キーを押す

4. ビルドが成功すると、`src\WinSvcMon.App\bin\Debug` または `src\WinSvcMon.App\bin\Release` フォルダに実行ファイルが生成されます。

## 実行方法

1. `WinSvcMon.App.exe` をダブルクリックして起動します。
   - 管理者権限が必要な場合は、右クリックして「管理者として実行」を選択してください。

2. アプリケーションが起動すると、設定ファイルで指定されたサービスの監視が自動的に開始されます。

3. サービスの状態が表示され、各サービスに対して開始・停止などの操作が可能になります。

## MonitoringConfiguration.json の設定

アプリケーションの動作は `MonitoringConfiguration.json` ファイルで設定します。このファイルはアプリケーションの実行ファイルと同じディレクトリに配置する必要があります。

### ファイル構造

```json
{
  "Services": [
    {
      "ServiceName": "Service01",
      "DisplayName": "サービス1"
    },
    {
      "ServiceName": "Service02",
      "DisplayName": "サービス2"
    },
    {
      "ServiceName": "Service03",
      "DisplayName": "サービス3"
    }
  ],
  "MonitoringIntervalSeconds": 5
}
```

### 設定項目の説明

#### Services（必須）

監視対象のサービスリストを指定します。各サービスには以下の項目を設定します：

- **ServiceName**（必須）: Windows サービスの実際のサービス名を指定します。これは Windows のサービス管理コンソールで表示される「サービス名」と一致する必要があります。
- **DisplayName**（必須）: アプリケーション上で表示するサービスの名前を指定します。

#### MonitoringIntervalSeconds（オプション）

サービスの状態を確認する間隔を秒単位で指定します。デフォルト値は 5 秒です。

### 設定例

#### 基本的な設定

```json
{
  "Services": [
    {
      "ServiceName": "wuauserv",
      "DisplayName": "Windows Update"
    },
    {
      "ServiceName": "LanmanServer",
      "DisplayName": "Server"
    }
  ],
  "MonitoringIntervalSeconds": 10
}
```

この設定では、Windows Update サービスと Server サービスを 10 秒間隔で監視します。

#### 多数のサービスを監視する設定

```json
{
  "Services": [
    {
      "ServiceName": "wuauserv",
      "DisplayName": "Windows Update"
    },
    {
      "ServiceName": "LanmanServer",
      "DisplayName": "Server"
    },
    {
      "ServiceName": "BITS",
      "DisplayName": "Background Intelligent Transfer Service"
    },
    {
      "ServiceName": "WSearch",
      "DisplayName": "Windows Search"
    },
    {
      "ServiceName": "W32Time",
      "DisplayName": "Windows Time"
    }
  ],
  "MonitoringIntervalSeconds": 15
}
```

この設定では、5つのサービスを15秒間隔で監視します。

#### 短い監視間隔の設定

```json
{
  "Services": [
    {
      "ServiceName": "SQLServerAgent",
      "DisplayName": "SQL Server エージェント"
    },
    {
      "ServiceName": "MSSQLSERVER",
      "DisplayName": "SQL Server データベース"
    }
  ],
  "MonitoringIntervalSeconds": 2
}
```

この設定では、SQL Server 関連の2つのサービスを2秒間隔で監視します。短い間隔で監視することで、状態変化をより早く検出できますが、システムリソースの消費が増加する可能性があります。

## トラブルシューティング

### サービスが表示されない場合

- サービス名が正しいか確認してください。
- 管理者権限でアプリケーションを実行しているか確認してください。
- 監視対象のサービスがシステムに存在するか確認してください。

### アプリケーションが起動しない場合

- .NET Framework 4.8.1 がインストールされているか確認してください。
- `MonitoringConfiguration.json` ファイルの構文が正しいか確認してください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は [LICENSE](LICENSE) ファイルを参照してください。