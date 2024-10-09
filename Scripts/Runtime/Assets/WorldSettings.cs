using System;
using UnityEditor;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Assets;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Extra;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets
{
    public sealed class WorldSettings : ProviderAsset<WorldSettings>
    {
        #region Static Area

        public static WorldSettings Singleton => GetSingleton("World", "worlds.asset");

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("World", "worlds.asset");
#endif

        #endregion
        
        #region Save Data

        [SerializeField] [Scene] private string startupScene;
        
        [SerializeField]
        private FadeItem[] fades = Array.Empty<FadeItem>();

        [SerializeField] private WorldItem[] worlds = Array.Empty<WorldItem>();

        #endregion

        #region Properties

        public string StartupScene => startupScene;

        public FadeItem[] Fades => fades;

        public WorldItem[] Worlds => worlds;

        #endregion
    }

    [Serializable]
    public sealed class FadeItem
    {
        #region Save Data

        [SerializeField] private string identifier;
        
        [SerializeField] 
        private WorldFade fade;

        #endregion

        #region Properties

        public string Identifier => identifier;

        public WorldFade Fade => fade;

        #endregion
    }

    [Serializable]
    public sealed class WorldItem
    {
        #region Save Data
        
        [SerializeField] 
        private string identifier;
        
        [SerializeField]
        private SceneItem[] scenes = Array.Empty<SceneItem>();

        [SerializeField] private string fadeKey;
        
        #endregion

        #region Properties

        public string Identifier => identifier;

        public SceneItem[] Scenes => scenes;

        public string FadeKey => fadeKey;

        #endregion
    }

    [Serializable]
    public sealed class SceneItem
    {
        #region Save Data

        [SerializeField] private string groups;
        
        [SerializeField] [Scene] private string scenePath;

        #endregion

        #region Properties

        public string Groups => groups;

        public string ScenePath => scenePath;

        #endregion
    }
}