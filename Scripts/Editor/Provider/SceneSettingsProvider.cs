using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnitySceneBase.Editor.scene_system.scene_base.Scripts.Editor.Provider;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Editor.scene_system.scene_ex.Scripts.Editor.Provider
{
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
            base("Project/Player/Scene System", new []{"Scene", "System", "Tooling", "Loading"})
        {
        }

        public override void OnGUI(string searchContext)
        {
#if WORLD_EX
            EditorGUILayout.HelpBox("World Extensions and Scene Extension are not compatible. Please remove one dependency from project!", MessageType.Error);
#endif
            
            base.OnGUI(searchContext);
        }

        protected override ReorderableList CreateItemList(SerializedObject settings, SerializedProperty itemsProperty) => new SceneItemList(settings, itemsProperty);
    }
}