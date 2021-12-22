#if !UNITY_EDITOR
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
#endif
using System;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets
{
    public sealed class SceneSystemSettings : SceneSystemSettingsBase<SceneItem>
    {
        #region Static Area

#if UNITY_EDITOR
        private const string Path = "Assets/Resources/scene-system.asset";
#endif

        public static SceneSystemSettings Singleton
        {
            get
            {
#if UNITY_EDITOR
                var settings = AssetDatabase.LoadAssetAtPath<SceneSystemSettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find game settings, create new");

                    settings = new SceneSystemSettings();
                    AssetDatabase.CreateAsset(settings, Path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                return settings;
#else
                return AssetResourcesLoader.Instance.GetAsset<SceneSystemSettings>();
#endif
            }
        }

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);
#endif

        #endregion
    }

    [Serializable]
    public sealed class SceneItem : SceneItemBase
    {
        #region Inspector Data

        [SerializeField]
        [Scene]
        private string scene;

        [SerializeField]
        private bool neverUnloadScene;

        #endregion

        #region Properties

        public string Scene => scene;

        public override string[] Scenes => new[] { scene };

        public bool NeverUnloadScene => neverUnloadScene;

        #endregion
    } 
}