#if DEMO
using System;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityBlending.Runtime.scene_system.blending.Scripts.Runtime.Components;
using UnityEngine;

namespace UnitySceneEx.Demo.scene_system.scene_ex.Scripts.Demo.Components
{
    public sealed class DemoBlending : BlendingSystem
    {
        [SerializeField]
        private CanvasGroup group;
        
        public override float LoadingProgress { get; set; }
        public override void ShowBlend(Action onFinished = null)
        {
            @group.alpha = 0f;
            @group.blocksRaycasts = true;
            AnimationBuilder.Create(this)
                .Animate(AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 1f, v => @group.alpha = v)
                .WithFinisher(onFinished)
                .Start();
        }

        public override void ShowBlendImmediately()
        {
            @group.alpha = 1f;
            @group.blocksRaycasts = true;
        }

        public override void HideBlend(Action onFinished = null)
        {
            @group.alpha = 1f;
            @group.blocksRaycasts = true;
            AnimationBuilder.Create(this)
                .Animate(AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 1f, v => @group.alpha = 1f-v, 
                    () => @group.blocksRaycasts = false)
                .WithFinisher(onFinished)
                .Start();
        }

        public override void HideBlendImmediately()
        {
            @group.alpha = 0f;
            @group.blocksRaycasts = false;
        }
    }
}
#endif