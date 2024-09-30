using System;
using System.Collections.Generic;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor;
using UnityEditorInternal;
using UnityEngine;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor.Provider
{
    public sealed class WorldList : TableReorderableList
    {
        private SceneList[] lists = Array.Empty<SceneList>();
        
        public WorldList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject,
            elements)
        {
            Columns.Add(new FixedColumn { HeaderText = "Identifier", AbsoluteWidth = 100f, ElementCallback = IdentifierElementCallback});
            Columns.Add(new FlexibleColumn { HeaderText = "Scenes", ElementCallback = ScenesElementCallback});
            
            elementHeightCallback += ElementHeightCallback;

            OnChangedCallback(this);
            onChangedCallback += OnChangedCallback;
        }

        private float ElementHeightCallback(int i)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var scenesProp = prop.FindPropertyRelative("scenes");
            
            return 75f + (scenesProp.arraySize - 1) * 25f;
        }

        private void OnChangedCallback(ReorderableList reorderableList)
        {
            lists = new SceneList[reorderableList.serializedProperty.arraySize];
            for (var i = 0; i < lists.Length; i++)
            {
                var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(i);
                var scenesProp = prop.FindPropertyRelative("scenes");
                
                lists[i] = new SceneList(reorderableList.serializedProperty.serializedObject, scenesProp);
            }
        }

        private void IdentifierElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var identifierProp = prop.FindPropertyRelative("identifier");
            
            EditorGUI.PropertyField(rect.Height(20f), identifierProp, GUIContent.none);
        }

        private void ScenesElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            lists[i].DoList(rect);
        }
    }
}