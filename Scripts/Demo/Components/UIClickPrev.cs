using UnityEngine.EventSystems;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Components
{
    public sealed class UIClickPrev : UIBehaviour
    {
        public void HandleClickPrev()
        {
            SceneController.Singleton.Load("prev");
        }
    }
}