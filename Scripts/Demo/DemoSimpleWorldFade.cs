#if DEMO
using System;
using TMPro;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.Projects.unity_scene_ex.Scripts.Demo
{
    [DisallowMultipleComponent]
    public sealed class DemoSimpleWorldFade : WorldFade
    {
        #region Inspector Data

        [SerializeField] private CanvasGroup root;
        [SerializeField] private CanvasGroup text;
        
        [SerializeField] private TextMeshProUGUI progress;

        #endregion

        #region Builtin Methods

        private void Awake()
        {
            root.alpha = 0;
            text.alpha = 0;
        }

        #endregion
        
        protected override void DoShow(string worldKey, Action onComplete)
        {
            root.alpha = 0;
            text.alpha = 0;
            
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => root.alpha = x)
                .AnimateConstant(1f, x => text.alpha = x)
                .WithFinisher(onComplete)
                .Start();
        }

        protected override void DoShowImmediately(string worldKey)
        {
            root.alpha = 1f;
            text.alpha = 1f;
        }

        protected override void DoProgressUpdated(string worldKey, float progress)
        {
            this.progress.text = progress + "%";
        }

        protected override void DoProgressCompleted(string worldKey)
        {
            progress.text = "Complete";
        }

        protected override void DoHide(string worldKey, Action onComplete)
        {
            root.alpha = 1f;
            text.alpha = 1f;
            
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => text.alpha = 1f - x)
                .AnimateConstant(1f, x => root.alpha = 1f - x)
                .WithFinisher(onComplete)
                .Start();
        }

        protected override void DoHideImmediately(string worldKey)
        {
            root.alpha = 0;
            text.alpha = 0;
        }
    }
}
#endif