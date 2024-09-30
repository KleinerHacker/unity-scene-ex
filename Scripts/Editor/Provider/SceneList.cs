using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor;
using UnityEngine;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor.Provider
{
    public sealed class SceneList : TableReorderableList
    {
        public SceneList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FixedColumn {HeaderText = "Groups (optional)", AbsoluteWidth = 150f, ElementCallback = GroupsElementCallback});
            Columns.Add(new FlexibleColumn {HeaderText = "Scene", ElementCallback = SceneElementCallback});

            elementHeight = 23f;
        }

        private void SceneElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var scenePathProp = prop.FindPropertyRelative("scenePath");
            
            EditorGUI.PropertyField(rect.Height(20f), scenePathProp, GUIContent.none);
        }

        private void GroupsElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var groupsProp = prop.FindPropertyRelative("groups");
            
            EditorGUI.PropertyField(rect.Height(20f), groupsProp, GUIContent.none);
        }
    }
}