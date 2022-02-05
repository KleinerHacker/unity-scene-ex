using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Assets;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Components;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Components
{
    public sealed class SceneController : SceneControllerBase<SceneController, SceneItem>
    {
        #region Static Area

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadSceneSystem()
        {
            if (!SceneSystemSettings.Singleton.UseSystem)
                return;
            
            Debug.Log("Loading scene system");
            LoadSceneSystemBasics(SceneSystemSettings.Singleton.BlendingSystem, SceneSystemSettings.Singleton.AdditionalGameObjects, 
                SceneSystemSettings.Singleton.CreateEventSystem,
                eventSystem =>
                {
                    eventSystem.sendNavigationEvents = SceneSystemSettings.Singleton.ESUseNavigation;
                    eventSystem.firstSelectedGameObject = SceneSystemSettings.Singleton.ESFirstSelection;
                    eventSystem.pixelDragThreshold = SceneSystemSettings.Singleton.ESDragThreshold;
                },
                inputModule =>
                {
                    inputModule.moveRepeatDelay = SceneSystemSettings.Singleton.ESMoveRepeatDelay;
                    inputModule.moveRepeatRate = SceneSystemSettings.Singleton.ESMoveRepeatRate;
                    inputModule.deselectOnBackgroundClick = SceneSystemSettings.Singleton.ESDeselectOnBackground;
                    inputModule.pointerBehavior = SceneSystemSettings.Singleton.ESPointerBehavior;
                    if (SceneSystemSettings.Singleton.ESActionAsset != null)
                    {
                        inputModule.actionsAsset = SceneSystemSettings.Singleton.ESActionAsset;
                    }

                    inputModule.xrTrackingOrigin = SceneSystemSettings.Singleton.ESXROrigin;
                });

            var goSceneSystem = new GameObject("Scene System");
            var sceneSystem = goSceneSystem.AddComponent<SceneController>();
            DontDestroyOnLoad(goSceneSystem);

            var sceneItem = SceneSystemSettings.Singleton.Items
                .FirstOrDefault(x => x.Scene == SceneManager.GetActiveScene().path);
            sceneSystem.RaiseSceneEvent(RuntimeOnSwitchSceneType.LoadScenes, sceneItem?.Identifier, new[] { SceneManager.GetActiveScene().path });
        }

        #endregion

        #region Properties

        protected override bool UseBlendCallbacks => SceneSystemSettings.Singleton.UseSwitchCallbacks;
        protected override bool UseSwitchCallbacks => SceneSystemSettings.Singleton.UseSwitchCallbacks;
        protected override SceneBlendState StartupBlendState => SceneSystemSettings.Singleton.StartupBlendState;

        #endregion

        public override void Load(string identifier, bool doNotUnload, Action onFinished, ParameterData parameterData = null, bool overwrite = true) => 
            Load(identifier, doNotUnload, onFinished, parameterData, overwrite, SceneSystemSettings.Singleton.ParameterInitialData);

        protected override SceneItem FindSceneItem(string identifier) => SceneSystemSettings.Singleton.Items.FirstOrDefault(x => x.Identifier == identifier);

        protected override string[] RaiseSceneEvent(RuntimeOnSwitchSceneType type, string identifier, string[] scenes)
        {
            var result = base.RaiseSceneEvent(type, identifier, scenes);
            if (type == RuntimeOnSwitchSceneType.UnloadScenes)
            {
                var scenesNeverUnload = SceneSystemSettings.Singleton.Items
                    .Where(x => x.NeverUnload)
                    .SelectMany(x => x.Scenes)
                    .ToArray();

                result = result.Where(x => !scenesNeverUnload.Contains(x)).ToArray();
            }

            return result;
        }

        protected override string GetAllowedParameterDataType(string identifier)
        {
            var sceneItem = SceneSystemSettings.Singleton.Items.FirstOrDefault(x => x.Identifier == identifier);
            if (sceneItem == null)
                throw new ArgumentException("Identifier unknown: " + identifier);

            return sceneItem.ParameterDataType;
        }

        protected override bool IsAllowNullParameterData(string identifier)
        {
            var sceneItem = SceneSystemSettings.Singleton.Items.FirstOrDefault(x => x.Identifier == identifier);
            if (sceneItem == null)
                throw new ArgumentException("Identifier unknown: " + identifier);

            return sceneItem.ParameterDataAllowNull;
        }
    }
}