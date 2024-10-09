using System;
using System.Collections.Generic;
using System.Linq;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor;
using UnityEditorInternal;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor.Provider
{
    public sealed class WorldList : TableReorderableList
    {
        private SceneList[] sceneLists = Array.Empty<SceneList>();
        private bool[] sceneListFoldouts = Array.Empty<bool>();
        
        public WorldList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject,
            elements)
        {
            Columns.Add(new FixedColumn { HeaderText = "Identifier", AbsoluteWidth = 100f, ElementCallback = IdentifierElementCallback});
            Columns.Add(new FlexibleColumn { HeaderText = "Scenes", ElementCallback = ScenesElementCallback});
            Columns.Add(new FixedColumn { HeaderText = "Fade", AbsoluteWidth = 200f, ElementCallback = FadeElementCallback});
            
            elementHeightCallback += ElementHeightCallback;

            OnChangedCallback(this);
            onChangedCallback += OnChangedCallback;
        }

        private void FadeElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var fadeKeyProp = prop.FindPropertyRelative("fadeKey");

            var elements = WorldSettings.Singleton.Fades
                .Select(x => string.IsNullOrWhiteSpace(x.Identifier) ? "<unknown>" : x.Identifier)
                .ToArray();
            var index = elements.IndexOf(x => x == fadeKeyProp.stringValue);
            var newIndex = EditorGUI.Popup(rect.Height(20f), index, elements);
            if (index != newIndex)
            {
                fadeKeyProp.stringValue = newIndex < 0 ? null : elements[newIndex];
            }
            
            if (newIndex < 0 || newIndex >= elements.Length)
            {
                GUI.Box(rect.Size(new Vector2(20f, 20f)), EditorGUIUtility.IconContent("console.warnicon.sml").image);
            }
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

        private void ScenesElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            sceneListFoldouts[i] = EditorGUI.BeginFoldoutHeaderGroup(rect.Height(20f), sceneListFoldouts[i], "Scenes");
            if (sceneListFoldouts[i])
            {
                sceneLists[i].DoList(rect.ShiftY(20f));
            }
            EditorGUI.EndFoldoutHeaderGroup();
        }

        private void OnChangedCallback(ReorderableList reorderableList)
        {
            sceneListFoldouts = new bool[reorderableList.serializedProperty.arraySize];
            sceneLists = new SceneList[reorderableList.serializedProperty.arraySize];
            for (var i = 0; i < sceneLists.Length; i++)
            {
                var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(i);
                var scenesProp = prop.FindPropertyRelative("scenes");
                
                sceneLists[i] = new SceneList(reorderableList.serializedProperty.serializedObject, scenesProp);
            }
        }

        private float ElementHeightCallback(int i)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            var scenesProp = prop.FindPropertyRelative("scenes");

            return sceneListFoldouts[i] ? 100f + (scenesProp.arraySize - 1) * 25f : 25f;
        }
    }
}