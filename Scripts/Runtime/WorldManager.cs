using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;
using Object = UnityEngine.Object;

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime
{
    public static class WorldManager
    {
        public static void LoadWorld(string worldKey, Action onLoadCompleted = null)
        {
            DoLoadWorld(worldKey, onLoadCompleted);
        }

        private static void DoLoadWorld(string worldKey, Action onLoadCompleted)
        {
#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Loading world: " + worldKey);
#endif
            var world = WorldSettings.Singleton.Worlds.First(x => x.Identifier == worldKey);
            var fade = WorldSettings.Singleton.Fades.FirstOrDefault(x => x.Identifier == world.FadeKey);

            if (fade == null)
            {
#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] Load without fade...");
#endif
                LoadScenes(world, null, onLoadCompleted);
                return;
            }

#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Instantiate World Fade Prefab: " + fade.Identifier);
#endif
            var fadeGo = Object.Instantiate(fade.Fade.gameObject, Vector3.zero, Quaternion.identity);
            Object.DontDestroyOnLoad(fadeGo);

#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Show World Fade Prefab...");
#endif
            var worldFade = fadeGo.GetComponent<WorldFade>();
            worldFade.Show(worldKey, () => LoadScenes(world, worldFade, () =>
            {
#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] Hide World Fade Prefab...");
#endif
                worldFade.Hide(worldKey, () =>
                {
                    Object.Destroy(fadeGo);
                    onLoadCompleted?.Invoke();
                });
            }));
        }

        private static async void LoadScenes(WorldItem world, WorldFade fade, Action onComplete)
        {
#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Loading scenes: " + world.Scenes.Length);
#endif

            var loadingOperations = world.Scenes
                .Select((scene, i) => LoadScene(scene, i == 0))
                .ToList();

#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Wait for loading");
#endif

            do
            {
                UnityDispatcher.RunLater(() => fade.OnProgressUpdated(world.Identifier, loadingOperations.CalculateProgress()));
                await Task.Yield();
            } while (loadingOperations.IsReady());
            
#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] Loading completetd");
#endif
            UnityDispatcher.RunLater(() => fade.OnProgressCompleted(world.Identifier));

            if (fade.AutoFinish)
            {
                onComplete?.Invoke();
            }
            else
            {
#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] Wait for manual finishing...");
#endif
                fade.OnFinished += RunComplete;
            }

            void RunComplete(object sender, EventArgs e)
            {
#if PCSOFT_WORLD_LOGGING
                Debug.Log("[Scene System] Receive manual finishing...");
#endif
                
                fade.OnFinished -= RunComplete;
                onComplete?.Invoke();
            }
        }

        private static AsyncOperation LoadScene(SceneItem scene, bool firstScene)
        {
#if PCSOFT_WORLD_LOGGING
            Debug.Log("[Scene System] > Load scene: " + scene.ScenePath);
#endif
            
            return SceneManager.LoadSceneAsync(scene.ScenePath,
                firstScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }
    }
}