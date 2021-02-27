using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using SiraUtil;
using BeatmapInformation.Views;

namespace BeatmapInformation.Installer
{
    public class BMInfoGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<BeatmapInformationViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
        }
    }
}
