# BeatmapInformation

[![Build and Release](https://github.com/denpadokei/BeatmapInformation/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/BeatmapInformation/actions/workflows/build-and-release.yml)

プレイ中の曲情報（曲名・作曲者・難易度・スコア・コンボ・精度・ランク・カバー画像・音声波形）を、プレイスペース上のフローティングスクリーンに表示する MOD です。プロファイルごとに表示内容・サイズ・位置を変えられ、複数のスクリーンを同時に表示できます。

## 機能

- 曲名、サブタイトル、作曲者／マッパー、難易度ラベル、スコア、コンボ、精度、ランクをリアルタイムに表示
- 曲のカバー画像と、曲の進行度を示すリング＋経過時間を表示
- 背景に音声スペクトラム（4〜31 バンド）を表示
- 表示する項目、フォントサイズ、行間、画面サイズ、曲面の半径、レイヤー、位置・角度をプロファイル単位で設定
- プロファイルは `UserData/BeatmapInformation/Profile` の JSON ファイル単位で管理し、複数有効化すると複数スクリーンを表示
- オーバーレイモードで画面上（スクリーンスペース）に表示可能
- ポーズ中にグリップで掴んで移動・回転でき、`LockPosition` で固定可能
- テキストは `%SONG_NAME%` などの置換文字列と TMP のリッチテキストに対応
- 表示中の曲の BeatSaver キー（`!bsr` 用）を取得して表示可能（`api.beatsaver.com` へ HTTPS で問い合わせ）

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.40.8 |
| 依存 MOD | BSIPA 4.3.6 / BeatSaberMarkupLanguage 1.12.5 / SiraUtil 3.2.0 / SongCore 3.15.3 |

## インストール

1. Beat Saber を 1.40.8 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/BeatmapInformation/releases) から `BeatmapInformation-7.0.0-bs1.40.8.zip` をダウンロードし、`Plugins` と `UserData` フォルダをゲームフォルダへ上書きコピーする

zip には `Plugins/BeatmapInformation.dll` と、プロファイルのサンプル (`UserData/BeatmapInformation/Profile/Default.json` / `BeatmapInformation_flor.json` / `BeatmapInformation_overlay.json`) が含まれます。

## 使い方

1. ゲーム内の設定（Mod Settings）に追加される **Beatmap Information** タブを開きます。
2. プロファイル一覧のチェックで有効／無効を切り替えます。南京錠アイコンで位置を固定できます。
3. 有効なプロファイルの数だけスクリーンが表示されます。有効化の変更は次のプレイから反映されます。
4. 位置を固定していない場合、ポーズ中に表示されるハンドルを掴んで移動・回転できます。移動結果はプロファイルに保存されます。

スクリーンの見た目や表示項目はプロファイル JSON を直接編集して変更します。ファイルを保存すると自動で再読み込みされ、プレイ中のスクリーンにも反映されます。

サンプルプロファイル:

- [床に置くときの設定のサンプル](https://github.com/denpadokei/BeatmapInformation/files/10899962/BeatmapInformation_flor.zip)
- [オーバーレイの設定のサンプル](https://github.com/denpadokei/BeatmapInformation/files/10899964/BeatmapInformation_overlay.zip)

## 設定

プロファイルは `UserData/BeatmapInformation/Profile/*.json` に保存されます（1 ファイル = 1 スクリーン）。

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| Enable | bool | true | このプロファイルのスクリーンを表示する |
| LockPosition | bool | false | true にするとポーズ中もハンドルを表示せず、位置を固定する |
| OverlayMode | bool | false | ゲーム内ではなく画面上（スクリーンスペースオーバーレイ）に表示する |
| ChangeScale | bool | false | `ScreenScale` による拡大縮小を有効にする |
| ScreenScale | float | 0.02 | スクリーンの大きさ |
| ScreenRadius | float | 0.0 | スクリーンの曲面半径 |
| ScreenLayer | int | 5 | スクリーンのレイヤー。既定は UI レイヤー。0 などにすると HideUI でも消えなくなる |
| SongTimerVisible | bool | true | 曲の進行リングと経過時間を表示する |
| SongTimeRingScale | float | 1.7 | 進行リングの大きさ |
| SongTimeTextFontSize | float | 7.0 | 経過時間テキストのサイズ |
| CoverVisible | bool | true | カバー画像を表示する |
| CoverPivotPos | float | 0.75 | カバー画像のピボット位置 |
| CoverAlpha | float | 0.75 | 曲時間表示中のカバー画像の透明度 |
| CoverSize | float | 40.0 | カバー画像のサイズ |
| SongNameFontSize | float | 13.0 | 曲名のサイズ |
| SongSubNameFontSize | float | 5.0 | サブタイトルのサイズ |
| SongAuthorNameFontSize | float | 5.0 | 作曲者／マッパーのサイズ |
| SongKeyVisible | bool | false | BeatSaver キーを表示する |
| SongKeyFontSize | float | 5.0 | BeatSaver キーのサイズ |
| ScoreVisible | bool | true | スコアを表示する |
| ScoreFontSize | float | 12.0 | スコアのサイズ |
| ComboVisible | bool | true | コンボを表示する |
| ComboFontSize | float | 7.0 | コンボのサイズ |
| AccVisible | bool | true | 精度を表示する |
| AccFontSize | float | 10.0 | 精度のサイズ |
| RankVisible | bool | true | ランクを表示する |
| RankFontSize | float | 10.0 | ランクのサイズ |
| DifficulityLabelVisible | bool | true | 難易度ラベルを表示する |
| DifficulityLabelFontSize | float | 5.0 | 難易度ラベルのサイズ |
| TextSpaceHeight | float | -1.0 | テキスト領域の高さ（-1 で自動） |
| SubTextSpacing | float | -1.0 | サブタイトル・作曲者・難易度ブロックの行間 |
| ScoreTextSpacing | float | -5.0 | コンボ・スコアブロックの行間 |
| RankTextSpacing | float | -5.0 | 精度・ランクブロックの行間 |
| AudioSpectrumVisible | bool | true | 背景の音声波形を表示する |
| AudioSpectrumAlpha | float | 0.8 | 音声波形の透明度 |
| BandType | string | "ThirtyOneBand" | 波形のバンド数。`FourBand` / `FourBandVisual` / `EightBand` / `TenBand` / `TwentySixBand` / `ThirtyOneBand` |
| ScreenPosX / ScreenPosY / ScreenPosZ | float | 0.0 / 0.7 / -1.1 | スクリーンの位置 |
| ScreenRotX / ScreenRotY / ScreenRotZ | float | 0.0 / 0.0 / 0.0 | スクリーンの角度 |
| AncherMinX / AncherMaxX / AncherMinY / AncherMaxY | float | 0.0 / 1.0 / 0.0 / 1.0 | 波形とテキストのアンカー範囲 |
| SongNameFormat | string | "%SONG_NAME%" | 曲名欄の書式 |
| SongSubNameFormat | string | "%SONG_SUB_NAME%" | サブタイトル欄の書式 |
| SongAuthorNameFormat | string | "<color=#888888>%SONG_AUTHOR_NAME%[%SONG_MAPPER_NAME%]</color>" | 作曲者欄の書式 |
| SongKeyFormat | string | "!bsr %SONG_KEY%" | BeatSaver キー欄の書式 |
| DifficurityFormat | string | "<color=#000000>%DIFFICURITY%</color>" | 難易度欄の書式 |
| ScoreFormat | string | "%SCORE%" | スコア欄の書式 |
| ComboFormat | string | "%COMBO% <size=50%>COMBO</size>" | コンボ欄の書式 |
| AccFormat | string | "%SEIDO% %" | 精度欄の書式 |
| RankFormat | string | "%RANK%" | ランク欄の書式 |

書式中の置換文字列:

| 置換元 | 置換先 |
| --- | --- |
| %SONG_NAME% | 曲名 |
| %SONG_SUB_NAME% | サブタイトル |
| %SONG_AUTHOR_NAME% | 作曲者 |
| %SONG_MAPPER_NAME% | マッパー名（複数の場合はカンマ区切り） |
| %SONG_KEY% | BeatSaver キー |
| %DIFFICURITY% | 難易度 |
| %SCORE% | スコア（3 桁区切りのカンマ付き） |
| %COMBO% | コンボ数 |
| %SEIDO% | 精度（小数点以下 2 桁） |
| %RANK% | ランク |

## ビルド

```powershell
git clone https://github.com/denpadokei/BeatmapInformation.git
cd BeatmapInformation
dotnet build .\BeatmapInformation\BeatmapInformation.csproj -c Release
```

`BeatSaberDir` に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT License（`LICENSE`）。
