using System;
using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEngine;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Extras;

namespace UnitySceneEx.Editor.scene_system.scene_ex.Scripts.Editor.Extras
{
    [CustomPropertyDrawer(typeof(SceneSystemSelectorAttribute))]
    public sealed class SceneSystemSelectorEditor : ExtendedDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
                throw new InvalidOperationException("Property " + property.name + " must typed with string");

            var itemNames = SceneSystemSettings.Singleton.Items.Select(x => x.Identifier).ToArray();
            var index = itemNames.IndexOf(x => string.Equals(x, property.stringValue));
            var newIndex = EditorGUI.Popup(position, property.displayName, index, itemNames);
            if (index != newIndex)
            {
                if (newIndex < 0)
                {
                    property.stringValue = null;
                }
                else
                {
                    property.stringValue = itemNames[newIndex];
                }
            }
        }
    }
}