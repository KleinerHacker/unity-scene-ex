using UnityEngine.EventSystems;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Components
{
    public sealed class UIClickNext : UIBehaviour
    {
        public void HandleNext()
        {
            SceneController.Singleton.Load("next");
        }
    }
}