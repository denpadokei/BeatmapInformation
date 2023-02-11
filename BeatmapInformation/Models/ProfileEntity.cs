using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Bases;
using BeatmapInformation.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapInformation.Models
{
    public class ProfileEntity : NotificationObject
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        private bool _enable;
        public bool Enable { get => this._enable; set => this.SetProperty(ref this._enable, value); }
        private bool _lockPsition;
        public bool LockPosition { get => this._lockPsition; set => this.SetProperty(ref this._lockPsition, value); }
        private bool _overlayMode = false;
        public bool OverlayMode { get => this._overlayMode; set => this.SetProperty(ref this._overlayMode, value); }
        private bool _changeScale = false;
        public bool ChangeScale { get => this._changeScale; set => this.SetProperty(ref this._changeScale, value); }
        private float _screenScale = 0.02f;
        public float ScreenScale { get => this._screenScale; set => this.SetProperty(ref this._screenScale, value); }
        private float _screenRadius = 0f;
        public float ScreenRadius { get => this._screenRadius; set => this.SetProperty(ref this._screenRadius, value); }
        private int _screemLayer = 5;
        public int ScreenLayer { get => this._screemLayer; set => this.SetProperty(ref this._screemLayer, value); }
        private bool _songTimeVisible = true;
        public bool SongTimerVisible { get => this._songTimeVisible; set => this.SetProperty(ref this._songTimeVisible, value); }
        private float _songTimeRingScale = 1.7f;
        public float SongTimeRingScale { get => this._songTimeRingScale; set => this.SetProperty(ref this._songTimeRingScale, value); }
        private float _songTimeFontSize = 7f;
        public float SongTimeTextFontSize { get => this._songTimeFontSize; set => this.SetProperty(ref this._songTimeFontSize, value); }
        private bool _coverVisible = true;
        public bool CoverVisible { get => this._coverVisible; set => this.SetProperty(ref this._coverVisible, value); }
        private float _coverPivotPos = 0.75f;
        public float CoverPivotPos { get => this._coverPivotPos; set => this.SetProperty(ref this._coverPivotPos, value); }
        private float _coverAlpha = 0.75f;
        public float CoverAlpha { get => this._coverAlpha; set => this.SetProperty(ref this._coverAlpha, value); }
        private float _coverSize = 40f;
        public float CoverSize { get => this._coverSize; set => this.SetProperty(ref this._coverSize, value); }
        private float _songNameFontSize = 13;
        public float SongNameFontSize { get => this._songNameFontSize; set => this.SetProperty(ref this._songNameFontSize, value); }
        public float _songSubNameFontSize = 5;
        public float SongSubNameFontSize { get => this._songSubNameFontSize; set => this.SetProperty(ref this._songSubNameFontSize, value); }
        public float _songAuthorNameFontSize = 5;
        public float SongAuthorNameFontSize { get => this._songAuthorNameFontSize; set => this.SetProperty(ref this._songAuthorNameFontSize, value); }
        public bool _songKeyVisible = false;
        public bool SongKeyVisible { get => this._songKeyVisible; set => this.SetProperty(ref this._songKeyVisible, value); }
        public float _songKeyFontSize = 5;
        public float SongKeyFontSize { get => this._songKeyFontSize; set => this.SetProperty(ref this._songKeyFontSize, value); }
        public bool _scoreVisible = true;
        public bool ScoreVisible { get => this._scoreVisible; set => this.SetProperty(ref this._scoreVisible, value); }
        public float _scoreFontSize = 12;
        public float ScoreFontSize { get => this._scoreFontSize; set => this.SetProperty(ref this._scoreFontSize, value); }
        public bool _comboVisible = true;
        public bool ComboVisible { get => this._comboVisible; set => this.SetProperty(ref this._comboVisible, value); }
        public float _comboFontSize = 7;
        public float ComboFontSize { get => this._comboFontSize; set => this.SetProperty(ref this._comboFontSize, value); }
        public bool _accVisible = true;
        public bool AccVisible { get => this._accVisible; set => this.SetProperty(ref this._accVisible, value); }
        public float _accFontSize = 10;
        public float AccFontSize { get => this._accFontSize; set => this.SetProperty(ref this._accFontSize, value); }
        public bool _rankVisible = true;
        public bool RankVisible { get => this._rankVisible; set => this.SetProperty(ref this._rankVisible, value); }
        public float _rankFontSize = 10;
        public float RankFontSize { get => this._rankFontSize; set => this.SetProperty(ref this._rankFontSize, value); }
        public float _textSpaceHeight = -1f;
        public float TextSpaceHeight { get => this._textSpaceHeight; set => this.SetProperty(ref this._textSpaceHeight, value); }
        public bool _difficulityLabelVisible = true;
        public bool DifficulityLabelVisible { get => this._difficulityLabelVisible; set => this.SetProperty(ref this._difficulityLabelVisible, value); }
        public float _difficulityLabelFontSize = 5f;
        public float DifficulityLabelFontSize { get => this._difficulityLabelFontSize; set => this.SetProperty(ref this._difficulityLabelFontSize, value); }
        public float _subTextSpacing = -1;
        public float SubTextSpacing { get => this._subTextSpacing; set => this.SetProperty(ref this._subTextSpacing, value); }
        public float _scoreTextSpacing = -5;
        public float ScoreTextSpacing { get => this._scoreTextSpacing; set => this.SetProperty(ref this._scoreTextSpacing, value); }
        public float _rankTextSpacing = -5;
        public float RankTextSpacing { get => this._rankTextSpacing; set => this.SetProperty(ref this._rankTextSpacing, value); }
        public bool _audioSpectrumVisible = true;
        public bool AudioSpectrumVisible { get => this._audioSpectrumVisible; set => this.SetProperty(ref this._audioSpectrumVisible, value); }
        public float _audioSpectrumAlpha = 0.8f;
        public float AudioSpectrumAlpha { get => this._audioSpectrumAlpha; set => this.SetProperty(ref this._audioSpectrumAlpha, value); }
        public string _bandType = AudioSpectrum.BandType.ThirtyOneBand.ToString();
        public string BandType { get => this._bandType; set => this.SetProperty(ref this._bandType, value); }
        public float _screenPosX = 0f;
        public float ScreenPosX { get => this._screenPosX; set => this.SetProperty(ref this._screenPosX, value); }
        public float _screenPosY = 0.7f;
        public float ScreenPosY { get => this._screenPosY; set => this.SetProperty(ref this._screenPosY, value); }
        public float _screenPosZ = -1.1f;
        public float ScreenPosZ { get => this._screenPosZ; set => this.SetProperty(ref this._screenPosZ, value); }
        public float _screenRotX = 0;
        public float ScreenRotX { get => this._screenRotX; set => this.SetProperty(ref this._screenRotX, value); }
        public float _screenRotY = 0;
        public float ScreenRotY { get => this._screenRotY; set => this.SetProperty(ref this._screenRotY, value); }
        public float _screenRotZ = 0;
        public float ScreenRotZ { get => this._screenRotZ; set => this.SetProperty(ref this._screenRotZ, value); }
        public float _ancherMinX = 0;
        public float AncherMinX { get => this._ancherMinX; set => this.SetProperty(ref this._ancherMinX, value); }
        public float _ancherMaxX = 1;
        public float AncherMaxX { get => this._ancherMaxX; set => this.SetProperty(ref this._ancherMaxX, value); }
        public float _ancherMinY = 0;
        public float AncherMinY { get => this._ancherMinY; set => this.SetProperty(ref this._ancherMinY, value); }
        public float _ancherMaxY = 1;
        public float AncherMaxY { get => this._ancherMaxY; set => this.SetProperty(ref this._ancherMaxY, value); }
        public string _songNameFormat = TextFormatter.SONG_NAME;
        public string SongNameFormat { get => this._songNameFormat; set => this.SetProperty(ref this._songNameFormat, value); }
        public string _songSubNameFormat = TextFormatter.SONG_SUB_NAME;
        public string SongSubNameFormat { get => this._songSubNameFormat; set => this.SetProperty(ref this._songSubNameFormat, value); }
        public string _songAuthorNameFormat = $"<color=#888888>{TextFormatter.SONG_AUTHOR_NAME}[{TextFormatter.SONG_MAPPER_NAME}]</color>";
        public string SongAuthorNameFormat { get => this._songAuthorNameFormat; set => this.SetProperty(ref this._songAuthorNameFormat, value); }
        public string _songKeyFormat = $"!bsr {TextFormatter.SONG_KEY}";
        public string SongKeyFormat { get => this._songKeyFormat; set => this.SetProperty(ref this._songKeyFormat, value); }
        public string _difficurityFormat = $"<color=#000000>{TextFormatter.DIFFICURITY}</color>";
        public string DifficurityFormat { get => this._difficurityFormat; set => this.SetProperty(ref this._difficurityFormat, value); }
        public string _scoreFormat = TextFormatter.SCORE;
        public string ScoreFormat { get => this._scoreFormat; set => this.SetProperty(ref this._scoreFormat, value); }
        public string _comboFormat = $"{TextFormatter.COMBO} <size=50%>COMBO</size>";
        public string ComboFormat { get => this._comboFormat; set => this.SetProperty(ref this._comboFormat, value); }
        public string _accFormat = $"{TextFormatter.ACCURACY} %";
        public string AccFormat { get => this._accFormat; set => this.SetProperty(ref this._accFormat, value); }
        public string _rankFormat = TextFormatter.RANK;
        public string RankFormat { get => this._rankFormat; set => this.SetProperty(ref this._rankFormat, value); }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Load(string filePath)
        {

        }

        public void Save(string filePath)
        {

        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private string _filePath;
        private static readonly string s_rootProfileDir = Path.Combine(Environment.CurrentDirectory, "UserData", "BeatmapInformation", "Profile");
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        static ProfileEntity()
        {
            if (!Directory.Exists(s_rootProfileDir)) {
                Directory.CreateDirectory(s_rootProfileDir);
            }
        }

        public ProfileEntity(string filePath)
        {
            this._filePath = filePath;
        }
        #endregion
    }
}
