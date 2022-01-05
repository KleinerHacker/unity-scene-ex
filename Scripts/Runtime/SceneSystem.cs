using System;
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime
{
    public static class SceneSystem
    {
        public static void Load(string identifier, ParameterData parameterData = null, bool overwrite = true) => 
            SceneController.Singleton.Load(identifier, parameterData, overwrite);

        public static void Load(string identifier, bool doNotUnload, ParameterData parameterData = null, bool overwrite = true) => 
            SceneController.Singleton.Load(identifier, doNotUnload, parameterData, overwrite);

        public static void Load(string identifier, Action onFinished, ParameterData parameterData = null, bool overwrite = true) => 
            SceneController.Singleton.Load(identifier, onFinished, parameterData, overwrite);

        public static void Load(string identifier, bool doNotUnload, Action onFinished, ParameterData parameterData = null, bool overwrite = true) => 
            SceneController.Singleton.Load(identifier, doNotUnload, onFinished, parameterData, overwrite);
    }
}