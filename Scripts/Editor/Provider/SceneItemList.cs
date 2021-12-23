using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnitySceneBase.Editor.scene_system.scene_base.Scripts.Editor.Provider;

namespace UnitySceneEx.Editor.scene_system.scene_ex.Scripts.Editor.Provider
{
    public sealed class SceneItemList : ItemListBase
    {
        public SceneItemList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
        }

        protected override void OnDrawCommonHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Scene");
        }

        protected override void OnDrawCommonElement(Rect rect, int i, bool isactive, bool isfocused)
        {
            var property = serializedProperty.GetArrayElementAtIndex(i);
            var sceneProperty = property.FindPropertyRelative("scene");
            EditorGUI.PropertyField(rect, sceneProperty, GUIContent.none);
        }
    }
}