#if DEMO
using System;
using TMPro;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.Projects.unity_scene_ex.Scripts.Demo
{
    [DisallowMultipleComponent]
    public sealed class DemoWaiterWorldFade : WorldFade
    {
        #region Inspector Data

        [SerializeField] private CanvasGroup root;
        [SerializeField] private TextMeshProUGUI text;

        #endregion

        #region Builtin Methods

        private void Awake()
        {
            root.alpha = 0f;
        }

        private void LateUpdate()
        {
            if (State == WorldFadeState.Completed && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                Finish();
            }
        }

        #endregion
        
        protected override void DoShow(string worldKey, Action onComplete)
        {
            root.alpha = 0f;
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => root.alpha = x)
                .WithFinisher(onComplete)
                .Start();
        }

        protected override void DoShowImmediately(string worldKey)
        {
            root.alpha = 1f;
        }

        protected override void DoProgressUpdated(string worldKey, float progress)
        {
            text.text = progress.ToString("F2") + " %";
        }

        protected override void DoProgressCompleted(string worldKey)
        {
            text.text = "Press any key to continue...";
        }

        protected override void DoHide(string worldKey, Action onComplete)
        {
            root.alpha = 1f;
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => root.alpha = 1f - x)
                .WithFinisher(onComplete)
                .Start();
        }

        protected override void DoHideImmediately(string worldKey)
        {
            root.alpha = 0f;
        }
    }
}
#endif