using System;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets
{
    public sealed class SceneSystemSettings : SceneSystemSettingsBase<SceneItem, SceneSystemSettings>
    {
        #region Static Area

        private const string FileName = "scene-system.asset";

        public static SceneSystemSettings Singleton => GetSingleton("Scene System", FileName);

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("Scene System", FileName);
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

        #endregion

        #region Properties

        public string Scene => scene;

        public override string[] Scenes => new[] { scene };

        #endregion
    }
}