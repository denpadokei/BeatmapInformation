using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Models;
using BeatmapInformation.SimpleJsons;
using BeatmapInformation.WebClients;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SongCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
        public float SongSubNameFontSIze
        {
            get => this.songSubNameFontSize_;

            set => this.SetProperty(ref this.songSubNameFontSize_, value);
        }

        /// <summary>曲のキー を取得、設定</summary>
        private string songKey_;
        [UIValue("song-key")]
        /// <summary>曲のキー を取得、設定</summary>
        public string SongKey
        {
            get => this.songKey_;

            set => this.SetProperty(ref this.songKey_, value);
        }

        /// <summary>曲のキーのフォントサイズ を取得、設定</summary>
        private float songKeyFontSize_;
        [UIValue("song-key-font-size")]
        /// <summary>曲のキーのフォントサイズ を取得、設定</summary>
        public float SongKeyFontSize
        {
            get => this.songKeyFontSize_;

            set => this.SetProperty(ref this.songKeyFontSize_, value);
        }

        /// <summary>曲のキーを表示するかどうか を取得、設定</summary>
        private bool songKeyActive_;
        [UIValue("song-key-visible")]
        /// <summary>曲のキーを表示するかどうか を取得、設定</summary>
        public bool SongKeyActive
        {
            get => this.songKeyActive_;

            set => this.SetProperty(ref this.songKeyActive_, value);
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
        private float difficulityFontsize_;
        [UIValue("difficulity-fontsize")]
        /// <summary>難易度フォントサイズ を取得、設定</summary>
        public float DifficulityFontSize
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

        /// <summary>波形表示のアンカーMinX を取得、設定</summary>
        private float bgAncherMinX_;
        [UIValue("bg-ancher-min-x")]
        /// <summary>波形表示のアンカーMinX を取得、設定</summary>
        public float BGAncherMinX
        {
            get => this.bgAncherMinX_;

            set => this.SetProperty(ref this.bgAncherMinX_, value);
        }

        /// <summary>波形表示アンカーMaxX を取得、設定</summary>
        private float _bgAncherMaxX;
        [UIValue("bg-ancher-max-x")]
        /// <summary>波形表示アンカーMaxX を取得、設定</summary>
        public float BGAncherMaxX
        {
            get => this._bgAncherMaxX;

            set => this.SetProperty(ref this._bgAncherMaxX, value);
        }

        /// <summary>波形表示アンカーMinY を取得、設定</summary>
        private float _bgAncherMinY;
        [UIValue("bg-ancher-min-y")]
        /// <summary>波形表示アンカーMinY を取得、設定</summary>
        public float BGAncherMinY
        {
            get => this._bgAncherMinY;

            set => this.SetProperty(ref this._bgAncherMinY, value);
        }

        /// <summary>波形表示アンカーMaxY を取得、設定</summary>
        private float _bgAncherMaxY;
        [UIValue("bg-ancher-max-y")]
        /// <summary>波形表示アンカーMaxY を取得、設定</summary>
        public float BGAncherMaxY
        {
            get => this._bgAncherMaxY;

            set => this.SetProperty(ref this._bgAncherMaxY, value);
        }

        /// <summary>テキスト表示アンカーMinX を取得、設定</summary>
        private float _textAncherMinX;
        [UIValue("text-ancher-min-x")]
        /// <summary>テキスト表示アンカーMinX を取得、設定</summary>
        public float TextAncherMinX
        {
            get => this._textAncherMinX;

            set => this.SetProperty(ref this._textAncherMinX, value);
        }

        /// <summary>テキスト表示アンカーMaxX を取得、設定</summary>
        private float _textAncherMaxX;
        [UIValue("text-ancher-max-x")]
        /// <summary>テキスト表示アンカーMaxX を取得、設定</summary>
        public float TextAncherMaxX
        {
            get => this._textAncherMaxX;

            set => this.SetProperty(ref this._textAncherMaxX, value);
        }

        /// <summary>テキスト表示アンカーMinY を取得、設定</summary>
        private float _textAncherMinY;
        [UIValue("text-ancher-min-y")]
        /// <summary>テキスト表示アンカーMinY を取得、設定</summary>
        public float TextAncherMinY
        {
            get => this._textAncherMinY;

            set => this.SetProperty(ref this._textAncherMinY, value);
        }

        /// <summary>テキスト表示アンカーMaxY を取得、設定</summary>
        private float _textAncherMaxY;
        [UIValue("text-ancher-max-y")]
        /// <summary>テキスト表示アンカーMaxY を取得、設定</summary>
        public float TextAncherMaxY
        {
            get => this._textAncherMaxY;

            set => this.SetProperty(ref this._textAncherMaxY, value);
        }
        public FloatingScreen InformationScreen { get; set; }
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
        }
        private IEnumerator Start()
        {
            yield return new WaitWhile(() => !this._initialized || this.InformationScreen == null || !this.InformationScreen);
            this.InformationScreen.ShowHandle = false;
#if !DEBUG
            // GameCore中のVRPointerはメニュー画面でのVRpointerと異なるのでもう一度セットしなおす必要がある。
            // その他のMODとの干渉も考える
            if (this.InformationScreen.screenMover?.gameObject.GetInstanceID() != this._pointer.gameObject.GetInstanceID()) {
                var mover = this._pointer.gameObject.GetComponent<FloatingScreenMoverPointer>();
                if (mover == null) {
                    mover = this._pointer.gameObject.AddComponent<FloatingScreenMoverPointer>();
                }
                Destroy(this.InformationScreen.screenMover);
                this.InformationScreen.screenMover = mover;
                this.InformationScreen.screenMover.Init(this.InformationScreen);
            }
            this.InformationScreen.screenMover.enabled = false;
#endif
        }

        protected override void OnDestroy()
        {
            Logger.Debug("OnDestroy call");
            this._scoreController.scoreDidChangeEvent -= this.OnScoreDidChangeEvent;
            this._comboController.comboDidChangeEvent -= this.OnComboDidChangeEvent;
            this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent -= this.OnRelativeScoreOrImmediateRankDidChangeEvent;
            if (this._pauseController != null) {
                this._pauseController.didPauseEvent -= this.OnDidPauseEvent;
                this._pauseController.didResumeEvent -= this.OnDidResumeEvent;
            }
            this._audioSpectrum.UpdatedRawSpectums -= this.OnUpdatedRawSpectums;
            if (this._profile != null) {
                this._profile.PropertyChanged -= this.OnProfile_PropertyChanged;
            }
            if (this.InformationScreen != null) {
                this.InformationScreen.HandleGrabbed -= this.OnHandleGrabbed;
                this.InformationScreen.HandleReleased -= this.OnHandleReleased;
                Destroy(this.InformationScreen.gameObject);
            }
            base.OnDestroy();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void ResetView()
        {
            this.UpdateAllText(0, 0, 1, RankModel.Rank.SSS);
        }

        public async Task InitializeAsync(CancellationToken token, ProfileEntity entity)
        {
            if (this._profile != null) {
                this._profile.PropertyChanged -= this.OnProfile_PropertyChanged;
            }
            this._profile = entity;
            this._profile.PropertyChanged += this.OnProfile_PropertyChanged;
            this._audioSpectrum.UpdatedRawSpectums += this.OnUpdatedRawSpectums;
            this._scoreController.scoreDidChangeEvent += this.OnScoreDidChangeEvent;
            this._comboController.comboDidChangeEvent += this.OnComboDidChangeEvent;
            this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent += this.OnRelativeScoreOrImmediateRankDidChangeEvent;
            if (this._pauseController != null) {
                this._pauseController.didPauseEvent += this.OnDidPauseEvent;
                this._pauseController.didResumeEvent += this.OnDidResumeEvent;
            }

            var previewBeatmapLevel = Loader.GetLevelById(this._difficultyBeatmap.level.levelID);
            if (previewBeatmapLevel == null) {
                Logger.Debug("previewmap is null!");
                return;
            }
            this._coverSprite = await previewBeatmapLevel.GetCoverImageAsync(token);
            HMMainThreadDispatcher.instance.Enqueue(this.SetCover(this._coverSprite));

            HMMainThreadDispatcher.instance.Enqueue(this.InitializeCorutinen());
            var hash = previewBeatmapLevel.levelID.Split('_').LastOrDefault();
            var beatmap = await WebClient.GetAsync($"https://api.beatsaver.com/maps/hash/{hash.ToLower()}", token);
            if (!string.IsNullOrEmpty(beatmap?.ContentToString())) {
                var json = JSON.Parse(beatmap.ContentToString());
                this._textFormatter.SongKey = json["id"];
            }
            this._initialized = true;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        /// <summary>
        /// コンボ数が変化したときに呼び出されます。
        /// </summary>
        /// <param name="obj"></param>
        private void OnComboDidChangeEvent(int obj)
        {
            this.UpdateAllText(-1, obj);
        }

        /// <summary>
        /// スコアが変化したときに呼び出されます。
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void OnScoreDidChangeEvent(int arg1, int arg2)
        {
            this.UpdateAllText(arg2);
        }

        /// <summary>
        /// 精度が変わったときに呼び出されます。
        /// </summary>
        private void OnRelativeScoreOrImmediateRankDidChangeEvent()
        {
            this.UpdateAllText(-1, -1, this._relativeScoreAndImmediateRankCounter.relativeScore, this._relativeScoreAndImmediateRankCounter.immediateRank);
        }

        private void UpdateAllText(int score = -1, int combo = -1, float seido = -1, RankModel.Rank rank = RankModel.Rank.SSS)
        {

            if (0 <= score && this._score < score) {
                this._score = score;
            }
            if (0 <= combo && this._combo != combo) {
                this._combo = combo;
            }
            if (0 <= seido && this._seido != seido) {
                this._seido = seido;
            }
            if (rank != RankModel.Rank.SSS && this._rank != rank) {
                this._rank = rank;
            }
            var entity = this._scoreContainer.Spawn();
            entity.Set(this._score, this._combo, this._seido, this._rank);
            HMMainThreadDispatcher.instance.Enqueue(() =>
            {
                if (this._isUpdateSongName) {
                    this.SongName = this._textFormatter.Convert(this._profile?.SongNameFormat, entity);
                }
                if (this._isUpdateSongSubName) {
                    this.SongSubName = this._textFormatter.Convert(this._profile?.SongSubNameFormat, entity);
                }
                if (this._isUpdateSongKey) {
                    this.SongKey = this._textFormatter.Convert(this._profile?.SongKeyFormat, entity);
                }
                if (this._isUpdateSongAuthorName) {
                    this.SongAuthor = this._textFormatter.Convert(this._profile?.SongAuthorNameFormat, entity);
                }
                if (this._isUpdateDifficurity) {
                    this.Difficulity = this._textFormatter.Convert(this._profile?.DifficurityFormat, entity);
                }
                if (this._isUpdateCombo) {
                    this.Combo = this._textFormatter.Convert(this._profile?.ComboFormat, entity);
                }
                if (this._isUpdateScore) {
                    this.Score = this._textFormatter.Convert(this._profile?.ScoreFormat, entity);
                }
                if (this._isUpdateRank) {
                    this.Rank = this._textFormatter.Convert(this._profile?.RankFormat, entity);
                }
                if (this._isUpdateSeido) {
                    this.Seido = this._textFormatter.Convert(this._profile?.AccFormat, entity);
                }
                this._scoreContainer.Despawn(entity);
            });
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
            if (this._profile != null && this._profile.SongTimerVisible) {
                this._cover.color = new Color(this._cover.color.r, this._cover.color.g, this._cover.color.b, this._profile.CoverAlpha);
            }
            this.CreateTimeCanvas();
        }

        private IEnumerator CanvasConfigUpdate()
        {
            yield return new WaitWhile(() => !this._initialized || this.InformationScreen == null || !this.InformationScreen);
            try {
                var coreGameHUDController = Resources.FindObjectsOfTypeAll<CoreGameHUDController>().FirstOrDefault();
                if (coreGameHUDController != null) {
                    var energyGo = coreGameHUDController.GetField<GameObject, CoreGameHUDController>("_energyPanelGO");
                    var energyCanvas = energyGo.GetComponent<Canvas>();
                    foreach (var canvas in this.InformationScreen.GetComponentsInChildren<Canvas>()) {
                        canvas.worldCamera = Camera.main;
                        canvas.overrideSorting = energyCanvas.overrideSorting;
                        canvas.sortingLayerID = energyCanvas.sortingLayerID;
                        canvas.sortingLayerName = energyCanvas.sortingLayerName;
                        this.SortinglayerOrder = energyCanvas.sortingOrder;
                        canvas.sortingOrder = this.SortinglayerOrder;
                        canvas.gameObject.layer = this._profile?.ScreenLayer ?? 5;
                    }
                    foreach (var graphic in this.InformationScreen.GetComponentsInChildren<Graphic>()) {
                        graphic.raycastTarget = false;
                    }
                    try {
                        Destroy(this.InformationScreen.GetComponent<VRGraphicRaycaster>());
                    }
                    catch (Exception e) {
                        Logger.Error(e);
                    }
                }
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private void UpdateScreenLayer(int layer)
        {
            if (!this.InformationScreen) {
                return;
            }
            foreach (var canvas in this.InformationScreen.GetComponentsInChildren<Canvas>()) {
                canvas.gameObject.layer = layer;
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
            this._songtimeRing.transform.localScale = Vector3.one * (this._profile?.SongTimeRingScale ?? 1.7f);
        }

        private void UpdateSongTime()
        {
            if (this._profile?.SongTimerVisible != true) {
                return;
            }
            if (this._songtimeRing == null) {
                return;
            }
            var time = this._audioTimeSyncController.songTime;
            this.SongtimeText = $"{time.Minutes()}:{time.Seconds():00}";
            this._songtimeRing.fillAmount = (time <= 0f || this._songLength == 0) ? 1 : Mathf.Floor(time) / this._songLength;
        }

        private void OnUpdatedRawSpectums(AudioSpectrum obj)
        {
            this.UpdateAudioSpectroms(obj);
        }

        private void CreateSpctromImages()
        {
            this.baseAudioSpectumImage.raycastTarget = false;
            var spectromImageGO = Instantiate(this.baseAudioSpectumImage.gameObject);
            spectromImageGO.SetActive(true);
            var spectromImage = spectromImageGO.GetComponent<ImageView>();
            spectromImage.enabled = false;
            spectromImage.rectTransform.sizeDelta = new Vector2(10f, 100f);
            spectromImage.type = Image.Type.Filled;
            spectromImage.fillMethod = Image.FillMethod.Vertical;
            spectromImage.sprite = Sprite.Create(new Texture2D(10, 100), new Rect(0, 0, 10, 100), Vector2.one / 2);
            spectromImage.color = new Color(spectromImage.color.r, spectromImage.color.g, spectromImage.color.b, (this._profile?.AudioSpectrumAlpha ?? 0.8f));
            spectromImage.fillAmount = 1f;
            var images = new List<ImageView>();
            foreach (var item in this._audioSpectrum.MeanLevels.Select((x, y) => y)) {
                var copyImage = Instantiate(spectromImage, this.baseAudioSpectumImage.gameObject.transform.parent, false);
                copyImage.enabled = true;
                images.Add(copyImage);
            }
            this._spectroms = images.ToArray();
        }

        private void RebuildAudioSpectroms()
        {
            foreach (var image in this._spectroms) {
                if (image) {
                    Destroy(image.gameObject);
                }
                this._spectroms = Array.Empty<ImageView>();
            }
            this.CreateSpctromImages();
        }

        private void UpdateAudioSpectroms(AudioSpectrum audio)
        {
            if (this._spectroms == null || !audio) {
                return;
            }
            foreach (var image in this._spectroms.Select((x, y) => (x, y))) {
                var value = Mathf.Max(0f, audio.MeanLevels[image.y]) * 7;
                image.x.fillAmount = value >= 1f ? 1f : value;
            }
        }

        private void UpdateSpectumAlpha()
        {
            foreach (var image in this._spectroms) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, (this._profile?.AudioSpectrumAlpha ?? 0.8f));
            }
        }

        private void OnDidResumeEvent()
        {
            if (this.InformationScreen == null) {
                return;
            }
            this.InformationScreen.ShowHandle = false;
            this.InformationScreen.screenMover.enabled = false;
            foreach (var canvas in this.InformationScreen.GetComponentsInChildren<Canvas>()) {
                canvas.sortingOrder = this.SortinglayerOrder;
            }
        }

        private void OnDidPauseEvent()
        {
            if (this.InformationScreen == null) {
                return;
            }
            foreach (var canvas in this.InformationScreen.GetComponentsInChildren<Canvas>()) {
                canvas.sortingOrder = UI_SORTING_ORDER;
            }
            if (this._profile?.LockPosition == true) {
                return;
            }
            this.InformationScreen.ShowHandle = true;
            this.InformationScreen.screenMover.enabled = true;
        }

        [UIAction("#post-parse")]
        private void PostParse()
        {
            HMMainThreadDispatcher.instance.Enqueue(this.CanvasConfigUpdate());
            this.RebuildAudioSpectroms();

            this.ResetView();
        }
        private void OnChanged(ProfileEntity obj)
        {
            this.SetConfigValue(obj);
        }

        private void SetConfigValue(ProfileEntity p)
        {
            Logger.Debug("Update Config");

            this.CoverVisible = p.CoverVisible;
            this.CoverSize = p.CoverSize;
            this.CoverPivot = p.CoverPivotPos;
            this.SongtimeVisible = p.SongTimerVisible;
            if (this._songtimeRing) {
                this._songtimeRing.transform.localScale = Vector3.one * p.SongTimeRingScale;
                this._cover.color = new Color(this._cover.color.r, this._cover.color.g, this._cover.color.b, p.CoverAlpha);
            }
            this.SongtimeTextFontSize = p.SongTimeTextFontSize;

            this.TextSpaceHeight = p.TextSpaceHeight;
            this.TextSpaceWidth = 200f - p.CoverSize;

            this.SongNameFontSize = p.SongNameFontSize;
            this.SongSubNameFontSIze = p.SongSubNameFontSize;
            this.SongAuthorFontsize = p.SongAuthorNameFontSize;

            this.SongKeyActive = p.SongKeyVisible;
            this.SongKeyFontSize = p.SongKeyFontSize;

            this.ScoreVisible = p.ScoreVisible;
            this.ScoreFontsize = p.ScoreFontSize;

            this.ComboFontSize = p.ComboFontSize;
            this.ComboVisible = p.ComboVisible;

            this.SeidoFontsize = p.AccFontSize;
            this.SeidoVisible = p.AccVisible;

            this.RankVisible = p.RankVisible;
            this.RankFontsize = p.RankFontSize;

            this.DifficulityLabelVisible = p.DifficulityLabelVisible;
            this.DifficulityFontSize = p.DifficulityLabelFontSize;

            this.SubTextSpacing = p.SubTextSpacing;
            this.ScoreTextSpacing = p.ScoreTextSpacing;
            this.RankTextSpacing = p.RankTextSpacing;

            this.AudioSpectromVisible = p.AudioSpectrumVisible;

            this.BGAncherMinX = p.AncherMinX;
            this.BGAncherMaxX = p.AncherMaxX;
            this.BGAncherMinY = p.AncherMinY;
            this.BGAncherMaxY = p.AncherMaxY;

            this.TextAncherMinX = p.AncherMinX;
            this.TextAncherMaxX = p.AncherMaxX;
            this.TextAncherMinY = p.AncherMinY;
            this.TextAncherMaxY = p.AncherMaxY;

            HMMainThreadDispatcher.instance.Enqueue((Action)(() =>
            {
                this._audioSpectrum.Band = p.BandType;
                if (this.InformationScreen == null || !this.InformationScreen) {
                    return;
                }
                lock (_lockObject) {
                    this.InformationScreen.transform.position = new Vector3(p.ScreenPosX, p.ScreenPosY, p.ScreenPosZ);
                    this.InformationScreen.transform.rotation = Quaternion.Euler(p.ScreenRotX, p.ScreenRotY, p.ScreenRotZ);
                    this.UpdateScreenLayer(p.ScreenLayer);
                    this.RebuildAudioSpectroms();
                    this.UpdateSpectumAlpha();
                    var canvas2 = this.InformationScreen.gameObject.GetComponentInChildren<Canvas>();
                    var setting = this._curvedCanvasSettingsHelper.GetCurvedCanvasSettings(canvas2);
                    setting?.SetRadius(p.ScreenRadius);
                    if (!p.OverlayMode && p.ChangeScale) {
                        this.InformationScreen.transform.localScale = Vector3.one * p.ScreenScale;
                    }
                }

                this._isUpdateSongName = true;
                this._isUpdateSongSubName = true;
                this._isUpdateSongKey = true;
                this._isUpdateSongAuthorName = true;
                this._isUpdateDifficurity = true;
                this._isUpdateScore = true;
                this._isUpdateCombo = true;
                this._isUpdateSeido = true;
                this._isUpdateRank = true;
                this.ResetView();
                this._isUpdateSongName = this.CheckUpdateTarget(p.SongNameFormat);
                this._isUpdateSongSubName = this.CheckUpdateTarget(p.SongSubNameFormat);
                this._isUpdateSongKey = this.CheckUpdateTarget(p.SongKeyFormat);
                this._isUpdateSongAuthorName = this.CheckUpdateTarget(p.SongAuthorNameFormat);
                this._isUpdateDifficurity = this.CheckUpdateTarget(p.DifficurityFormat);
                this._isUpdateScore = this.CheckUpdateTarget(p.ScoreFormat);
                this._isUpdateCombo = this.CheckUpdateTarget(p.ComboFormat);
                this._isUpdateSeido = this.CheckUpdateTarget(p.AccFormat);
                this._isUpdateRank = this.CheckUpdateTarget(p.RankFormat);
            }));
        }

        private void OnHandleReleased(object sender, FloatingScreenHandleEventArgs e)
        {
            Logger.Debug($"Handle Released");
            if (this._profile == null) {
                return;
            }
            lock (_lockObject) {
                this._profile.ScreenPosX = e.Position.x;
                this._profile.ScreenPosY = e.Position.y;
                this._profile.ScreenPosZ = e.Position.z;

                var rot = e.Rotation.eulerAngles;

                this._profile.ScreenRotX = rot.x;
                this._profile.ScreenRotY = rot.y;
                this._profile.ScreenRotZ = rot.z;
            }
        }
        private void OnHandleGrabbed(object sender, FloatingScreenHandleEventArgs e)
        {
            Logger.Debug($"Handle Grabbed");
        }

        private bool CheckUpdateTarget(string format)
        {
            return (format.Contains(TextFormatter.SCORE)
|| format.Contains(TextFormatter.COMBO)
|| format.Contains(TextFormatter.ACCURACY)
|| format.Contains(TextFormatter.RANK));
        }

        private IEnumerator InitializeCorutinen()
        {
            var wait = new WaitWhile(() => !this._initialized || this._profile == null || this.InformationScreen == null);
            yield return wait;
            lock (_lockObject) {
                this.SetConfigValue(this._profile);
                this.InformationScreen.transform.rotation = Quaternion.Euler(this._profile.ScreenRotX, this._profile.ScreenRotY, this._profile.ScreenRotZ);
                if (this._profile.ChangeScale) {
                    this.InformationScreen.transform.localScale = Vector3.one * this._profile.ScreenScale;
                }
                this.InformationScreen.transform.position = new Vector3(this._profile.ScreenPosX, this._profile.ScreenPosY, this._profile.ScreenPosZ);
                this.InformationScreen.transform.rotation = Quaternion.Euler(this._profile.ScreenRotX, this._profile.ScreenRotY, this._profile.ScreenRotZ);
            }
        }

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
        private void OnPropertyChanged(string propertyName)
        {
            HMMainThreadDispatcher.instance?.Enqueue(() =>
            {
                this.NotifyPropertyChanged(propertyName);
            });
        }

        private void OnProfile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProfileEntity entity) {
                this.OnChanged(entity);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private IScoreController _scoreController;
        private IComboController _comboController;
        private IAudioTimeSource _audioTimeSyncController;
        private GameplayCoreSceneSetupData _gameplayCoreSceneSetupData;
        private RelativeScoreAndImmediateRankCounter _relativeScoreAndImmediateRankCounter;
        private PauseController _pauseController;
        [UIComponent("cover")]
        private readonly ImageView _cover;
        [UIComponent("songtime-ring")]
        private readonly ImageView _songtimeRing;
        private VRPointer _pointer;
        private Sprite _coverSprite;
        private AudioSpectrum _audioSpectrum;
        private ImageView[] _spectroms = Array.Empty<ImageView>();
        [UIComponent("audio-spetrum")]
        private readonly ImageView baseAudioSpectumImage;
        private CurvedCanvasSettingsHelper _curvedCanvasSettingsHelper;
        private TextFormatter _textFormatter;
        private MemoryPoolContainer<ScoreEntity> _scoreContainer;
        private float _songLength;
        private static readonly object _lockObject = new object();
        private int SortinglayerOrder;
        public const int UI_SORTING_ORDER = 31;
        /// <summary>プロファイル を取得、設定</summary>
        private ProfileEntity _profile;
        private IDifficultyBeatmap _difficultyBeatmap;

        private int _score;
        private int _combo;
        private float _seido;
        private RankModel.Rank _rank = RankModel.Rank.SSS;

        private bool _isUpdateSongName = true;
        private bool _isUpdateSongSubName = true;
        private bool _isUpdateSongKey = true;
        private bool _isUpdateSongAuthorName = true;
        private bool _isUpdateDifficurity = true;
        private bool _isUpdateScore = true;
        private bool _isUpdateCombo = true;
        private bool _isUpdateSeido = true;
        private bool _isUpdateRank = true;
        private bool _initialized = false;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scoreController"></param>
        /// <param name="comboController"></param>
        /// <param name="gameplayCoreSceneSetupData"></param>
        /// <param name="relativeScoreAndImmediateRankCounter"></param>
        /// <param name="inputModule"></param>
        /// <param name="audioTimeSource"></param>
        /// <param name="audioSpectrum"></param>
        /// <param name="textFormatter"></param>
        /// <param name="scorePool"></param>
        /// <param name="container"></param>
        [Inject]
        private void Constractor(IScoreController scoreController, IComboController comboController, GameplayCoreSceneSetupData gameplayCoreSceneSetupData, RelativeScoreAndImmediateRankCounter relativeScoreAndImmediateRankCounter, VRInputModule inputModule, IAudioTimeSource audioTimeSource, AudioSpectrum audioSpectrum, TextFormatter textFormatter, ScoreEntity.Pool scorePool, DiContainer container)
        {
            Logger.Debug("Constractor call");
            try {
                this._scoreController = scoreController;
                this._comboController = comboController;
                this._relativeScoreAndImmediateRankCounter = relativeScoreAndImmediateRankCounter;
                this._audioTimeSyncController = audioTimeSource;
                this._gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;

                this._audioSpectrum = audioSpectrum;
                this._textFormatter = textFormatter;
                this._pointer = inputModule.GetField<VRPointer, VRInputModule>("_vrPointer");
                this._curvedCanvasSettingsHelper = new CurvedCanvasSettingsHelper();
                this._scoreContainer = new MemoryPoolContainer<ScoreEntity>(scorePool);

                this._pauseController = container.TryResolve<PauseController>();

                this._audioSpectrum.Band = AudioSpectrum.BandType.ThirtyOneBand;
                this._difficultyBeatmap = this._gameplayCoreSceneSetupData.difficultyBeatmap;
                this._songLength = Mathf.Floor(this._audioTimeSyncController.songEndTime);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
    }
}
