using BeatmapInformation.Bases;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using static BeatmapInformation.AudioSpectrums.AudioSpectrum;

namespace BeatmapInformation.Models
{
    public class ProfileEntity : NotificationObject, IDisposable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        private bool _enable = true;
        public bool Enable { get => this._enable; set => this.SetProperty(ref this._enable, value); }
        private bool _lockPosition = false;
        public bool LockPosition { get => this._lockPosition; set => this.SetProperty(ref this._lockPosition, value); }
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
        private float _songSubNameFontSize = 5;
        public float SongSubNameFontSize { get => this._songSubNameFontSize; set => this.SetProperty(ref this._songSubNameFontSize, value); }
        private float _songAuthorNameFontSize = 5;
        public float SongAuthorNameFontSize { get => this._songAuthorNameFontSize; set => this.SetProperty(ref this._songAuthorNameFontSize, value); }
        private bool _songKeyVisible = false;
        public bool SongKeyVisible { get => this._songKeyVisible; set => this.SetProperty(ref this._songKeyVisible, value); }
        private float _songKeyFontSize = 5;
        public float SongKeyFontSize { get => this._songKeyFontSize; set => this.SetProperty(ref this._songKeyFontSize, value); }
        private bool _scoreVisible = true;
        public bool ScoreVisible { get => this._scoreVisible; set => this.SetProperty(ref this._scoreVisible, value); }
        private float _scoreFontSize = 12;
        public float ScoreFontSize { get => this._scoreFontSize; set => this.SetProperty(ref this._scoreFontSize, value); }
        private bool _comboVisible = true;
        public bool ComboVisible { get => this._comboVisible; set => this.SetProperty(ref this._comboVisible, value); }
        private float _comboFontSize = 7;
        public float ComboFontSize { get => this._comboFontSize; set => this.SetProperty(ref this._comboFontSize, value); }
        private bool _accVisible = true;
        public bool AccVisible { get => this._accVisible; set => this.SetProperty(ref this._accVisible, value); }
        private float _accFontSize = 10;
        public float AccFontSize { get => this._accFontSize; set => this.SetProperty(ref this._accFontSize, value); }
        private bool _rankVisible = true;
        public bool RankVisible { get => this._rankVisible; set => this.SetProperty(ref this._rankVisible, value); }
        private float _rankFontSize = 10;
        public float RankFontSize { get => this._rankFontSize; set => this.SetProperty(ref this._rankFontSize, value); }
        private float _textSpaceHeight = -1f;
        public float TextSpaceHeight { get => this._textSpaceHeight; set => this.SetProperty(ref this._textSpaceHeight, value); }
        private bool _difficulityLabelVisible = true;
        public bool DifficulityLabelVisible { get => this._difficulityLabelVisible; set => this.SetProperty(ref this._difficulityLabelVisible, value); }
        private float _difficulityLabelFontSize = 5f;
        public float DifficulityLabelFontSize { get => this._difficulityLabelFontSize; set => this.SetProperty(ref this._difficulityLabelFontSize, value); }
        private float _subTextSpacing = -1;
        public float SubTextSpacing { get => this._subTextSpacing; set => this.SetProperty(ref this._subTextSpacing, value); }
        private float _scoreTextSpacing = -5;
        public float ScoreTextSpacing { get => this._scoreTextSpacing; set => this.SetProperty(ref this._scoreTextSpacing, value); }
        private float _rankTextSpacing = -5;
        public float RankTextSpacing { get => this._rankTextSpacing; set => this.SetProperty(ref this._rankTextSpacing, value); }
        private bool _audioSpectrumVisible = true;
        public bool AudioSpectrumVisible { get => this._audioSpectrumVisible; set => this.SetProperty(ref this._audioSpectrumVisible, value); }
        private float _audioSpectrumAlpha = 0.8f;
        public float AudioSpectrumAlpha { get => this._audioSpectrumAlpha; set => this.SetProperty(ref this._audioSpectrumAlpha, value); }
        private BandType _bandType = BandType.ThirtyOneBand;
        [JsonConverter(typeof(StringEnumConverter))]
        public BandType BandType { get => this._bandType; set => this.SetProperty(ref this._bandType, value); }
        private float _screenPosX = 0f;
        public float ScreenPosX { get => this._screenPosX; set => this.SetProperty(ref this._screenPosX, value); }
        private float _screenPosY = 0.7f;
        public float ScreenPosY { get => this._screenPosY; set => this.SetProperty(ref this._screenPosY, value); }
        private float _screenPosZ = -1.1f;
        public float ScreenPosZ { get => this._screenPosZ; set => this.SetProperty(ref this._screenPosZ, value); }
        private float _screenRotX = 0;
        public float ScreenRotX { get => this._screenRotX; set => this.SetProperty(ref this._screenRotX, value); }
        private float _screenRotY = 0;
        public float ScreenRotY { get => this._screenRotY; set => this.SetProperty(ref this._screenRotY, value); }
        private float _screenRotZ = 0;
        public float ScreenRotZ { get => this._screenRotZ; set => this.SetProperty(ref this._screenRotZ, value); }
        private float _ancherMinX = 0;
        public float AncherMinX { get => this._ancherMinX; set => this.SetProperty(ref this._ancherMinX, value); }
        private float _ancherMaxX = 1;
        public float AncherMaxX { get => this._ancherMaxX; set => this.SetProperty(ref this._ancherMaxX, value); }
        private float _ancherMinY = 0;
        public float AncherMinY { get => this._ancherMinY; set => this.SetProperty(ref this._ancherMinY, value); }
        private float _ancherMaxY = 1;
        public float AncherMaxY { get => this._ancherMaxY; set => this.SetProperty(ref this._ancherMaxY, value); }
        private string _songNameFormat = TextFormatter.SONG_NAME;
        public string SongNameFormat { get => this._songNameFormat; set => this.SetProperty(ref this._songNameFormat, value); }
        private string _songSubNameFormat = TextFormatter.SONG_SUB_NAME;
        public string SongSubNameFormat { get => this._songSubNameFormat; set => this.SetProperty(ref this._songSubNameFormat, value); }
        private string _songAuthorNameFormat = $"<color=#888888>{TextFormatter.SONG_AUTHOR_NAME}[{TextFormatter.SONG_MAPPER_NAME}]</color>";
        public string SongAuthorNameFormat { get => this._songAuthorNameFormat; set => this.SetProperty(ref this._songAuthorNameFormat, value); }
        private string _songKeyFormat = $"!bsr {TextFormatter.SONG_KEY}";
        public string SongKeyFormat { get => this._songKeyFormat; set => this.SetProperty(ref this._songKeyFormat, value); }
        private string _difficurityFormat = $"<color=#000000>{TextFormatter.DIFFICURITY}</color>";
        public string DifficurityFormat { get => this._difficurityFormat; set => this.SetProperty(ref this._difficurityFormat, value); }
        private string _scoreFormat = TextFormatter.SCORE;
        public string ScoreFormat { get => this._scoreFormat; set => this.SetProperty(ref this._scoreFormat, value); }
        private string _comboFormat = $"{TextFormatter.COMBO} <size=50%>COMBO</size>";
        public string ComboFormat { get => this._comboFormat; set => this.SetProperty(ref this._comboFormat, value); }
        private string _accFormat = $"{TextFormatter.ACCURACY} %";
        public string AccFormat { get => this._accFormat; set => this.SetProperty(ref this._accFormat, value); }
        private string _rankFormat = TextFormatter.RANK;
        public string RankFormat { get => this._rankFormat; set => this.SetProperty(ref this._rankFormat, value); }
        private string _filePath = "";
        [JsonIgnore]
        public string FilePath { get => this._filePath; set => this.SetProperty(ref this._filePath, value); }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.FilePath)) {
                this.Initialize(FilePath);
            }
            base.OnPropertyChanged(e);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Initialize(string filepath)
        {
            this._filePath = filepath;
            if (this._profileWatcher != null) {
                this._profileWatcher.Changed -= this.OnProfileWatcher_Changed;
                this._profileWatcher.Dispose();
            }
            this._profileWatcher = new FileSystemWatcher(Path.GetDirectoryName(this.FilePath));
            this._profileWatcher.NotifyFilter = s_all;
            this._profileWatcher.Filter = Path.GetFileName(this.FilePath);
            this._profileWatcher.Changed += this.OnProfileWatcher_Changed;
            this._profileWatcher.IncludeSubdirectories = true;
            this._profileWatcher.EnableRaisingEvents = true;
        }
        public void Load()
        {
            if (string.IsNullOrEmpty(this.FilePath) || !File.Exists(this.FilePath)) {
                return;
            }
            lock (s_lockObj) {
                try {
                    var profileJson = File.ReadAllText(this.FilePath);
                    var profile = JsonConvert.DeserializeObject<ProfileEntity>(profileJson);
                    this.CopyFrom(profile);
                }
                catch (Exception e) {
                    Plugin.Log.Error(e);
                }
            }
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.FilePath)) {
                return;
            }
            lock (s_lockObj) {
                this._profileWatcher.EnableRaisingEvents = false;
                try {
                    var profileJson = JsonConvert.SerializeObject(this, Formatting.Indented);
                    File.WriteAllText(this.FilePath, profileJson);
                }
                catch (Exception e) {
                    Plugin.Log.Error(e);
                }
                finally {
                    this._profileWatcher.EnableRaisingEvents = true;
                }
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void OnProfileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.Load();
        }

        private void CopyFrom(ProfileEntity entity)
        {
            foreach (var prop in typeof(ProfileEntity).GetProperties(BindingFlags.Public)) {
                if (prop.Name == nameof(this.FilePath)) {
                    continue;
                }
                prop.SetValue(this, prop.GetValue(entity));
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private static readonly object s_lockObj = new object();
        private FileSystemWatcher _profileWatcher;
        private bool _disposedValue;
        private const NotifyFilters s_all = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue) {
                if (disposing) {

                }
                if (this._profileWatcher != null) {
                    this._profileWatcher.Changed -= this.OnProfileWatcher_Changed;
                    this._profileWatcher.Dispose();
                    this._profileWatcher = null;
                }
                _disposedValue = true;
            }
        }

        ~ProfileEntity()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
