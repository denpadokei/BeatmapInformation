using BeatmapInformation.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace BeatmapInformation.Views
{
    [HotReload]
    internal class SettingTabViewController : BSMLAutomaticViewController, IInitializable
    {
        public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);
        [UIValue("enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab("Beatmap Information");
            base.OnDestroy();
        }

        public void Initialize()
        {
            GameplaySetup.instance.AddTab("Beatmap Information", this.ResourceName, this);
        }
    }
}