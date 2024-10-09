using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor;
using UnityEngine;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor.Provider
{
    public sealed class FadeList : TableReorderableList
    {
        public FadeList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FixedColumn { HeaderText = "Identifier", AbsoluteWidth = 150f, ElementCallback = IdentifierElementCallback});
            Columns.Add(new FlexibleColumn { HeaderText = "Fade", ElementCallback = FadeElementCallback});
        }

        private void IdentifierElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var identifierProp = prop.FindPropertyRelative("identifier");
            
            EditorGUI.PropertyField(rect.Height(20f), identifierProp, GUIContent.none);
            if (string.IsNullOrWhiteSpace(identifierProp.stringValue))
            {
                GUI.Box(rect.Size(new Vector2(20f, 20f)), EditorGUIUtility.IconContent("console.warnicon.sml").image);
            }
        }
        
        private void FadeElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var fadeProp = prop.FindPropertyRelative("fade");
            
            EditorGUI.PropertyField(rect.Height(20f), fadeProp, GUIContent.none);
            if (fadeProp.objectReferenceValue == null)
            {
                GUI.Box(rect.Size(new Vector2(20f, 20f)), EditorGUIUtility.IconContent("console.warnicon.sml").image);
            }
        }
    }
}