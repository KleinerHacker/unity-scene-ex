using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor
{
    [Overlay(typeof(SceneView), "Scene Extensions", defaultDisplay = true)]
    public class SceneExtensionOverlay : ToolbarOverlay
    {
        public SceneExtensionOverlay() : base("UnitySceneEx/ToolbarSceneChooser")
        {
        }
    }
    
    [EditorToolbarElement("UnitySceneEx/ToolbarSceneChooser", typeof(SceneView))]
    public sealed class ToolbarSceneChooser : EditorToolbarDropdown
    {
        public ToolbarSceneChooser()
        {
            text = "Open World";
            tooltip = "Open a complete world with all its scenes";
            clicked += Onclicked;
        }

        private void Onclicked()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("xxx"), false, () => {});
            menu.ShowAsContext();
        }
    }
}