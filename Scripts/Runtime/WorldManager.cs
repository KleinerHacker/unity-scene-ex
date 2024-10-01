using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime
{
    public static class WorldManager
    {
        public static void LoadWorld(string worldKey)
        {
            LoadScenes(worldKey, () => { });
        }

        private static void LoadScenes(string worldKey, Action onComplete)
        {
#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Loading world: " + worldKey);
#endif
            var world = WorldSettings.Singleton.Worlds.First(x => x.Identifier == worldKey);

#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Loading scenes: " + world.Scenes.Length);
#endif

            var operations = new List<AsyncOperation>();
            for (var i = 0; i < world.Scenes.Length; i++)
            {
                var scene = world.Scenes[i];
#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] > Load scene: " + scene.ScenePath);
#endif
                var operation = SceneManager.LoadSceneAsync(scene.ScenePath, i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
                operations.Add(operation);
            }

#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Start loading timer");
#endif
            Task.Run(() =>
            {
                while (!operations.IsReady())
                {
                    Thread.Sleep(10);
                }

#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] Finish loading timer");
#endif
                onComplete?.Invoke();
            });
        }
    }
}