using System.Linq;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils;
using UnityEditorInternal;
using UnityEngine;
using UnitySceneBase.Editor.scene_system.scene_base.Scripts.Editor.Provider;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Editor.scene_system.scene_ex.Scripts.Editor.Provider
{
    [SceneBaseInfo("PCSOFT_SCENE")]
    public sealed class SceneSettingsProvider : SceneSettingsProviderBase
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SceneSettingsProvider();
        }

        #endregion

        #region Properties

        protected override SerializedObject Settings => SceneSystemSettings.SerializedSingleton;
        protected override bool HasAnyEmptyIdentifier => SceneSystemSettings.Singleton.Items.Any(x => string.IsNullOrWhiteSpace(x.Identifier));
        protected override bool HasAnyDoubleIdentifier => SceneSystemSettings.Singleton.Items.GroupBy(x => x.Identifier).Any(x => x.Count() > 1);

        #endregion

        public SceneSettingsProvider() :
            base("Project/Player/Scene System", new[] { "Scene", "System", "Tooling", "Loading" })
        {
        }

        public override void OnGUI(string searchContext)
        {
            GUILayout.Space(15f);

#if PCSOFT_WORLD
            EditorGUILayout.HelpBox("World Extensions and Scene Extension are not compatible. Please remove one dependency from project!", MessageType.Error);
#endif

            ExtendedEditorGUILayout.SymbolFieldLeft("Activate Toolbar Integration", "SCENE_TOOLBAR_INTEGRATION");
            EditorGUILayout.Space(25f);
            base.OnGUI(searchContext);
        }

        protected override ReorderableList CreateItemList(SerializedObject settings, SerializedProperty itemsProperty) => new SceneItemList(settings, itemsProperty);
    }
}