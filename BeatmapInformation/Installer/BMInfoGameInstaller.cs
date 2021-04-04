﻿using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Models;
using BeatmapInformation.Views;
using SiraUtil;
using Zenject;

namespace BeatmapInformation.Installer
{
    public class BMInfoGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<BeatmapInformationViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<AudioSpectrum>().FromNewComponentOnNewGameObject(nameof(AudioSpectrum)).AsCached();
            this.Container.BindInterfacesAndSelfTo<TextFormatter>().AsSingle();
            this.Container.BindMemoryPool<ScoreEntity, ScoreEntity.Pool>().WithInitialSize(32).AsCached();
        }
    }
}
