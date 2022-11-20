using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime;
using UnityEngine;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime
{
    public static class UnitySceneExStartupEvents
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("Load scene settings");
            AssetResourcesLoader.LoadFromResources<SceneSystemSettings>("");
        }
    }
}