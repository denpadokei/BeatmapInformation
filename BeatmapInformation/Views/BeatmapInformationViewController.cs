using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Configuration;
using BeatSaberMarkupLanguage.Attributes;
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
using TMPro;
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

        /// <summary>曲の時間テキスト を取得、設定</summary>
        private string songtimeText_;
        [UIValue("songtime-text")]
        /// <summary>曲の時間テキスト を取得、設定</summary>
        public string SongtimeText
        {
            get => this.songtimeText_;

            set => this.SetProperty(ref this.songtimeText_, value);
        }

        /// <summary>曲時間テキストのフォントサイズ を取得、設定</summary>
        private float songtimeTextFontSize_;
        [UIValue("songtime-fontsize")]
        /// <summary>曲時間テキストのフォントサイズ を取得、設定</summary>
        public float SongtimeTextFontSize
        {
            get => this.songtimeTextFontSize_;

            set => this.SetProperty(ref this.songtimeTextFontSize_, value);
        }

        /// <summary>曲時間の表示 を取得、設定</summary>
        private bool songtimeVisible_;
        [UIValue("songtime-visible")]
        /// <summary>曲時間の表示 を取得、設定</summary>
        public bool SongtimeVisible
        {
            get => this.songtimeVisible_;

            set => this.SetProperty(ref this.songtimeVisible_, value);
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
        private float songNameFontSize_;
        [UIValue("song-name-fontsize")]
        /// <summary>曲名のフォントサイズ を取得、設定</summary>
        public float SongNameFontSize
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
        private float songSubNameFontSize_;
        [UIValue("song-sub-name-fontsize")]
        /// <summary>サブタイトルフォントサイズ を取得、設定</summary>
        public float SongSUbNameFontSIze
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
        private float songAuthorFontsize_;
        [UIValue("song-author-fontsize")]
        /// <summary>曲作者フォントサイズ を取得、設定</summary>
        public float SongAuthorFontsize
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
        private float comboFontSize_;
        [UIValue("combo-fontsize")]
        /// <summary>コンボ数フォントサイズ を取得、設定</summary>
        public float ComboFontSize
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
        private float scoreFontsize_;
        [UIValue("score-fontsize")]
        /// <summary>スコアフォントサイズ を取得、設定</summary>
        public float ScoreFontsize
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
        private float rankFontsize_;
        [UIValue("rank-fontsize")]
        /// <summary>ランクフォントサイズ を取得、設定</summary>
        public float RankFontsize
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
        private float seidoFontsize_;
        [UIValue("seido-fontsize")]
        /// <summary>精度フォントサイズ を取得、設定</summary>
        public float SeidoFontsize
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

        /// <summary>波形表示 を取得、設定</summary>
        private bool audioSpectromVisible_;
        [UIValue("audiospectrom-visible")]
        /// <summary>波形表示 を取得、設定</summary>
        public bool AudioSpectromVisible
        {
            get => this.audioSpectromVisible_;

            set => this.SetProperty(ref this.audioSpectromVisible_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // Unity Message
        private void Update()
        {
            // デバッグ中はBSML再読み込みを頻繁に行うのでカバー画像再読み込みしないと不便だけどリリースするときには消して何ら問題ない
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Q)) {
                HMMainThreadDispatcher.instance.Enqueue(this.SetCover(this._coverSprite));
            }
#endif
            this.UpdateSongTime();
            this.UpdateAudioSpectroms();
        }


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
        private void OnScoreDidChangeEvent(int arg1, int arg2) => this.Score = $"{arg2:#,0}";
        /// <summary>
        /// コンボ数が変化したときに呼び出されます。
        /// </summary>
        /// <param name="obj"></param>
        private void OnComboDidChangeEvent(int obj) => this.UpdateComboText(obj);
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
        private void UpdateRankText() => this.Rank = RankModel.GetRankName(this._relativeScoreAndImmediateRankCounter.immediateRank);
        /// <summary>
        /// 精度を更新します。
        /// </summary>
        private void UpdateSeidoText() => this.Seido = $"{this._relativeScoreAndImmediateRankCounter.relativeScore * 100:0.00} %";
        /// <summary>
        /// コンボ数を更新します。
        /// </summary>
        /// <param name="combo"></param>
        private void UpdateComboText(int combo) => this.Combo = $"{combo} <size=50%>COMBO</size>";
        /// <summary>
        /// カバー画像を更新します。
        /// </summary>
        /// <param name="beatmapCover"></param>
        /// <returns></returns>
        private IEnumerator SetCover(Sprite beatmapCover)
        {
            yield return new WaitWhile(() => this._cover == null || !this._cover);
            this._cover.sprite = beatmapCover;
            if (PluginConfig.Instance.SongTimerVisible) {
                this._cover.color = new Color(this._cover.color.r, this._cover.color.g, this._cover.color.b, PluginConfig.Instance.CoverAlpha);
            }
            this.CreateTimeCanvas();
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

        private void CreateTimeCanvas()
        {
            this._songtimeRing.sprite = Resources.FindObjectsOfTypeAll<Sprite>().FirstOrDefault(x => x.name == "Circle");
            this._songtimeRing.type = Image.Type.Filled;
            this._songtimeRing.fillClockwise = true;
            this._songtimeRing.fillOrigin = 2;
            this._songtimeRing.fillAmount = 1;
            this._songtimeRing.fillMethod = Image.FillMethod.Radial360;
            this._songtimeRing.transform.localScale = Vector3.one * PluginConfig.Instance.SontTimeRingScale;
        }

        private void UpdateSongTime()
        {
            if (!PluginConfig.Instance.SongTimerVisible) {
                return;
            }
            if (this._songtimeRing == null) {
                return;
            }
            var time = this._audioTimeSyncController.songTime;
            if (time <= 0f) return;
            this.SongtimeText = $"{time.Minutes()}:{time.Seconds():00}";
            this._songtimeRing.fillAmount = Mathf.Floor(time) / this._songLength;
        }

        private void CreateSpctromImages()
        {
            var spectromImageGO = Instantiate(this.baseAudioSpectumImage.gameObject);
            spectromImageGO.SetActive(true);
            var spectromImage = spectromImageGO.GetComponent<ImageView>();
            spectromImage.enabled = false;
            spectromImage.rectTransform.sizeDelta = new Vector2(10f, 100f);
            spectromImage.type = Image.Type.Filled;
            spectromImage.fillMethod = Image.FillMethod.Vertical;
            spectromImage.sprite = Sprite.Create(new Texture2D(10, 100), new Rect(0, 0, 10, 100), Vector2.one / 2);
            spectromImage.color = new Color(spectromImage.color.r, spectromImage.color.g, spectromImage.color.b, PluginConfig.Instance.AudioSpectrumAlpha);
            spectromImage.fillAmount = 1f;
            var images = new List<ImageView>();
            foreach (var item in this._audioSpectrum.MeanLevels.Select((x, y) => y)) {
                var copyImage = Instantiate(spectromImage, this.baseAudioSpectumImage.gameObject.transform.parent, false);
                copyImage.enabled = true;
                images.Add(copyImage);
            }
            this._spectroms = images.ToArray();
        }

        private void UpdateAudioSpectroms()
        {
            if (this._spectroms == null) {
                return;
            }
            foreach (var image in this._spectroms.Select((x, y) => (x, y))) {
                var value = Mathf.Max(0f, this._audioSpectrum.MeanLevels[image.y]) * 7;
                image.x.fillAmount = value >= 1f ? 1f : value;
            }
        }

        private void UpdateSpectumAlpha()
        {
            foreach (var image in this._spectroms) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, PluginConfig.Instance.AudioSpectrumAlpha);
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

        [UIAction("#post-parse")]
        private void PostParse()
        {
            this.CreateSpctromImages();
        }

        private void OnChenged(PluginConfig obj) => this.SetConfigValue(obj);

        private void OnReloaded(PluginConfig obj) => this.SetConfigValue(obj);

        private void SetConfigValue(PluginConfig p)
        {
            Logger.Debug("Update Config");

            this.CoverVisible = p.CoverVisible;
            this.CoverSize = p.CoverSize;
            this.CoverPivot = p.CoverPivotPos;
            this.SongtimeVisible = p.SongTimerVisible;
            if (this._songtimeRing) {
                this._songtimeRing.transform.localScale = Vector3.one * PluginConfig.Instance.SontTimeRingScale;
                this._cover.color = new Color(this._cover.color.r, this._cover.color.g, this._cover.color.b, PluginConfig.Instance.CoverAlpha);
            }
            this.SongtimeTextFontSize = p.SongTimeTextFontSize;

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

            this.AudioSpectromVisible = p.AudioSpectrumVisible;

            if (this._informationScreen == null || !this._informationScreen) {
                return;
            }
            lock (_lockObject) {
                this._informationScreen.transform.position = new Vector3(p.ScreenPosX, p.ScreenPosY, p.ScreenPosZ);
                this._informationScreen.transform.rotation = Quaternion.Euler(p.ScreenRotX, p.ScreenRotY, p.ScreenRotZ);
                this.UpdateSpectumAlpha();
                var canvas = this._informationScreen.gameObject.GetComponentInChildren<Canvas>();
                var setting = this._curvedCanvasSettingsHelper.GetCurvedCanvasSettings(canvas);
                setting?.SetRadius(p.ScreenRadius);
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

        private void OnHandleGrabbed(object sender, FloatingScreenHandleEventArgs e) => Logger.Debug($"Handle Grabbed");

        /// <summary>
        /// プロパティへ値をセットし、Viewへ通知します
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="property">プロパティ用変数</param>
        /// <param name="value">更新する値</param>
        /// <param name="membername">このメソッドを呼んだメンバーの名前（自動挿入なので指定する必要なし）</param>
        /// <returns>変更があったかどうか</returns>
        private bool SetProperty<T>(ref T property, T value, [CallerMemberName] string membername = null)
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
        private void OnPropertyChanged(string propertyName) => HMMainThreadDispatcher.instance?.Enqueue(() =>
                                                             {
                                                                 this.NotifyPropertyChanged(propertyName);
                                                             });
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private ScoreController _scoreController;
        private AudioTimeSyncController _audioTimeSyncController;
        private GameplayCoreSceneSetupData _gameplayCoreSceneSetupData;
        private FloatingScreen _informationScreen;
        private RelativeScoreAndImmediateRankCounter _relativeScoreAndImmediateRankCounter;
        private PauseController _pauseController;
        [UIComponent("cover")]
        private readonly ImageView _cover;
        [UIComponent("songtime-ring")]
        private readonly ImageView _songtimeRing;
        private VRPointer _pointer;
        private Sprite _coverSprite;
        private AudioSpectrum _audioSpectrum;
        private ImageView[] _spectroms;
        [UIComponent("audio-spectrom-group")]
        private readonly HorizontalLayoutGroup audioSpectromGroup;
        [UIComponent("audio-spetrum")]
        private ImageView baseAudioSpectumImage;
        private CurvedCanvasSettingsHelper _curvedCanvasSettingsHelper;
        private float _songLength;
        private static readonly object _lockObject = new object();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private async void Constractor(ScoreController scoreController, GameplayCoreSceneSetupData gameplayCoreSceneSetupData, RelativeScoreAndImmediateRankCounter relativeScoreAndImmediateRankCounter, PauseController pauseController, VRInputModule inputModule, AudioTimeSyncController audioTimeSyncController, AudioSpectrum audioSpectrum)
        {
            Logger.Debug("Constractor call");
            try {
                this._scoreController = scoreController;
                this._relativeScoreAndImmediateRankCounter = relativeScoreAndImmediateRankCounter;
                this._audioTimeSyncController = audioTimeSyncController;
                this._gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
                this._pauseController = pauseController;
                this._audioSpectrum = audioSpectrum;
                this._pointer = inputModule.GetField<VRPointer, VRInputModule>("_vrPointer");
                this._curvedCanvasSettingsHelper = new CurvedCanvasSettingsHelper();
                if (!PluginConfig.Instance.Enable) {
                    return;
                }
                var diff = this._gameplayCoreSceneSetupData.difficultyBeatmap;
                var previewBeatmapLevel = Loader.GetLevelById(diff.level.levelID);
                if (previewBeatmapLevel == null) {
                    Logger.Debug("previewmap is null!");
                    return;
                }
                this._songLength = Mathf.Floor(this._audioTimeSyncController.songLength);
                this._scoreController.scoreDidChangeEvent += this.OnScoreDidChangeEvent;
                this._scoreController.comboDidChangeEvent += this.OnComboDidChangeEvent;
                this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent += this.OnRelativeScoreOrImmediateRankDidChangeEvent;
                this._pauseController.didPauseEvent += this.OnDidPauseEvent;
                this._pauseController.didResumeEvent += this.OnDidResumeEvent;
                this._coverSprite = await previewBeatmapLevel.GetCoverImageAsync(CancellationToken.None);

                this.SetConfigValue(PluginConfig.Instance);
                this._informationScreen = FloatingScreen.CreateFloatingScreen(new Vector2(200f, 120f), true, new Vector3(PluginConfig.Instance.ScreenPosX, PluginConfig.Instance.ScreenPosY, PluginConfig.Instance.ScreenPosZ), Quaternion.Euler(0f, 0f, 0f), PluginConfig.Instance.ScreenRadius);
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
