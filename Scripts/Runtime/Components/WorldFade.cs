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

        #region Properties

        internal bool AutoFinish => autoFinish;
        protected float Progress { get; private set; }
        protected WorldFadeState State { get; private set; } = WorldFadeState.Idle;

        #endregion

        #region Events

        internal event EventHandler OnFinished;

        #endregion

        #region Builtin Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            switch (isShown)
            {
                case true when !show:
                    HideImmediatelyInEditor();
                    isShown = false;
                    break;
                case false when show:
                    ShowImmediatelyInEditor();
                    isShown = true;
                    break;
            }
        }
#endif

        #endregion

        public void Show(string worldKey, Action onComplete)
        {
            if (State != WorldFadeState.Idle)
                return;

            State = WorldFadeState.Showing;
            DoShow(worldKey, () =>
            {
                State = WorldFadeState.Waiting;
                onComplete?.Invoke();
            });
        }

        public void ShowImmediately(string worldKey)
        {
            if (State != WorldFadeState.Idle)
                return;

            State = WorldFadeState.Showing;
            try
            {
                DoShowImmediately(worldKey);
            }
            finally
            {
                State = WorldFadeState.Waiting;
            }
        }
#if UNITY_EDITOR
        public void ShowImmediatelyInEditor()
        {
            DoShowImmediatelyInEditor();
        }
#endif

        public void OnProgressUpdated(string worldKey, float progress)
        {
            Progress = progress;
            State = WorldFadeState.InProgress;
            
            DoProgressUpdated(worldKey, progress);
        }

        public void OnProgressCompleted(string worldKey)
        {
            State = WorldFadeState.Completed;
            try
            {
                DoProgressCompleted(worldKey);
            }
            finally
            {
                if (autoFinish)
                {
                    State = WorldFadeState.Finished;
                }
            }
        }

        public void Hide(string worldKey, Action onComplete)
        {
            State = WorldFadeState.Hiding;
            DoHide(worldKey, () =>
            {
                State = WorldFadeState.Disposable;
                onComplete?.Invoke();
            });
        }

        public void HideImmediately(string worldKey)
        {
            State = WorldFadeState.Hiding;
            try
            {
                DoHideImmediately(worldKey);
            }
            finally
            {
                State = WorldFadeState.Disposable;
            }
        }
#if UNITY_EDITOR
        public void HideImmediatelyInEditor()
        {
            DoHideImmediatelyInEditor();
        }
#endif

        /// <summary>
        /// Call this method if <see cref="autoFinish"/> is set to <c>false</c> if loading is done.
        /// </summary>
        protected void Finish()
        {
            if (State == WorldFadeState.Finished)
                return;

            State = WorldFadeState.Finished;
            OnFinished?.Invoke(this, EventArgs.Empty);
        }

        #region Override Section

        protected abstract void DoShow(string worldKey, Action onComplete);
        protected abstract void DoShowImmediately(string worldKey);
#if UNITY_EDITOR
        protected virtual void DoShowImmediatelyInEditor()
        {
            DoShowImmediately("");
        }
#endif

        protected virtual void DoProgressUpdated(string worldKey, float progress)
        {
        }

        protected virtual void DoProgressCompleted(string worldKey)
        {
        }

        protected abstract void DoHide(string worldKey, Action onComplete);
        protected abstract void DoHideImmediately(string worldKey);
#if UNITY_EDITOR
        protected virtual void DoHideImmediatelyInEditor()
        {
            DoHideImmediately("");
        }
#endif

        #endregion
    }

    public enum WorldFadeState
    {
        Idle,
        Showing,
        Waiting,
        InProgress,
        Completed,
        Finished,
        Hiding,
        Disposable,
    }
}