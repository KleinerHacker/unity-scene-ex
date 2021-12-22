using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
using UnityEngine;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime
{
    public static class UnitySceneExEvent
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("Load scene settings");
            AssetResourcesLoader.Instance.LoadAssets<SceneSystemSettings>("");
        }
    }
}