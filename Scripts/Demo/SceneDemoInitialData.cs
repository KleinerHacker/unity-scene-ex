#if DEMO
using UnityEngine;
#endif

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
#if DEMO
    public class SceneDemoInitialData : ScriptableObject
    {
        [SerializeField]
        private int value;

        public int Value => value;
    }
#endif
}