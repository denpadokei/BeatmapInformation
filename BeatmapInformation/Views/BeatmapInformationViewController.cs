using BeatmapInformation.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SongCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VRUIControls;
using Zenject;

namespace BeatmapInformation.Views
{
    [HotReload]
    public class BeatmapInformationViewController : BSMLAutomaticViewController
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        // For this method of setting the ResourceName, this class must be the first class in the file.
        //public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

        /// <summary>カバー画像の表示 を取得、設定</summary>
        private bool coverVisible_;
        [UIValue("cover-visible")]
        /// <summary>カバー画像の表示 を取得、設定</summary>
        public bool CoverVisible
        {
            get => this.coverVisible_;

            set => this.SetProperty(ref this.coverVisible_, value);
        }

        /// <summary>カバー画像のサイズ を取得、設定</summary>
        private float coverSize_;
        [UIValue("cover-size")]
        /// <summary>カバー画像のサイズ を取得、設定</summary>
        public float CoverSize
        {
            get => this.coverSize_;

            set => this.SetProperty(ref this.coverSize_, value);
        }

        /// <summary>カバー画像のピボット を取得、設定</summary>
        private float coverPivot_;
        [UIValue("cover-pivot")]
        /// <summary>カバー画像のピボット を取得、設定</summary>
        public float CoverPivot
        {
            get => this.coverPivot_;

            set => this.SetProperty(ref this.coverPivot_, value);
        }

        /// <summary>テキストスペースの高さ を取得、設定</summary>
        private float textSpaceHeight_;
        [UIValue("text-height")]
        /// <summary>テキストスペースの高さ を取得、設定</summary>
        public float TextSpaceHeight
        {
            get => this.textSpaceHeight_;

            set => this.SetProperty(ref this.textSpaceHeight_, value);
        }

        /// <summary>テキストスペースの幅 を取得、設定</summary>
        private float textSpaceWidth_;
        [UIValue("text-width")]
        /// <summary>テキストスペースの幅 を取得、設定</summary>
        public float TextSpaceWidth
        {
            get => this.textSpaceWidth_;

            set => this.SetProperty(ref this.textSpaceWidth_, value);
        }

        /// <summary>曲のタイトル を取得、設定</summary>
        private string songName_;
        [UIValue("song-name")]
        /// <summary>曲のタイトル を取得、設定</summary>
        public string SongName
        {
            get => this.songName_ ?? "SongName";

            set => this.SetProperty(ref this.songName_, value);
        }

        /// <summary>曲名のフォントサイズ を取得、設定</summary>
        private int songNameFontSize_;
        [UIValue("song-name-fontsize")]
        /// <summary>曲名のフォントサイズ を取得、設定</summary>
        public int SongNameFontSize
        {
            get => this.songNameFontSize_;

            set => this.SetProperty(ref this.songNameFontSize_, value);
        }

        /// <summary>曲のサブタイトル を取得、設定</summary>
        private string songSubName_;
        [UIValue("song-sub-name")]
        /// <summary>曲のサブタイトル を取得、設定</summary>
        public string SongSubName
        {
            get => this.songSubName_ ?? "";

            set => this.SetProperty(ref this.songSubName_, value);
        }

        /// <summary>サブタイトルフォントサイズ を取得、設定</summary>
        private int songSubNameFontSize_;
        [UIValue("song-sub-name-fontsize")]
        /// <summary>サブタイトルフォントサイズ を取得、設定</summary>
        public int SongSUbNameFontSIze
        {
            get => this.songSubNameFontSize_;

            set => this.SetProperty(ref this.songSubNameFontSize_, value);
        }

        /// <summary>曲作者 を取得、設定</summary>
        private string songAuthor_;
        [UIValue("song-author")]
        /// <summary>曲作者 を取得、設定</summary>
        public string SongAuthor
        {
            get => this.songAuthor_ ?? "";

            set => this.SetProperty(ref this.songAuthor_, value);
        }

