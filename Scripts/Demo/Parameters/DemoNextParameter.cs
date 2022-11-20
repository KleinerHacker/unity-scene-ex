using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Parameters
{
    public interface IDemoNextParameter
    {
        public string Text { get; }
    }
    
    [ParameterInitialDataType(typeof(DemoNextParameterInitialData))]
    public sealed class DemoNextParameter : ParameterData, IDemoNextParameter
    {
        public string Text
        {
            get => Get<string>("text");
            set => Add("text", value);
        }

        public override void InitializeData(ScriptableObject initData)
        {
            var data = (DemoNextParameterInitialData)initData;
            if (data == null)
                return;

            Text = data.Text;
        }
    }

    public sealed class DemoNextParameterInitialData : ScriptableObject, IDemoNextParameter
    {
        [SerializeField]
        private string text;

        public string Text => text;
    }
}