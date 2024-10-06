using System;
using UnityEngine;
#if UNITY_URP
using UnityEngine.Rendering.Universal;
#endif

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components
{
    [DisallowMultipleComponent]
    public abstract class WorldFade : MonoBehaviour
    {
        #region Inspector Data

        [SerializeField] private bool show = false;
        [SerializeField] [HideInInspector] private bool isShown = false;

        [SerializeField]
        [Space]
        [Tooltip("Set automatic finishing of fade if progress is complete or not. " +
                 "This is useful if you want to wait for user input to start next scene(s) only if user is ready.")]
        protected bool autoFinish = true;

        #endregion

#if UNITY_EDITOR
        
#endif

        #region Builtin Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            switch (isShown)
            {
                case true when !show:
                    HideImmediatelyEditor("");
                    isShown = false;
                    break;
                case false when show:
                    ShowImmediatelyEditor("");
                    isShown = true;
                    break;
            }
        }
#endif

        #endregion

        public abstract void Show(string worldKey, Action onComplete);
        public abstract void ShowImmediately(string worldKey);
#if UNITY_EDITOR
        public virtual void ShowImmediatelyEditor(string worldKey)
        {
            ShowImmediately(worldKey);
        }
#endif

        public abstract void OnProgressUpdated(string worldKey, float progress);
        public abstract void OnProgressCompleted(string worldKey);

        public abstract void Hide(string worldKey, Action onComplete);
        public abstract void HideImmediately(string worldKey);
#if UNITY_EDITOR
        public virtual void HideImmediatelyEditor(string worldKey)
        {
            HideImmediately(worldKey);
        }
#endif

        /// <summary>
        /// Call this method if <see cref="autoFinish"/> is set to <c>false</c> if loading is done.
        /// </summary>
        protected void Finish()
        {
        }
    }
}