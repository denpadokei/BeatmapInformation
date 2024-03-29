﻿using BeatmapInformation.Views;
using Zenject;

namespace BeatmapInformation.Installer
{
    public class BMInfoMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingTabViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
        }
    }
}
