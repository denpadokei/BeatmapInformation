using BeatmapInformation.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;

namespace BeatmapInformation.Views
{
    [HotReload]
    internal class SettingTabViewController : BSMLAutomaticViewController, IInitializable
    {
        public string ResourceName => string.Join(".", this.GetType().Namespace, this.GetType().Name);
        [UIValue("enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        [UIValue("postion-lock")]
        public bool PositionLovk
        {
            get => PluginConfig.Instance.LockPosition;
            set => PluginConfig.Instance.LockPosition = value;
        }

        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab("Beatmap Information");
            base.OnDestroy();
        }

        public void Initialize() => GameplaySetup.instance.AddTab("Beatmap Information", this.ResourceName, this);

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
    }
}