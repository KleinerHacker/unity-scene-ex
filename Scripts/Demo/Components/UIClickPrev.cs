#if DEMO
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Parameters;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime;
using UnitySceneEx.Runtime.scene_system.scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Components
{
    public sealed class UIClickPrev : UIBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        protected override void Start()
        {
            text.text = SceneParameter.Get<DemoNextParameter>().Text;
        }
        
        public void HandleClickPrev()
        {
            SceneController.Singleton.Load("prev", new DemoPrevParameter {Text = "Hello"});
        }
    }
}
#endif