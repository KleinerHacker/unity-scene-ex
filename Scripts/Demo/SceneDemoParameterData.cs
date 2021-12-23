using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
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
}