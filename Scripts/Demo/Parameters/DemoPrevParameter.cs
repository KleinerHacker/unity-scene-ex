using UnityEngine;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Parameters
{
    public interface IDemoPrevParameter
    {
        public string Text { get; }
    }
    
    [ParameterInitialDataType(typeof(DemoPrevParameterInitialData))]
    public sealed class DemoPrevParameter : ParameterData, IDemoPrevParameter
    {
        public string Text
        {
            get => Get<string>("text");
            set => Add("text", value);
        }

        public override void InitializeData(ScriptableObject initData)
        {
            var data = (DemoPrevParameterInitialData)initData;
            if (data == null)
                return;

            Text = data.Text;
        }
    }

    public sealed class DemoPrevParameterInitialData : ScriptableObject, IDemoPrevParameter
    {
        [SerializeField]
        private string text;

        public string Text => text;
    }
}