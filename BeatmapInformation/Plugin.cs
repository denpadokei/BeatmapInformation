using BeatmapInformation.Installer;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace BeatmapInformation
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("BeatmapInformation initialized.");
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
            zenjector.OnGame<BMInfoGameInstaller>().OnlyForStandard();
            zenjector.OnMenu<BMInfoMenuInstaller>();
        }

        [OnStart]
        public void OnApplicationStart() => Log.Debug("OnApplicationStart");

        [OnExit]
        public void OnApplicationQuit() => Log.Debug("OnApplicationQuit");
    }
}
