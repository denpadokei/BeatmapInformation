using BeatmapInformation.Views;
using BeatSaberMarkupLanguage.FloatingScreen;
using SiraUtil.Zenject;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BeatmapInformation.Models
{
    internal class MultiViewManager : IAsyncInitializable, IDisposable
    {
        private List<BeatmapInformationViewController> _viewControllers = new List<BeatmapInformationViewController>();
        private bool _disposedValue;
        private ProfileManager _profileManager;
        private DiContainer _diContainer;

        [Inject]
        public MultiViewManager(ProfileManager profileManager, DiContainer diContainer)
        {
            this._profileManager = profileManager;
            this._diContainer = diContainer;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            foreach (var profile in this._profileManager.Profiles.Values) {
                if (!profile.Enable) {
                    continue;
                }
                var view = _diContainer.Resolve<BeatmapInformationViewController>();
                view.InformationScreen = FloatingScreen.CreateFloatingScreen(new Vector2(200f, 120f), true, new Vector3(profile.ScreenPosX, profile.ScreenPosY, profile.ScreenPosZ), Quaternion.Euler(0f, 0f, 0f), profile.ScreenRadius);
                view.InformationScreen.ShowHandle = false;
                view.InformationScreen.SetRootViewController(view, HMUI.ViewController.AnimationType.None);
                foreach (var canvas in view.InformationScreen.GetComponentsInChildren<Canvas>(true)) {
                    if (profile.OverlayMode) {
                        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                        view.transform.localScale = Vector3.one;
                    }
                    else {
                        canvas.renderMode = RenderMode.WorldSpace;
                    }
                }
                await view.InitializeAsync(token, profile);
                view.gameObject.SetActive(true);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue) {
                if (disposing) {
                    foreach (var view in this._viewControllers) {
                        GameObject.Destroy(view.gameObject);
                    }
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
