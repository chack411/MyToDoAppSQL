# Azure App Services with SQL DB & .NET ハンズオン

⏲️ _Est. time to complete: 90 min._ ⏲️

## 目次

- [Azure App Services with SQL DB \& .NET ハンズオン](#azure-app-services-with-sql-db--net-ハンズオン)
  - [目次](#目次)
  - [本ハンズオンで学べること 🎯](#本ハンズオンで学べること-)
  - [アプリケーション概要](#アプリケーション概要)
  - [事前準備](#事前準備)
  - [Web アプリとデータベースの作成](#web-アプリとデータベースの作成)
  - [GitHub リポジトリの作成](#github-リポジトリの作成)
  - [アプリのビルドとデプロイ](#アプリのビルドとデプロイ)
  - [デプロイ スロットを利用したアプリケーションの更新](#デプロイ-スロットを利用したアプリケーションの更新)
  - [まとめ](#まとめ)
  - [参考資料](#参考資料)

## 本ハンズオンで学べること 🎯

- Azure App Services と SQL データベースの概要
- .NET と ASP.NET Core の概要
- GitHub の概要
  - GitHub Actions の概要
  - 変更を Commit して Push する方法
  - Pull Request を作成してマージする方法

## アプリケーション概要

このハンズオンで使用する MyToDoAppSQL は、ASP.NET Core 9.0 と Microsoft SQL Server（Entity Framework Core）を用いたシンプルな ToDo 管理 Webアプリケーションです。タスク（ToDo）の登録・編集・削除・詳細表示が可能です。詳細は、GitHub Copilot で生成した [こちら](../README.md) のソフトウェア内部仕様書をご覧ください。

## 事前準備
- GitHub アカウントの作成
  - GitHub のアカウントをお持ちでない場合は、[https://github.com/](https://github.com/) からアカウントを新規作成してください。
- (任意) GitHub Copilot の準備
  - GitHub Copilot を使用する場合は、[こちら](https://github.com/features/copilot) から GitHub Copilot を有効化してください。
- Microsoft Azure サブスクリプションの準備
  - Microsoft Azure のアカウントをお持ちでない場合は、[こちら](https://azure.microsoft.com/ja-jp/free/) からアカウントを新規作成してください。
  - Azure のアカウントをお持ちの方は、[こちら](https://portal.azure.com/) から Azure ポータルにサインインして、トップページのダッシュボードが表示されることを確認してください。
  - 管理者側で用意された Azure サブスクリプションを使用する場合は、Azure ポータルにサインインして、管理者側で事前に払い出された **共同作成者ロールが割り当てられているリソースグループ** が用意されていることを確認してください。

(任意) 本ハンズオンは、Web ブラウザのみで完結するように設計されていますが、Visual Studio 2022 を使用してローカル環境での開発を行うことも可能です。必要に応じて、以下の手順で Visual Studio 2022、Git、.NET のインストールや準備をおこなってください。
- Visual Studio 2022 のインストール
  - Visual Studio 2022 をお持ちでない場合は、[こちら](https://visualstudio.microsoft.com/ja/downloads/) から Visual Studio 2022 をダウンロードしてインストールしてください。
  - インストール時に、`ASP.NET と Web 開発` と `Azure の開発` のワークロードを選択してください。
  - また、`個別のコンポーネント` タブから `Git for Windows` を選択してください。
  - インストール後、Visual Studio 2022 を起動して、`MS アカウント` または `GitHub` アカウントでサインインして初期画面が表示されることを確認してください。
- Git の確認
  - Visual Studio 2022 と共に Git もインストールされます。Windows メニューから `コマンド プロンプト` を開き `git` と入力して Git コマンドが実行できることを確認してください。
  - もし、Git が実行できない場合は、[こちら](https://gitforwindows.org/) から **Git for Windows** をダウンロードして、個別にセットアップしてください。
- .NET 9 SDK の確認
  - Visual Studio 2022 と共に .NET SDK もインストールされます。Windows メニューから `コマンド プロンプト` を開き `dotnet` と入力して .NET コマンドが実行できることを確認してください。
  - もし、`dotnet` コマンドが実行出来ない場合は、[こちら](https://dotnet.microsoft.com/download/dotnet) から **.NET 9 SDK** をダウンロードしてインストールしてください。
- Entity Framework Core ツールのインストール
  - Windows メニューから `コマンド プロンプト` を開き、以下のコマンドを実行して Entity Framework Core ツールをインストールしてください。
    ```bash
    dotnet tool install --global dotnet-ef
    ```

## Web アプリとデータベースの作成
1. Azure ポータルにサインインして、ページ上部にある検索バーに `Webアプリとデータベース` と入力します。
2. [Marketplace] 見出しの下にある `Web アプリとデータベース` を選択して、`Web アプリとデータベースの作成` ページに移動します。
![Web アプリとデータベースの作成](./images/WebApp%20and%20DB.png)
3. `Web アプリとデータベースの作成` ページで、以下の情報を入力してください。
   - サブスクリプション: 使用する Azure サブスクリプション名
   - リソースグループ: 事前に用意された共同作成者ロールが割り当てられているリソースグループを選択するか、[新規作成] から新しく作成するリソースグループ名を入力
   - リージョン: `Japan East` (任意のリージョン)
   - 名前: `mytodowebapp` (任意の名前)
   - ランタイムスタック: `.NET 9 (STS)`
   - エンジン: `SQLAzure (推奨)`
   - サーバー名: `mytodowebapp-server` (任意の名前)
   - データベース名: `mytodowebapp-database` (任意の名前)
   - Azure Cache for Redis を追加しますか?: `いいえ`
   - ホスティング プラン: `Basic - 趣味や研究目的`
![Web アプリとデータベースの作成](./images/Create%20WebApp%20and%20DB.png)
1. 上記の情報を入力したら、`確認および作成` をクリックしてください。
2. `確認および作成` ページでエラーがなければ、`作成` をクリックしてください。
3. デプロイメントが完了するまで数分かかります。デプロイメントが完了したら、[リソースに移動] ボタンをクリックしてください。
4. 作成した Web アプリの概要ページが表示されます。![Web アプリの概要ページ](./images/WebApp%20details.png)
5. [規定のドメイン] にある URL をクリックして、Web アプリが正常に作成されたことを確認してください。![Web アプリ初期ページ](./images/WebApp%20initial%20page.png)

## GitHub リポジトリの作成
1. GitHub にサインインして、次のサンプル リポジトリを表示します。
   - [https://github.com/chack411/MyToDoAppSQL](https://github.com/chack411/MyToDoAppSQL)
2. 右上の `Use this template` ボタンをクリックして、`Create a new repository` をクリックします。![Create a new repository](./images/Create%20a%20new%20repository.png)
3. `Owner` で自分のアカウント名を選び、`Repository name` に `MyToDoAppSQL` などの任意の名前を入力します。
4. リポジトリの公開設定を `Public` (または `Private`) に設定します。
5. `Create repository` をクリックします。![Create a new repository](./images/Create%20a%20new%20repository2.png)
6. ご自身の GitHub アカウントのリポジトリに、指定した名前でリポジトリが作成されたことを確認します。![Create a new repository](./images/Create%20a%20new%20repository3.png)

## アプリのビルドとデプロイ
1. Azure ポータルにサインインして、作成した Web アプリの概要ページを開きます。
2. 左側のメニューから `設定` - `環境設定` を選択して、`接続文字列` タブを選択します。`AZURE_SQL_CONNECTIONSTRING` という名前の接続文字列があることを確認します。![接続文字列](./images/Connection%20string.png)
3. この接続文字列は、Web アプリを作成したときに自動生成された、SQL データベースへの接続情報です。この文字列をクリックして、`接続文字列の追加/編集` ページを開き、名前を `MyToDoAppSQLContext` に変更して、[適用] ボタンをクリックします。この接続文字列名 `MyToDoAppSQLContext` は、ソースコードで指定されている名前になります。![接続文字列の追加/編集](./images/Connection%20string2.png)
4. `接続文字列` の一覧ページに戻りますので、名前が変更されていることを確認し、もう一度ページ下部にある `適用` ボタンをクリックして変更を保存します。![接続文字列の追加/編集](./images/Connection%20string3.png)
5. 続いて、左側のメニューから `デプロイ` - `デプロイ センター` を選択します。
6. `デプロイ センター` ページの [ソース] で、`GitHub` を選択し、GitHub にサインインします。
7. `GitHub` のリポジトリを選択します。
   - 組織: ご自身の GitHub アカウント名
   - Repository: `MyToDoAppSQL` (作成したリポジトリ名)
   - Branch: `main`
8. `認証タイプ` で `基本認証` を選択します。
   - Azure サブスクリプションの所有者や共同作成者権限を持っている場合は、`ユーザー割り当て ID` を選択することで、セキュアな認証を行うこともできます。
9. `デプロイ センター` ページ上部の [保存] ボタンをクリックして、設定を保存します。
![Deployment Center](./images/Deployment%20Center.png)
10. GitHub のリポジトリに戻り、Actions タブをクリックして、`Azure App Service` のデプロイメントが開始されていることを確認します。![GitHub Actions](./images/GitHub%20Actions.png)
11. デプロイメントが完了したら、Azure ポータルに戻り、作成した Web アプリの概要ページを開きます。
12. [規定のドメイン] にある URL をクリックして、Web アプリを表示します。初回の表示には数分かかります。また、初期画面で `Error` が表示されます。![Web アプリ初期ページ](./images/WebApp%20Error.png)
13. このエラーは、**SQL Database にデータベースが作成されていない** ために発生しています。次の手順で、GitHub Actions のワークフロー ファイル (YAML ファイル) を更新して、データベースの初期化コマンドを実行して SQL Database にデータベースを作成します。
14. GitHub のリポジトリに戻り、`Actions` タブをクリックして、`Add or update the Azure App Service build and deployment workflow config` のワークフローを選択します。![GitHub Actions](./images/GitHub%20Actions2.png)
15. `main_<Azureで作成したWebアプリ名>.yml` をクリックして、ワークフロー ファイルを開きます。![GitHub Actions](./images/GitHub%20Actions3.png)
16. このファイルは、GitHub Actions のワークフロー ファイルで、Azure App Service にデプロイするための設定が記述されています。ワークフロー ファイルの上部右側にある `鉛筆マーク` ボタンをクリックして、ワークフロー ファイルの編集画面に移ります。![GitHub Actions](./images/GitHub%20Actions4.png)
17. 32行目の `name: Upload artifact for deployment job` の前に、以下のコマンドを追加してください。
```yaml
      - name: Database migration
        run: |
          dotnet tool install -g dotnet-ef
          dotnet ef migrations bundle --runtime linux-x64 -p MyToDoAppSQL -o ${{env.DOTNET_ROOT}}/myapp/migrationsbundle
```
![GitHub Actions](./images/GitHub%20Actions5.png)
18. 追加したら、`Commit changes` ボタンをクリックして、変更をコミットします。この際 `Commit directly to hte main branch` が選択されていることを確認してください。![GitHub Actions](./images/GitHub%20Actions6.png)
19. 再び、GitHub のリポジトリに戻り、`Actions` タブをクリックして、新しいワークフローが開始されていることを確認します。![GitHub Actions](./images/GitHub%20Actions7.png)
20. ワークフローの実行が完了したら、Azure ポータルに戻り、作成した Web アプリの概要ページを開きます。
21. ページ左側のメニューから `開発ツール` - `SSH` を選択して、`移動 →` ボタンをクリックします。![SSH](./images/SSH.png)
22. `SSH` ページが表示されます。![SSH](./images/SSH2.png)
23. 次のコマンドを入力して `/home/site/wwwroot` フォルダに移動します。
    ```bash
    cd /home/site/wwwroot
    ```

24. `wwwroot` フォルダに移動したら、`ls` コマンドを入力して、デプロイした Web アプリのファイル群があることを確認します。
25. `migrationsbundle` コマンドを実行します。   
    ```bash
    migrationsbundle
    ```
26. このコマンドは、アプリケーションのデータベースのスキーマに基づいて、SQL データベースの初期化を行うコマンドです。実行後、以下のようなメッセージが表示され、初期状態の SQL データベースが作成されます。![SSH](./images/SSH3.png)
27. ふたたび、Azure ポータルに戻り、作成した Web アプリの概要ページを開きます。[規定のドメイン] にある URL をクリックして、Web アプリがエラーなく表示されることを確認します。![Web アプリ初期ページ](./images/WebApp%20initial%20page2.png)
28. `+ Create New` ボタンをクリックして、タスクを追加して、動作を確認します。![Web アプリ初期ページ](./images/WebApp%20create%20task.png)
![Web アプリ初期ページ](./images/WebApp%20initial%20page3.png)


## デプロイ スロットを利用したアプリケーションの更新

WIP...

## まとめ

以上で、Azure App Services と SQL データベースを使用した .NET アプリケーションのデプロイが完了しました。GitHub Actions を使用して、アプリケーションのビルドとデプロイを自動化する方法についても学びました。
今後は、GitHub Copilot を使用して、アプリケーションの機能を追加したり、デザインを変更したりすることができます。また、Azure App Services のデプロイ スロットを利用して、アプリケーションの更新を行うこともできます。これにより、アプリケーションの可用性を高めることができます。さらには、Azure の他のサービスを組み合わせて、より高度なアプリケーションを構築することも可能です。例えば、Azure Functions を使用してサーバーレスアーキテクチャを実現したり、Azure Cosmos DB を使用して分散データベースを構築したりすることができます。
ぜひ、Azure のサービスを活用して、より高度なアプリケーションを構築してみてください。

おつかれさまでした！

## 参考資料
- [Azure App Service](https://azure.microsoft.com/ja-jp/services/app-service/)
- [Azure SQL Database](https://azure.microsoft.com/ja-jp/services/sql-database/)
