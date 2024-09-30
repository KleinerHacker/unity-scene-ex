using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Editor.Projects.unity_scene_ex.Scripts.Editor.Provider
{
    public sealed class WorldProvider : SettingsProvider
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new WorldProvider();
        }

        #endregion

        private WorldList worldList;
        
        private SerializedObject _settings;

        private SerializedProperty startupSceneProperty;
        private SerializedProperty worldsProperty;

        public WorldProvider() : base("Project/Player/Worlds", SettingsScope.Project, new List<string> {"World", "Scene"})
        {
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = WorldSettings.SerializedSingleton;
            if (_settings == null)
                return;

            startupSceneProperty = _settings.FindProperty("startupScene");
            worldsProperty = _settings.FindProperty("worlds");

            worldList = new WorldList(_settings, worldsProperty);
        }

        public override void OnTitleBarGUI()
        {
            ExtendedEditorGUILayout.SymbolField("Activate Verbose Logging", "WORLD_LOGGING");
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();
            
            EditorGUILayout.LabelField("Startup Scene:");
            EditorGUILayout.PropertyField(startupSceneProperty, GUIContent.none);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("List of worlds", EditorStyles.boldLabel);
            worldList.DoLayoutList();
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Update Build Scenes"))
            {
                if (EditorUtility.DisplayDialog("Override Build Scenes",
                        "Are you sure to override all scenes in build settings?", "Yes", "No"))
                {
                    var scenes = WorldSettings.Singleton.Worlds
                        .SelectMany(x => x.Scenes)
                        .Select(x => x.ScenePath)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();
                    scenes.Insert(0, WorldSettings.Singleton.StartupScene);
                    EditorBuildSettings.scenes = scenes
                        .Select(x => new EditorBuildSettingsScene(x, true))
                        .ToArray();
                    
                    Debug.Log("Replace all scenes with (in this order): " + string.Join(",", scenes));
                }
            }

            _settings.ApplyModifiedProperties();
        }
    }
}