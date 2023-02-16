using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils;
using UnityEngine;
using UnitySceneBase.Editor.scene_system.scene_base.Scripts.Editor;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;
using UnityToolbarExtender;

namespace UnitySceneEx.Editor.scene_system.scene_ex.Scripts.Editor.Provider
{
    [InitializeOnLoad]
    public static class SceneSystemToolbar
    {
#if SCENE_TOOLBAR_INTEGRATION
        private static readonly SceneSystemSettings SceneSystemSettings;
        private static readonly SerializedObject SerializedObject;

        static SceneSystemToolbar()
        {
            SceneSystemSettings = SceneSystemSettings.Singleton;
            SerializedObject = SceneSystemSettings.SerializedSingleton;

            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            SerializedObject.Update();

            GUILayout.FlexibleSpace();

            GUILayout.Space(5f);

            ExtendedEditorGUILayout.SymbolField(new GUIContent("Use Scene System"), "PCSOFT_SCENE", ToolbarStyles.toggleStyle);
            ExtendedEditorGUILayout.SymbolField(new GUIContent("Editor Scene Loading"), "PCSOFT_SCENE_EDITOR_LOAD", ToolbarStyles.toggleStyle);

            SerializedObject.ApplyModifiedProperties();
        }

        private static class ToolbarStyles
        {
            public static readonly GUIStyle toggleStyle;

            static ToolbarStyles()
            {
                toggleStyle = new GUIStyle("Toggle")
                {
                    fontSize = 12,
                    alignment = TextAnchor.MiddleLeft,
                    imagePosition = ImagePosition.TextOnly,
                    fontStyle = FontStyle.Normal,
                    fixedHeight = 20f,
                    wordWrap = false,
                    margin = new RectOffset(5, 5, 5, 5)
                };
            }
        }
#endif
    }
}