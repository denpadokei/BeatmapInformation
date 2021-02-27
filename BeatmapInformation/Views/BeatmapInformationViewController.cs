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

        /// <summary>曲のタイトル を取得、設定</summary>
        private string songName_;
        [UIValue("song-name")]
        /// <summary>曲のタイトル を取得、設定</summary>
        public string SongName
        {
            get => this.songName_ ?? "SongName";

            set => this.SetProperty(ref this.songName_, value);
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

        /// <summary>曲作者 を取得、設定</summary>
        private string songAuthor_;
        [UIValue("song-author")]
        /// <summary>曲作者 を取得、設定</summary>
        public string SongAuthor
        {
            get => this.songAuthor_ ?? "";

            set => this.SetProperty(ref this.songAuthor_, value);
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

        /// <summary>スコア を取得、設定</summary>
        private string score_;
        [UIValue("score")]
        /// <summary>スコア を取得、設定</summary>
        public string Score
        {
            get => this.score_ ?? "0";

            set => this.SetProperty(ref this.score_, value);
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

        /// <summary>精度 を取得、設定</summary>
        private string seido_;
        [UIValue("seido")]
        /// <summary>精度 を取得、設定</summary>
        public string Seido
        {
            get => this.seido_ ?? "";

            set => this.SetProperty(ref this.seido_, value);
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
        protected override void OnDestroy()
        {
            Logger.Debug("OnDestroy call");
            this._scoreController.scoreDidChangeEvent -= this.OnScoreDidChangeEvent;
            this._scoreController.comboDidChangeEvent -= this.OnComboDidChangeEvent;
            this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent -= this.OnRelativeScoreOrImmediateRankDidChangeEvent;
            if (this._informationScreen != null) {
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
            this._currentScore = arg2;
            this.Score = $"{this._currentScore:#,0}";
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
        [UIComponent("cover")]
        private ImageView _cover;
        private Sprite _coverSprite;
        private volatile int _currentScore;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private async void Constractor(ScoreController scoreController, GameplayCoreSceneSetupData gameplayCoreSceneSetupData, RelativeScoreAndImmediateRankCounter relativeScoreAndImmediateRankCounter)
        {
            Logger.Debug("Constractor call");
            this._scoreController = scoreController;
            this._relativeScoreAndImmediateRankCounter = relativeScoreAndImmediateRankCounter;
            this._gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
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
            this._coverSprite = await previewBeatmapLevel.GetCoverImageAsync(CancellationToken.None);
            HMMainThreadDispatcher.instance.Enqueue(this.SetCover(this._coverSprite));
            this._informationScreen = FloatingScreen.CreateFloatingScreen(new Vector2(200f, 120f), false, new Vector3(0f, 0.7f, -1.1f), Quaternion.Euler(0, 0, 0));
            this._informationScreen.SetRootViewController(this, HMUI.ViewController.AnimationType.None);
            this._informationScreen.transform.SetParent(this.transform);
            this._informationScreen.GetComponent<Canvas>().sortingOrder = 30;
            this.SongName = previewBeatmapLevel.songName;
            this.SongSubName = previewBeatmapLevel.songSubName;
            this.SongAuthor = previewBeatmapLevel.songAuthorName;
            this.UpdateComboText(0);
        }
        #endregion
    }
}
