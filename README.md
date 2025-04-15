# MyToDoAppSQL

## 概要
MyToDoAppSQL は、ASP.NET Core を使用して構築されたシンプルな ToDo アプリケーションです。このアプリケーションは、タスクの作成、編集、削除、詳細表示を行うことができます。

## 主な機能
- タスクの一覧表示
- タスクの作成
- タスクの編集
- タスクの削除
- タスクの詳細表示

## 技術スタック
- **フレームワーク**: ASP.NET Core 9.0
- **データベース**: Microsoft SQL Server (Entity Framework Core を使用)
- **フロントエンド**: Bootstrap, jQuery

## ディレクトリ構成
```
MyToDoAppSQL.sln
MyToDoAppSQL/
  ├── Controllers/       # コントローラー
  ├── Data/              # データベースコンテキスト
  ├── Migrations/        # マイグレーションファイル
  ├── Models/            # モデルクラス
  ├── Properties/        # プロジェクト設定
  ├── Views/             # ビュー
  ├── wwwroot/           # 静的ファイル (CSS, JS, ライブラリ)
  ├── appsettings.json   # アプリケーション設定
  └── Program.cs         # アプリケーションエントリポイント
```

## データベース構造
### Item テーブル
| カラム名      | データ型      | 必須 | 説明             |
|---------------|---------------|------|------------------|
| Id            | int           | Yes  | 主キー           |
| ToDo          | string        | Yes  | タスクの内容     |
| DueDate       | DateTime      | Yes  | 締切日           |
| IsComplete    | bool          | Yes  | 完了フラグ       |

## 主なファイル
### Program.cs
アプリケーションのエントリポイントであり、サービスの登録やミドルウェアの設定を行います。

### Controllers/ItemsController.cs
タスクに関連する操作 (CRUD) を管理するコントローラー。

### Models/Item.cs
タスクを表すデータモデル。

### Views/Items/
- **Index.cshtml**: タスクの一覧表示。
- **Create.cshtml**: 新しいタスクの作成フォーム。
- **Edit.cshtml**: タスクの編集フォーム。
- **Details.cshtml**: タスクの詳細表示。
- **Delete.cshtml**: タスクの削除確認。

## 使用方法
1. **データベースの設定**:
   `appsettings.json` 内の `ConnectionStrings:MyToDoAppSQLContext` を適切な接続文字列に更新します。

2. **マイグレーションの適用**:
   ```bash
   dotnet ef database update
   ```

3. **アプリケーションの起動**:
   ```bash
   dotnet run
   ```
   ブラウザで `http://localhost:5016` にアクセスします。

## ライセンス
このプロジェクトは MIT ライセンスの下で提供されています。詳細は各ライブラリのライセンスファイルを参照してください。