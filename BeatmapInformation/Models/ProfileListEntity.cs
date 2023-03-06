using BeatmapInformation.Bases;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

namespace BeatmapInformation.Models
{
    internal class ProfileListEntity : NotificationObject
    {
        public ProfileListEntity(ProfileEntity entity)
        {
            this._profile = entity;
            this._profileName = Path.GetFileNameWithoutExtension(entity.FilePath);
            this._enable = entity.Enable;
            this._lockPosition = entity.LockPosition;
        }

        private readonly ProfileEntity _profile;

        /// <summary>プロファイル名 を取得、設定</summary>
        private string _profileName;
        /// <summary>プロファイル名 を取得、設定</summary>
        [UIValue("profile-name")]
        public string ProfileName
        {
            get => this._profileName;

            set => this.SetProperty(ref this._profileName, value);
        }

        /// <summary>有効化無効化 を取得、設定</summary>
        private bool _enable;
        /// <summary>有効化無効化 を取得、設定</summary>
        [UIValue("enable")]
        public bool Enable
        {
            get => this._enable;

            set => this.SetProperty(ref this._enable, value);
        }

        /// <summary>動かせるかどうか を取得、設定</summary>
        private bool _lockPosition;
        /// <summary>動かせるかどうか を取得、設定</summary>
        public bool LockPosition
        {
            get => this._lockPosition;

            set => this.SetProperty(ref this._lockPosition, value);
        }

        [UIComponent("lock-button")]
        private ClickableImage _button;

        [UIAction("#post-parse")]
        public void PostParse()
        {
            this._button.sprite = this.LockPosition ? LockIconLoader.Lock : LockIconLoader.UnLock;
        }

        [UIAction("lock-click")]
        public void ChangeLock()
        {
            this.LockPosition = !this.LockPosition;
            this._button.sprite = this.LockPosition ? LockIconLoader.Lock : LockIconLoader.UnLock;
            this.Save();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(this.Enable)) {
                this.Save();
            }
        }

        public void Save()
        {
            this._profile.Enable = this.Enable;
            this._profile.LockPosition = this.LockPosition;
            this._profile.Save();
        }
    }
}