        /// <summary>曲作者フォントサイズ を取得、設定</summary>
        private int songAuthorFontsize_;
        [UIValue("song-author-fontsize")]
        /// <summary>曲作者フォントサイズ を取得、設定</summary>
        public int SongAuthorFontsize
        {
            get => this.songAuthorFontsize_;

            set => this.SetProperty(ref this.songAuthorFontsize_, value);
        }

        /// <summary>難易度の表示 を取得、設定</summary>
        private bool difficulityLabelVisible_;
        [UIValue("difficulity-label-visible")]
        /// <summary>難易度の表示 を取得、設定</summary>
        public bool DifficulityLabelVisible
        {
            get => this.difficulityLabelVisible_;

            set => this.SetProperty(ref this.difficulityLabelVisible_, value);
        }

        /// <summary>難易度 を取得、設定</summary>
        private string difficulity_;
        [UIValue("difficulity")]
        /// <summary>難易度 を取得、設定</summary>
        public string Difficulity
        {
            get => this.difficulity_;

            set => this.SetProperty(ref this.difficulity_, value);
        }

        /// <summary>難易度フォントサイズ を取得、設定</summary>
        private int difficulityFontsize_;
        [UIValue("difficulity-fontsize")]
        /// <summary>難易度フォントサイズ を取得、設定</summary>
        public int DifficulityFontSize
        {
            get => this.difficulityFontsize_;

            set => this.SetProperty(ref this.difficulityFontsize_, value);
        }

        /// <summary>コンボ数の表示 を取得、設定</summary>
        private bool comboVisible_;
        [UIValue("combo-visible")]
        /// <summary>コンボ数の表示 を取得、設定</summary>
        public bool ComboVisible
        {
            get => this.comboVisible_;

            set => this.SetProperty(ref this.comboVisible_, value);
        }

        /// <summary>コンボ を取得、設定</summary>
        private string combo_;
        [UIValue("combo")]
        /// <summary>コンボ を取得、設定</summary>
        public string Combo
        {
            get => this.combo_ ?? "";

            set => this.SetProperty(ref this.combo_, value);
        }

        /// <summary>コンボ数フォントサイズ を取得、設定</summary>
        private int comboFontSize_;
        [UIValue("combo-fontsize")]
        /// <summary>コンボ数フォントサイズ を取得、設定</summary>
        public int ComboFontSize
        {
            get => this.comboFontSize_;

            set => this.SetProperty(ref this.comboFontSize_, value);
        }

        /// <summary>スコア表示 を取得、設定</summary>
        private bool scoreVisible_;
        [UIValue("score-visible")]
        /// <summary>スコア表示 を取得、設定</summary>
        public bool ScoreVisible
        {
            get => this.scoreVisible_;

            set => this.SetProperty(ref this.scoreVisible_, value);
        }

        /// <summary>スコア を取得、設定</summary>
        private string score_;
        [UIValue("score")]
        /// <summary>スコア を取得、設定</summary>
        public string Score
        {
            get => this.score_ ?? "0";

            set => this.SetProperty(ref this.score_, value);
        }

        /// <summary>スコアフォントサイズ を取得、設定</summary>
        private int scoreFontsize_;
        [UIValue("score-fontsize")]
        /// <summary>スコアフォントサイズ を取得、設定</summary>
        public int ScoreFontsize
        {
            get => this.scoreFontsize_;

            set => this.SetProperty(ref this.scoreFontsize_, value);
        }

        /// <summary>ランク表示 を取得、設定</summary>
        private bool rankVisible_;
        [UIValue("rank-visible")]
        /// <summary>ランク表示 を取得、設定</summary>
        public bool RankVisible
        {
            get => this.rankVisible_;

            set => this.SetProperty(ref this.rankVisible_, value);
        }

