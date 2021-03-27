using BeatmapInformation.Views;
using SiraUtil;
using Zenject;

namespace BeatmapInformation.Installer
{
    public class BMInfoGameInstaller : MonoInstaller
    {
        public override void InstallBindings() => this.Container.BindInterfacesAndSelfTo<BeatmapInformationViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
    }
}
