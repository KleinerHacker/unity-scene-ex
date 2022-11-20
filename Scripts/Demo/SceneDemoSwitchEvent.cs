#if DEMO
using UnitySceneBase.Runtime.scene_system.scene_base.Scripts.Runtime.Types;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo
{
    public static class SceneDemoSwitchEvent
    {
        [RuntimeOnSwitchScene(RuntimeOnSwitchSceneType.LoadScenes)]
        public static void OnLoad(RuntimeOnSwitchSceneArgs args)
        {
            if (args.Identifier == "next")
            {
                args.AdditionalScenes = new[] { "Assets/scene-system/scene-ex/Demo/DemoNextAdditional.unity" };
            }
        }
        
        [RuntimeOnSwitchScene(RuntimeOnSwitchSceneType.UnloadScenes)]
        public static void OnUnload(RuntimeOnSwitchSceneArgs args)
        {
            if (args.Identifier == "next")
            {
                args.AdditionalScenes = new[] { "Assets/scene-system/scene-ex/Demo/DemoNextAdditional.unity" };
            }
        }
    }
}
#endif