        /// <summary>ランク を取得、設定</summary>
        private string rank_;
        [UIValue("rank")]
        /// <summary>ランク を取得、設定</summary>
        public string Rank
        {
            get => this.rank_ ?? "SS";

            set => this.SetProperty(ref this.rank_, value);
        }

        /// <summary>ランクフォントサイズ を取得、設定</summary>
        private int rankFontsize_;
        [UIValue("rank-fontsize")]
        /// <summary>ランクフォントサイズ を取得、設定</summary>
        public int RankFontsize
        {
            get => this.rankFontsize_;

            set => this.SetProperty(ref this.rankFontsize_, value);
        }

        /// <summary>精度の表示 を取得、設定</summary>
        private bool seidoVisible_;
        [UIValue("seido-visible")]
        /// <summary>精度の表示 を取得、設定</summary>
        public bool SeidoVisible
        {
            get => this.seidoVisible_;

            set => this.SetProperty(ref this.seidoVisible_, value);
        }

        /// <summary>精度 を取得、設定</summary>
        private string seido_;
        [UIValue("seido")]
        /// <summary>精度 を取得、設定</summary>
        public string Seido
        {
            get => this.seido_ ?? "";

            set => this.SetProperty(ref this.seido_, value);
        }

        /// <summary>精度フォントサイズ を取得、設定</summary>
        private int seidoFontsize_;
        [UIValue("seido-fontsize")]
        /// <summary>精度フォントサイズ を取得、設定</summary>
        public int SeidoFontsize
        {
            get => this.seidoFontsize_;

            set => this.SetProperty(ref this.seidoFontsize_, value);
        }

        /// <summary>サブテキストのスペーシング を取得、設定</summary>
        private float subTextSpacing_;
        [UIValue("sub-text-spacing")]
        /// <summary>サブテキストのスペーシング を取得、設定</summary>
        public float SubTextSpacing
        {
            get => this.subTextSpacing_;

            set => this.SetProperty(ref this.subTextSpacing_, value);
        }

        /// <summary>スコアテキストのスペーシング を取得、設定</summary>
        private float scoreTextSpacing_;
        [UIValue("score-text-spacing")]
        /// <summary>スコアテキストのスペーシング を取得、設定</summary>
        public float ScoreTextSpacing
        {
            get => this.scoreTextSpacing_;

            set => this.SetProperty(ref this.scoreTextSpacing_, value);
        }

        /// <summary>ランクテキストのスペーシング を取得、設定</summary>
        private float rankTextSpacing_;
        [UIValue("rank-text-spacing")]
        /// <summary>ランクテキストのスペーシング を取得、設定</summary>
        public float RankTextSpacing
        {
            get => this.rankTextSpacing_;

            set => this.SetProperty(ref this.rankTextSpacing_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // Unity Message
#if DEBUG
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) {
                HMMainThreadDispatcher.instance.Enqueue(this.SetCover(this._coverSprite));
            }
        }
#endif
        private IEnumerator Start()
        {
            if (!PluginConfig.Instance.Enable) {
                yield break;
            }
            yield return new WaitWhile(() => this._informationScreen == null || !this._informationScreen);
            this._informationScreen.ShowHandle = false;
#if !DEBUG
            // GameCore中のVRPointerはメニュー画面でのVRpointerと異なるのでもう一度セットしなおす必要がある。
            // その他のMODとの干渉も考える
            if (this._informationScreen.screenMover?.gameObject.GetInstanceID() != this._pointer.gameObject.GetInstanceID()) {
                var mover = this._pointer.gameObject.GetComponent<FloatingScreenMoverPointer>();
                if (mover == null) {
                    mover = this._pointer.gameObject.AddComponent<FloatingScreenMoverPointer>();
                }
                Destroy(this._informationScreen.screenMover);
                this._informationScreen.screenMover = mover;
                this._informationScreen.screenMover.Init(this._informationScreen);
            }
            this._informationScreen.screenMover.enabled = false;
#endif
        }

