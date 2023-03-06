# BeatmapInformation
曲の情報をプレイスペースの後ろに表示するMOD  
![image](https://user-images.githubusercontent.com/55026301/113311340-2c95b500-9344-11eb-8d90-46aa8dd7b195.png)

# 依存MOD  
SiraUtil  
BeatSaberMarkupLanguage  
SongCore  
# 設定項目一覧  
プロファイルは「UserData\BeatmapInformation\Profile」に保存されます  

[床に置くときの設定のサンプル](https://github.com/denpadokei/BeatmapInformation/releases/download/0.7.0/SettingJsonTemplate.zip)  
[オーバーレイの設定のサンプル](https://github.com/denpadokei/BeatmapInformation/releases/download/1.1.0/BeatmapInformation_Config.zip)  
|設定項目|値|説明|
|---|---|---|
|Enable|bool|有効、無効を設定します。trueにするとゲーム中にスクリーンが表示されます。|
|LockPosition|bool|ポーズ中に移動できるかどうかを表します。<br>trueにするとグリップハンドルが表示されなくなります。|
|OverlayMode|bool|ゲーム上ではなく、画面上に表示するかどうかを設定します。|
|ChangeScale|bool|スクリーンの大きさを変更できるかどうかを表します。trueにすると大きさを変更できます。|
|ScreenScale|float|スクリーンの大きさを変更します。|
|ScreenRadius|float|スクリーンの曲面具合を変更します。|
|ScreenLayer|int|スクリーンが所属するレイヤーを設定します。デフォルトは5(UIレイヤー)です。<br>0とかにするとHideUIにしても消えなくなります。|
|SongTimerVisible|bool|曲の進行具合を表すかどうかを設定します。trueにすると表示されます。|
|SontTimeRingScale|float|曲の進行時間を表すリングの大きさを変更します。|
|SongTimeTextFontSize|float|曲の進行時間のテキストサイズを変更します。|
|CoverVisible|bool|曲のカバー画像を表示するかどうかを設定します。trueにすると表示されます。|
|CoverPivotPos|float|カバー画像の移動起点となるピボットの位置を表します。|
|CoverAlpha|float|曲時間を表示しているときのカバー画像の透明度を表します。|
|CoverSize|float|曲のカバー画像のサイズを変更します。|
|SongNameFontSize|float|曲名のテキストサイズを変更します。|
|SongSubNameFontSize|float|サブタイトルのサイズを変更します。|
|SongAuthorNameFontSize|float|曲製作者のテキストサイズを変更します。|
|ScoreVisible|bool|スコアを表示するかどうかを設定します。trueにすると表示されます。|
|ScoreFontSize|float|スコアのフォントサイズを変更します。|
|ComboVisible|bool|コンボ数を表示するかどうかを設定します。trueにすると表示されます。|
|ComboFontSize|float|コンボ数のテキストサイズを変更します。|
|SeidoVisible|bool|精度を表示するかどうかを設定します。trueにすると表示されます。|
|SeidoFontSize|float|精度のテキストサイズを変更します。|
|RankVisible|bool|ランクを表示するかどうかを設定します。trueにすると表示されます。|
|RankFontSize|float|ランクのフォントサイズを設定します。|
|TextSpaceHeight|float|全体の高さを設定します。|
|DifficulityLabelVisible|bool|難易度を表示するかどうかを設定します。trueにすると表示されます。|
|DifficulityLabelFontSize|float|難易度ラベルのフォントサイズを変更します。|
|SubTextSpacing|float|サブタイトル、曲製作者、難易度ラベルブロックの行間を変更します。|
|ScoreTextSpacing|float|コンボ数、スコアブロックの行間を変更します。|
|RankTextSpacing|float|精度、ランクブロックの行間を変更します。|
|AudioSpectrumVisible|bool|背景の音声波形を表示するかどうかを設定します。trueで表示されます。|
|AudioSpectrumAlpha|float|音声波形の透明度を変更します。|
|BandType|string|音声波形の本数。<br>"FourBand","FourBandVisual","EightBand","TenBand","TwentySixBand"."ThirtyOneBand"のどれかから選んでください。|
|ScreenPosX|float|スクリーンのX座標を設定します。|
|ScreenPosY|float|スクリーンのY座標を設定します。|
|ScreenPosZ|float|スクリーンのZ座標を設定します。|
|ScreenRotX|float|スクリーンのX座標を設定します。|
|ScreenRotY|float|スクリーンのY座標を設定します。|
|ScreenRotZ|float|スクリーンのZ座標を設定します。|
|AncherMinX|float|オーバーレイ時スクリーンの案界の最小X座標を設定します。|
|AncherMaxX|float|オーバーレイ時スクリーンの案界の最大x座標を設定します。|
|AncherMinY|float|オーバーレイ時スクリーンの案界の最小y座標を設定します。|
|AncherMaxY|float|オーバーレイ時スクリーンの案界の最大y座標を設定します。|
|SongNameFormat|string|曲名が入るスペースに表示する文字列を設定します。|
|SongSubNameFormat|string|曲サブタイトルが入るスペースに表示する文字列を設定します。|
|SongAuthorNameFormat|string|作曲者が入るスペースに表示する文字列を設定します。|
|DifficulityFormat|string|難易度が入るスペースに表示する文字列を設定します。|
|ScoreFormat|string|スコアが入るスペースに表示する文字列を設定します。|
|ComboFormat|string|コンボが入るスペースに表示する文字列を設定します。|
|SeidoFormat|string|精度が入るスペースに表示する文字列を設定します。|
|RankFormat|string|ランクが入るスペースに表示する文字列を設定します。|
## 置換される文字列  
|置換元|置換先|
|:---|:---|
|%SONG_NAME%|曲名|
|%SONG_SUB_NAME%|曲のサブタイトル|
|%SONG_AUTHOR_NAME%|作曲者|
|%SONG_MAPPER_NAME%|作成者|
|%SCORE%|スコア（数字のみ、3桁区切りで,が入る）|
|%COMBO%|コンボ数（数字のみ）|
|%SEIDO%|数字のみ（小数点以下2桁）|
|%RANK%|ランク|
