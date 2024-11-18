using BeatmapInformation.Configuration;
using BeatmapInformation.Models;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using Zenject;

namespace BeatmapInformation.Views
{
    [HotReload]
    internal class SettingTabViewController : BSMLAutomaticViewController, IInitializable
    {
        [Inject]
        public void Constractor(ProfileManager profileManager)
        {
            this._profileManager = profileManager;
        }

        private ProfileManager _profileManager;

        public string ResourceName => string.Join(".", this.GetType().Namespace, this.GetType().Name);
        

        /// <summary>説明 を取得、設定</summary>
        private List<object> _profiles = new List<object>();
        /// <summary>説明 を取得、設定</summary>
        [UIValue("profiles")]
        public List<object> Profiles
        {
            get => this._profiles ?? (this._profiles = new List<object>());

            set => this.SetProperty(ref this._profiles, value);
        }

        [UIComponent("profiles-list")]
        private CustomCellListTableData _profileTableData;

        protected override void OnDestroy()
        {
            GameplaySetup.Instance.RemoveTab("Beatmap Information");
            base.OnDestroy();
        }

        public void Initialize()
        {
            GameplaySetup.Instance.AddTab("Beatmap Information", this.ResourceName, this);
        }

        [UIAction("#post-parse")]
        public void PostParse()
        {
            this.ReLoad();
        }

        [UIAction("reset-position")]
        private void ResetPositionAndRotation()
        {
            PluginConfig.Instance.ScreenPosX = 0f;
            PluginConfig.Instance.ScreenPosY = 0.7f;
            PluginConfig.Instance.ScreenPosZ = -1.1f;

            PluginConfig.Instance.ScreenRotX = 0f;
            PluginConfig.Instance.ScreenRotY = 0f;
            PluginConfig.Instance.ScreenRotZ = 0f;
        }

        [UIAction("reload")]
        public void ReLoad()
        {
            this.Load();
        }

        

        private void Load()
        {
            this.Profiles.Clear();
            foreach (var profile in this._profileManager.Profiles.Values) {
                var entity = new ProfileListEntity(profile);
                
                this.Profiles.Add(entity);
            }
            this._profileTableData.TableView.ReloadData();
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) {
                return false;
            }

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        protected virtual void OnPropertyChanged(string args)
        {
            this.NotifyPropertyChanged(args);
        }
    }
}