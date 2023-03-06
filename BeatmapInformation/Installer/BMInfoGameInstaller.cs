using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Models;
using BeatmapInformation.Views;
using HMUI;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRUIControls;
using Zenject;

namespace BeatmapInformation.Installer
{
    public static class ZenjectExtention
    {
        internal class DummyRaycaster : BaseRaycaster
        {
            public override Camera eventCamera => Camera.main;

            public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
            {
            }
        }
        /// <summary>
        /// 複数のキャンバスを設定するときに使う
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="onInstantiated"></param>
        /// <returns></returns>
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromNewComponentAsViewControllerAsTransient(this FromBinder binder, Action<InjectContext, object> onInstantiated = null)
        {
            var go = new GameObject("ViewController");

            go.gameObject.SetActive(false);
            var canvas = go.AddComponent<Canvas>();
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Normal;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent;

            var raycaster = go.AddComponent<DummyRaycaster>();
            var componentBinding = binder.FromNewComponentOnNewPrefab(go);
            raycaster.enabled = false;
            componentBinding.OnInstantiated((ctx, obj) =>
            {
                if (obj is ViewController vc) {
                    var newRaycaster = go.AddComponent<VRGraphicRaycaster>();
                    GameObject.Destroy(raycaster);
                    var cache = ctx.Container.Resolve<PhysicsRaycasterWithCache>();
                    newRaycaster.SetField("_physicsRaycaster", cache);
                    go.name = vc.GetType().Name;
                    var rt = vc.rectTransform;
                    rt.localEulerAngles = Vector3.zero;
                    rt.anchorMax = rt.localScale = Vector3.one;
                    rt.anchorMin = rt.sizeDelta = Vector2.zero;
                }
                onInstantiated?.Invoke(ctx, obj);
            });
            return componentBinding;
        }
    }

    public class BMInfoGameInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<MultiViewManager>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<BeatmapInformationViewController>().FromNewComponentAsViewControllerAsTransient().AsTransient();
            this.Container.BindInterfacesAndSelfTo<AudioSpectrum>().FromNewComponentOn(new GameObject(nameof(AudioSpectrum))).AsCached();
            this.Container.BindInterfacesAndSelfTo<TextFormatter>().AsSingle();
            this.Container.BindMemoryPool<ScoreEntity, ScoreEntity.Pool>().WithInitialSize(32).AsCached();
        }
    }
}
