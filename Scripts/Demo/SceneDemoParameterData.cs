#if DEMO
using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;
#endif

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
#if DEMO
    [ParameterInitialDataType(typeof(SceneDemoInitialData))]
    public sealed class SceneDemoParameterData : ParameterData
    {
        public int Value { get; set; }

        public override void InitializeData(ScriptableObject initData)
        {
            var data = (SceneDemoInitialData)initData;
            Value = data.Value;
        }
    }
#endif
}