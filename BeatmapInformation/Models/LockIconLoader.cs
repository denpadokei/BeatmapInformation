using BeatSaberMarkupLanguage;
using IPA.Loader.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapInformation.Models
{
    internal class LockIconLoader
    {
        public const string LOCK_PATH = "BeatmapInformation.Icon.lock.png";
        public const string UNLOCK_PATH = "BeatmapInformation.Icon.unlock.png";

        public static Sprite Lock { get; private set; }
        public static Sprite UnLock { get; private set; }
        static LockIconLoader()
        {
            LoadIcon();
        }

        private static void LoadIcon()
        {
            try {
                var asm = Assembly.GetExecutingAssembly();
                var lockPngStream = asm.GetManifestResourceStream(LOCK_PATH);
                using (lockPngStream) {
                    var data = new byte[lockPngStream.Length];
                    lockPngStream.Read(data, 0, data.Length);
                    Lock = Utilities.LoadSpriteRaw(data);
                }

                var unLockPngStream = asm.GetManifestResourceStream(UNLOCK_PATH);
                using (unLockPngStream) {
                    var data = new byte[unLockPngStream.Length];
                    unLockPngStream.Read(data, 0, data.Length);
                    UnLock = Utilities.LoadSpriteRaw(data); ;
                }
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }
    }
}
