#if DEMO
using UnityEngine;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Extras;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
    [CreateAssetMenu(menuName = "DEMO/Scene Demo Asset")]
    public sealed class SceneDemoAsset : ScriptableObject
    {
        [SceneSystemSelector]
        [SerializeField]
        private string scene;
    }
}
#endif