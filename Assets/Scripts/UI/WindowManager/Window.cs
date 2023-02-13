using UnityEngine;

namespace Game.Ui.WindowManager
{
    [DisallowMultipleComponent]
    public abstract class Window : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------------------
        // MonoBehaviours methods

        void Awake()
        {
            WindowsManager.OnWindowOpenStarted += WindowOpenStartedHandler;
            WindowsManager.OnWindowOpenCompleted += WindowOpenCompletedHandler;
            WindowsManager.OnWindowCloseStarted += WindowCloseStartedHandler;
            WindowsManager.OnWindowCloseCompleted += OnWindowCloseCompletedHandler;
        }

        void OnDestroy()
        {
            WindowsManager.OnWindowOpenStarted -= WindowOpenStartedHandler;
            WindowsManager.OnWindowOpenCompleted -= WindowOpenCompletedHandler;
            WindowsManager.OnWindowCloseStarted -= WindowCloseStartedHandler;
            WindowsManager.OnWindowCloseCompleted -= OnWindowCloseCompletedHandler;
        }

        //--------------------------------------------------------------------------------------------------------------------------
        // Properties

        public WindowAnimation OpenWindowAnimation { get; protected set; }
        public WindowAnimation CloseWindowAnimation { get; protected set; }

        public WindowArguments Arguments { get; private set; }
        public virtual bool IsPoolable => false;

        //--------------------------------------------------------------------------------------------------------------------------
        // Public methods

        public void Init(WindowArguments arguments) => Arguments = arguments;
        public void Close() => WindowsManager.Instance.CloseWindow(this);

        //--------------------------------------------------------------------------------------------------------------------------

        private void WindowOpenStartedHandler(Window window) { if (window == this) OnOpenStarted(); }
        private void WindowOpenCompletedHandler(Window window) { if (window == this) OnOpenCompleted(); }
        private void WindowCloseStartedHandler(Window window) { if (window == this) OnCloseStarted(); }
        private void OnWindowCloseCompletedHandler(Window window) { if (window == this) OnCloseCompleted(); }

        //--------------------------------------------------------------------------------------------------------------------------
        // Virtual methods

        protected virtual void OnOpenStarted() { /* empty */ }
        protected virtual void OnOpenCompleted() { /* empty */ }
        protected virtual void OnCloseStarted() { /* empty */ }
        protected virtual void OnCloseCompleted() { /* empty */ }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}