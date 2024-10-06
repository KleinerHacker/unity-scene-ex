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
        
        public override void Show(string worldKey, Action onComplete)
        {
            root.alpha = 0;
            text.alpha = 0;
            
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => root.alpha = x)
                .AnimateConstant(1f, x => text.alpha = x)
                .WithFinisher(onComplete)
                .Start();
        }

        public override void ShowImmediately(string worldKey)
        {
            root.alpha = 1f;
            text.alpha = 1f;
        }

        public override void OnProgressUpdated(string worldKey, float progress)
        {
            Debug.LogError("PROGRESS: " + progress);
            this.progress.text = progress + "%";
        }

        public override void OnProgressCompleted(string worldKey)
        {
            progress.text = "Complete";
        }

        public override void Hide(string worldKey, Action onComplete)
        {
            root.alpha = 1f;
            text.alpha = 1f;
            
            AnimationBuilder.Create(this)
                .AnimateConstant(1f, x => text.alpha = 1f - x)
                .AnimateConstant(1f, x => root.alpha = 1f - x)
                .WithFinisher(onComplete)
                .Start();
        }

        public override void HideImmediately(string worldKey)
        {
            root.alpha = 0;
            text.alpha = 0;
        }
    }
}
#endif