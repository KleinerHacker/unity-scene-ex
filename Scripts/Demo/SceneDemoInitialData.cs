using UnityEngine;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
    public class SceneDemoInitialData : ScriptableObject
    {
        [SerializeField]
        private int value;

        public int Value => value;
    }
}