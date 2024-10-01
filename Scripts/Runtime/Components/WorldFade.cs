using System;
using UnityEngine;

namespace UnitySceneEx.Runtime.Projects.unity_scene_ex.Scripts.Runtime.Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public abstract class WorldFade : MonoBehaviour
    {
        #region Inspector Data

        [SerializeField]
        [Tooltip("Set automatic finishing of fade if progress is complete or not. " +
                 "This is useful if you want to wait for user input to start next scene(s) only if user is ready.")]
        protected bool autoFinish = true;

        #endregion
        
        public abstract void Show(string worldKey, Action onComplete);
        public abstract void ShowImmediately(string worldKey);
        
        public abstract void OnProgressUpdated(float progress);
        public abstract void OnProgressCompleted(string worldKey);
        
        public abstract void Hide(string worldKey, Action onComplete);
        public abstract void HideImmediately(string worldKey);

        /// <summary>
        /// Call this method if <see cref="autoFinish"/> is set to <c>false</c> if loading is done.
        /// </summary>
        protected void Finish()
        {
            
        }
    }
}
