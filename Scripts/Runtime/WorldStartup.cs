using UnityAssetLoader.Runtime.Projects.unity_asset_loader.Scripts.Runtime;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime
{
    public static class WorldStartup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("Initialize World System...");
            AssetResourcesLoader.LoadFromResources<WorldSettings>("");
        }
    }
}