#if DEMO
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime;

namespace UnitySceneEx.Demo.Projects.unity_scene_ex.Scripts.Demo
{
    public sealed class DemoWorld1Controller : MonoBehaviour
    {
        public void LoadNext()
        {
            WorldManager.LoadWorld("w2");
        }
    }
}
#endif