using BeatmapInformation.Views;
using BeatSaberMarkupLanguage.FloatingScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BeatmapInformation.Models
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class BeatmapInformationController : MonoBehaviour
    {
        private FloatingScreen informationScreen;
        [Inject]
        BeatmapInformationViewController viewController;
        // These methods are automatically called by Unity, you should remove any you aren't using.
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            Plugin.Log?.Debug($"{name}: Awake()");
            this.informationScreen = FloatingScreen.CreateFloatingScreen(new Vector2(5.5f, 3f), false, Vector3.zero, Quaternion.identity);
            this.informationScreen.SetRootViewController(viewController, HMUI.ViewController.AnimationType.None);
        }
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {
            
        }

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {

        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");

        }
        #endregion
    }
}
