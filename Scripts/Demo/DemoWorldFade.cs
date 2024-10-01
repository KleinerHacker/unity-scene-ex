using System;
using UnityEngine;
using UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components;

namespace UnitySceneEx.Demo.Projects.unity_scene_ex.Scripts.Demo
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public sealed class DemoWorldFade : WorldFade
    {
        public override void Show(string worldKey, Action onComplete)
        {
        }

        public override void ShowImmediately(string worldKey)
        {
        }

        public override void OnProgressUpdated(float progress)
        {
        }

        public override void OnProgressCompleted(string worldKey)
        {
        }

        public override void Hide(string worldKey, Action onComplete)
        {
        }

        public override void HideImmediately(string worldKey)
        {
        }
    }
}