#if DEMO
using System;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.Projects.unity_scene_ex.Scripts.Demo
{
    [DisallowMultipleComponent]
    public sealed class Demo3DWorldFade : WorldFade
    {
        #region Inspector Data

        [SerializeField] private CanvasGroup root;

        #endregion

        #region Builtin Methods

        private void Awake()
        {
            root.alpha = 0f;
        }

        #endregion
        
        protected override void DoShow(string worldKey, Action onComplete)
        {
            root.alpha = 0f;
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => root.alpha = x)
                .Wait(1f)
                .WithFinisher(onComplete)
                .Start();
        }

        protected override void DoShowImmediately(string worldKey)
        {
            root.alpha = 1f;
        }

        protected override void DoHide(string worldKey, Action onComplete)
        {
            root.alpha = 1f;
            AnimationBuilder.Create(this)
                .Wait(1f)
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