        protected override void OnDestroy()
        {
            Logger.Debug("OnDestroy call");
            this._scoreController.scoreDidChangeEvent -= this.OnScoreDidChangeEvent;
            this._scoreController.comboDidChangeEvent -= this.OnComboDidChangeEvent;
            this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent -= this.OnRelativeScoreOrImmediateRankDidChangeEvent;
            this._pauseController.didPauseEvent -= this.OnDidPauseEvent;
            this._pauseController.didResumeEvent -= this.OnDidResumeEvent;
            PluginConfig.Instance.OnReloaded -= this.OnReloaded;
            PluginConfig.Instance.OnChenged -= this.OnChenged;
            if (this._informationScreen != null) {
                this._informationScreen.HandleGrabbed -= this.OnHandleGrabbed;
                this._informationScreen.HandleReleased -= this.OnHandleReleased;
                Destroy(this._informationScreen);
            }
            base.OnDestroy();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        /// <summary>
        /// スコアが更新されたときに呼び出されます。
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void OnScoreDidChangeEvent(int arg1, int arg2)
        {
            this.Score = $"{arg2:#,0}";
        }
        /// <summary>
        /// コンボ数が変化したときに呼び出されます。
        /// </summary>
        /// <param name="obj"></param>
        private void OnComboDidChangeEvent(int obj)
        {
            this.UpdateComboText(obj);
        }
        /// <summary>
        /// 精度が変わったときに呼び出されます。
        /// </summary>
        private void OnRelativeScoreOrImmediateRankDidChangeEvent()
        {
            this.UpdateSeidoText();
            this.UpdateRankText();
        }
        /// <summary>
        /// ランク表示を更新します。
        /// </summary>
        private void UpdateRankText()
        {
            this.Rank = RankModel.GetRankName(this._relativeScoreAndImmediateRankCounter.immediateRank);
        }
        /// <summary>
        /// 精度を更新します。
        /// </summary>
        private void UpdateSeidoText()
        {
            this.Seido = $"{this._relativeScoreAndImmediateRankCounter.relativeScore * 100:0.00} %";
        }
        /// <summary>
        /// コンボ数を更新します。
        /// </summary>
        /// <param name="combo"></param>
        private void UpdateComboText(int combo)
        {
            this.Combo = $"{combo} <size=50%>COMBO</size>";
        }
        /// <summary>
        /// カバー画像を更新します。
        /// </summary>
        /// <param name="beatmapCover"></param>
        /// <returns></returns>
        private IEnumerator SetCover(Sprite beatmapCover)
        {
            yield return new WaitWhile(() => this._cover == null || !this._cover);
            this._cover.sprite = beatmapCover;
        }

        private IEnumerator CanvasConfigUpdate()
        {
            yield return new WaitWhile(() => this._informationScreen == null || !this._informationScreen);
            try {
                var coreGameHUDController = Resources.FindObjectsOfTypeAll<CoreGameHUDController>().FirstOrDefault();
                if (coreGameHUDController != null) {
                    var energyGo = coreGameHUDController.GetField<GameObject, CoreGameHUDController>("_energyPanelGO");
                    var energyCanvas = energyGo.GetComponent<Canvas>();
                    foreach (var canvas in this._informationScreen.GetComponentsInChildren<Canvas>()) {
                        canvas.overrideSorting = energyCanvas.overrideSorting;
                        canvas.sortingLayerID = energyCanvas.sortingLayerID;
                        canvas.sortingLayerName = energyCanvas.sortingLayerName;
                        canvas.sortingOrder = energyCanvas.sortingOrder;
                        canvas.gameObject.layer = energyCanvas.gameObject.layer;
                    }
                }
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private void OnDidResumeEvent()
        {
            if (this._informationScreen == null) {
                return;
            }
            this._informationScreen.ShowHandle = false;
            this._informationScreen.screenMover.enabled = false;
        }

        private void OnDidPauseEvent()
        {
            if (PluginConfig.Instance.LockPosition || this._informationScreen == null) {
                return;
            }
            this._informationScreen.ShowHandle = true;
            this._informationScreen.screenMover.enabled = true;
        }

        private void OnChenged(PluginConfig obj)
        {
            this.SetConfigValue(obj);
        }

        private void OnReloaded(PluginConfig obj)
        {
            this.SetConfigValue(obj);
        }

        private void SetConfigValue(PluginConfig p)
        {
            this.CoverVisible = p.CoverVisible;
            this.CoverSize = p.CoverSize;
            this.CoverPivot = p.CoverPivotPos;

            this.TextSpaceHeight = p.TextSpaceHeight;
            this.TextSpaceWidth = 200f - p.CoverSize;

            this.SongNameFontSize = p.SongNameFontSize;
            this.SongSUbNameFontSIze = p.SongSubNameFontSize;
            this.SongAuthorFontsize = p.SongAuthorNameFontSize;

            this.ScoreVisible = p.ScoreVisible;
            this.ScoreFontsize = p.ScoreFontSize;

            this.ComboFontSize = p.ComboFontSize;
            this.ComboVisible = p.ComboVisible;

            this.SeidoFontsize = p.SeidoFontSize;
            this.SeidoVisible = p.SeidoVisible;

            this.RankVisible = p.RankVisible;
            this.RankFontsize = p.RankFontSize;

            this.DifficulityLabelVisible = p.DifficulityLabelVisible;
            this.DifficulityFontSize = p.DifficulityLabelFontSize;

            this.SubTextSpacing = p.SubTextSpacing;
            this.ScoreTextSpacing = p.ScoreTextSpacing;
            this.RankTextSpacing = p.RankTextSpacing;

            if (this._informationScreen == null || !this._informationScreen) {
                return;
            }
            lock (_lockObject) {
                this._informationScreen.transform.position = new Vector3(p.ScreenPosX, p.ScreenPosY, p.ScreenPosZ);
                this._informationScreen.transform.rotation = Quaternion.Euler(p.ScreenRotX, p.ScreenRotY, p.ScreenRotZ);
                if (PluginConfig.Instance.ChangeScale) {
                    this._informationScreen.transform.localScale = Vector3.one * PluginConfig.Instance.ScreenScale;
                }
            }
        }

        private void OnHandleReleased(object sender, FloatingScreenHandleEventArgs e)
        {
            Logger.Debug($"Handle Released");
            lock (_lockObject) {
                PluginConfig.Instance.ScreenPosX = e.Position.x;
                PluginConfig.Instance.ScreenPosY = e.Position.y;
                PluginConfig.Instance.ScreenPosZ = e.Position.z;

                var rot = e.Rotation.eulerAngles;

                PluginConfig.Instance.ScreenRotX = rot.x;
                PluginConfig.Instance.ScreenRotY = rot.y;
                PluginConfig.Instance.ScreenRotZ = rot.z;
            }
        }

        private void OnHandleGrabbed(object sender, FloatingScreenHandleEventArgs e)
        {
            Logger.Debug($"Handle Grabbed");
        }

        /// <summary>
        /// プロパティへ値をセットし、Viewへ通知します
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="property">プロパティ用変数</param>
        /// <param name="value">更新する値</param>
        /// <param name="membername">このメソッドを呼んだメンバーの名前（自動挿入なので指定する必要なし）</param>
        /// <returns>変更があったかどうか</returns>
        private bool SetProperty<T>(ref T property, T value, [CallerMemberName]string membername = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value)) {
                return false;
            }
            property = value;
            this.OnPropertyChanged(membername);
            return true;
        }

        /// <summary>
        /// プロパティの変更があったときに呼び出されます。Viewへの通知をここでキャンセルできます。
        /// </summary>
        /// <param name="propertyName">呼び出されたプロパティ名</param>
        private void OnPropertyChanged(string propertyName)
        {
            HMMainThreadDispatcher.instance?.Enqueue(() =>
            {
                this.NotifyPropertyChanged(propertyName);
            });
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private ScoreController _scoreController;
        private GameplayCoreSceneSetupData _gameplayCoreSceneSetupData;
        private FloatingScreen _informationScreen;
        private RelativeScoreAndImmediateRankCounter _relativeScoreAndImmediateRankCounter;
        private PauseController _pauseController;
        [UIComponent("cover")]
        private ImageView _cover;
        private VRPointer _pointer;
        private Sprite _coverSprite;
        private static readonly object _lockObject = new object();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private async void Constractor(ScoreController scoreController, GameplayCoreSceneSetupData gameplayCoreSceneSetupData, RelativeScoreAndImmediateRankCounter relativeScoreAndImmediateRankCounter, PauseController pauseController, VRInputModule inputModule)
        {
            Logger.Debug("Constractor call");
            try {
                this._scoreController = scoreController;
                this._relativeScoreAndImmediateRankCounter = relativeScoreAndImmediateRankCounter;
                this._gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
                this._pauseController = pauseController;
                this._pointer = inputModule.GetField<VRPointer, VRInputModule>("_vrPointer");
                if (!PluginConfig.Instance.Enable) {
                    return;
                }
                var diff = this._gameplayCoreSceneSetupData.difficultyBeatmap;
                var previewBeatmapLevel = Loader.GetLevelById(diff.level.levelID);
                if (previewBeatmapLevel == null) {
                    Logger.Debug("previewmap is null!");
                    return;
                }
                this._scoreController.scoreDidChangeEvent += this.OnScoreDidChangeEvent;
                this._scoreController.comboDidChangeEvent += this.OnComboDidChangeEvent;
                this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent += this.OnRelativeScoreOrImmediateRankDidChangeEvent;
                this._pauseController.didPauseEvent += this.OnDidPauseEvent;
                this._pauseController.didResumeEvent += this.OnDidResumeEvent;
                this._coverSprite = await previewBeatmapLevel.GetCoverImageAsync(CancellationToken.None);

                this.SetConfigValue(PluginConfig.Instance);
                this._informationScreen = FloatingScreen.CreateFloatingScreen(new Vector2(200f, 120f), true, new Vector3(PluginConfig.Instance.ScreenPosX, PluginConfig.Instance.ScreenPosY, PluginConfig.Instance.ScreenPosZ), Quaternion.Euler(0f, 0f, 0f));
                this._informationScreen.SetRootViewController(this, HMUI.ViewController.AnimationType.None);
                this._informationScreen.transform.rotation = Quaternion.Euler(PluginConfig.Instance.ScreenRotX, PluginConfig.Instance.ScreenRotY, PluginConfig.Instance.ScreenRotZ);
                if (PluginConfig.Instance.ChangeScale) {
                    this._informationScreen.transform.localScale = Vector3.one * PluginConfig.Instance.ScreenScale;
                }
                this._informationScreen.HandleGrabbed += this.OnHandleGrabbed;
                this._informationScreen.HandleReleased += this.OnHandleReleased;
                HMMainThreadDispatcher.instance.Enqueue(this.CanvasConfigUpdate());
                HMMainThreadDispatcher.instance.Enqueue(this.SetCover(this._coverSprite));
                this.SongName = previewBeatmapLevel.songName;
                this.SongSubName = previewBeatmapLevel.songSubName;
                this.SongAuthor = previewBeatmapLevel.songAuthorName;
                this.Difficulity = diff.difficulty.ToString();
                this.UpdateComboText(0);
                PluginConfig.Instance.OnReloaded += this.OnReloaded;
                PluginConfig.Instance.OnChenged += this.OnChenged;
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
    }
}
