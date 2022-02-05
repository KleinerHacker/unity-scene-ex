using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Components;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Assets;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime
{
    public static class SceneParameter
    {
        public static T Get<T>() where T : ParameterData => SceneParameterController.GetData<T>(SceneSystemSettings.Singleton.ParameterInitialData);
    }
}