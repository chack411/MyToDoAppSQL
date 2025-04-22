# Azure App Service & SQL Database ハンズオン

このリポジトリは、Azure App Service と SQL Database を使用した ASP.NET Core Web アプリケーションのハンズオン用に作成されたものです。

ハンズオンドキュメントは [こちら](docs/README.md) をご覧ください。

- **アプリケーション名**: MyToDoAppSQL
- **アプリケーション概要**: タスク管理アプリケーション
- **使用技術**: ASP.NET Core 9.0, Entity Framework Core 9.0, SQL Server
- **データベース**: SQL Server
- **デプロイ先**: Azure App Service
- **データベース接続**: Azure SQL Database
- **CI/CD**: GitHub Actions
- **作成年月**: 2025年4月 

# MyToDoAppSQL ソフトウェア内部仕様書

## 1. システム概要
MyToDoAppSQL は ASP.NET Core 9.0 と Microsoft SQL Server（Entity Framework Core）を用いたシンプルな ToDo 管理 Web アプリケーションです。タスク（ToDo）の登録・編集・削除・詳細表示が可能です。

## 2. アーキテクチャ構成
- **フレームワーク**: ASP.NET Core 9.0
- **ORM**: Entity Framework Core 9.0
- **DB**: SQL Server
- **UI**: Razor Pages, Bootstrap, jQuery

### ディレクトリ構成（抜粋）
- Controllers/ : コントローラー（ItemsController, HomeController）
- Data/        : DBコンテキスト（MyToDoAppSQLContext）
- Models/      : ドメインモデル（Item, ErrorViewModel）
- Migrations/  : マイグレーション履歴
- Views/       : 各種Razorビュー
- wwwroot/     : 静的ファイル
- Program.cs   : エントリポイント

## 3. 主な機能仕様
### 3.1 タスク（Item）管理
- 一覧表示（Index）
- 詳細表示（Details）
- 新規作成（Create）
- 編集（Edit）
- 削除（Delete）

### 3.2 画面仕様
- /Items/Index : タスク一覧
- /Items/Create : タスク新規作成
- /Items/Edit/{id} : タスク編集
- /Items/Details/{id} : タスク詳細
- /Items/Delete/{id} : タスク削除確認

## 4. データモデル
### Item（タスク）
| プロパティ   | 型        | 必須 | 説明         |
|--------------|-----------|------|--------------|
| Id           | int       | ○    | 主キー       |
| ToDo         | string    | ○    | タスク内容   |
| DueDate      | DateTime  | ○    | 締切日       |
| IsComplete   | bool      | ○    | 完了フラグ   |

### ErrorViewModel
| プロパティ   | 型        | 説明         |
|--------------|-----------|--------------|
| RequestId    | string?   | リクエストID |
| ShowRequestId| bool      | 表示判定     |

## 5. コントローラー仕様
### ItemsController
- CRUD全機能を提供。
- DBアクセスは MyToDoAppSQLContext 経由。
- POSTアクションは [ValidateAntiForgeryToken] でCSRF対策。
- ModelState検証あり。
- 編集・削除時はID存在チェック。

### HomeController
- トップページ、プライバシーポリシー、エラー画面を提供。

## 6. DB構成
- テーブル: Item
  - カラム: Id, ToDo, DueDate, IsComplete
- マイグレーションで自動生成

## 7. 設定ファイル
- appsettings.json: DB接続文字列、ロギング設定
- launchSettings.json: 起動URL, 環境変数

## 8. セキュリティ
- CSRF対策: [ValidateAntiForgeryToken] 属性
- 入力検証: DataAnnotations, ModelState

## 9. 使用ライブラリ
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Bootstrap, jQuery, jQuery Validation

## 10. 使用方法
1. **データベースの設定**
   - `MyToDoAppSQL/appsettings.json` の `ConnectionStrings:MyToDoAppSQLContext` を自身のSQL Server環境に合わせて編集してください。
2. **マイグレーションの適用**
   - コマンドプロンプトでプロジェクトルートに移動し、以下を実行します。
     ```bash
     dotnet ef database update
     ```
3. **アプリケーションの起動**
   - 以下のコマンドでアプリを起動します。
     ```bash
     dotnet run
     ```
   - ブラウザで `http://localhost:5016` などにアクセスしてください。

## 11. ライセンス
- MIT License

---

本仕様書はソースコードおよび構成ファイルから GitHub Copilot, GPT-4.1 で自動生成されています。