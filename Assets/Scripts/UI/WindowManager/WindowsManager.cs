using System;
using System.Collections.Generic;
using AssetData;
using Game.Ui.WindowManager.WindowFactory;
using GameFlow.Client.Infrastructure;
using GameLogic.UnitLogic;
using UI.Bars;
using UnityEngine;
using Zenject;

namespace Game.Ui.WindowManager
{
    public class WindowsManager : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------------------
        // Events

        public static event Action<Window> OnWindowOpenStarted;
        public static event Action<Window> OnWindowOpenCompleted;
        public static event Action<Window> OnWindowCloseStarted;
        public static event Action<Window> OnWindowCloseCompleted;

        //--------------------------------------------------------------------------------------------------------------------------
        // Fields

        [SerializeField] private Transform _windowRoot;
        [SerializeField] private GameObject _blockInputGameObject;
        private IWindowFactory _windowFactory;

        private readonly LinkedList<WindowOpenRequest> _openWindowQueue = new LinkedList<WindowOpenRequest>();
        private readonly List<Window> _openedWindows = new List<Window>();
        private readonly Dictionary<Type, Stack<Window>> _windowsPool = new Dictionary<Type, Stack<Window>>();

        //--------------------------------------------------------------------------------------------------------------------------
        // Mono behaviours methods and singletone

        public static WindowsManager Instance { get; private set; }

        [Inject]
        public void Construct(IGameAssetData gameAssetData, IAssetInstantiator assetInstantiator)
        {
            _windowFactory =  new WindowAssetFactory(gameAssetData, assetInstantiator);;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Warning! Multiple WindowManagers detected!");
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            if (_openedWindows.Count > 0) return;

            var topWindowQueue = _openWindowQueue.First;

            var callback = topWindowQueue?.Value.allowOpenCallback;
            while (callback != null && callback.Invoke() == false)
            {
                topWindowQueue = topWindowQueue.Next;
                callback = topWindowQueue?.Value.allowOpenCallback;
            }

            if (topWindowQueue == null) return;
            var topWindowOpenRequest = topWindowQueue.Value;
            if (topWindowOpenRequest == null) return;

            _openWindowQueue.Remove(topWindowQueue);
            DoOpenWindow(topWindowOpenRequest);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        //--------------------------------------------------------------------------------------------------------------------------
        // Setup

        public void Setup(Transform windowsRoot, GameObject blockInputGameObject, IWindowFactory factory)
        {
            SetWindowsRoot(windowsRoot);
            SetBlockInputGameObject(blockInputGameObject);
            SetWindowFactory(factory);
        }

        public void SetWindowsRoot(Transform windowsRoot) => _windowRoot = windowsRoot;

        public void SetBlockInputGameObject(GameObject blockInputGameObject) =>
            _blockInputGameObject = blockInputGameObject;

        public void SetWindowFactory(IWindowFactory factory) => _windowFactory = factory;

        public bool IsBusy => _openedWindows.Count + _openWindowQueue.Count > 0;

        public bool IsAnyWindowOpened => _openedWindows.Count > 0;

        public bool IsAnyWindowQueued => _openWindowQueue.Count > 0;
        //--------------------------------------------------------------------------------------------------------------------------
        // Open window logic

        public void Open(Type windowType, WindowArguments arguments = null, Func<bool> allowOpenCallback = null)
        {
            var openType = arguments?.OpenTypeValue ?? WindowArguments.OpenType.Default;
            var windowOpenRequest = new WindowOpenRequest(windowType, true, arguments, allowOpenCallback);

            switch (openType)
            {
                case WindowArguments.OpenType.Default:
                    _openWindowQueue.AddLast(windowOpenRequest);
                    break;
                case WindowArguments.OpenType.OnTop:
                    DoOpenWindow(windowOpenRequest);
                    break;
                case WindowArguments.OpenType.HighPriority:
                    _openWindowQueue.AddFirst(windowOpenRequest);
                    break;
            }
        }

        public void Open<T>(WindowArguments arguments = null, Func<bool> allowOpenCallback = null) where T : Window
        {
            Open(typeof(T), arguments, allowOpenCallback);
        }

        public Window GetFirstOpenedWindowOfType(string typeName)
        {
            foreach (var openedWindow in _openedWindows)
            {
                if (openedWindow.GetType().Name.Equals(typeName)) return openedWindow;
            }

            return null;
        }

        public T GetFirstOpenedWindowOfType<T>() where T : Window
        {
            foreach (var openedWindow in _openedWindows)
            {
                if (openedWindow is T castedWindow) return castedWindow;
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private void DoOpenWindow(WindowOpenRequest topWindowOpenRequest)
        {
            var window = GetWindow(topWindowOpenRequest.windowType);
            if (window == null)
            {
                Debug.LogWarning("Warning! Can't open window for request: " + topWindowOpenRequest);
                return;
            }

            window.Init(topWindowOpenRequest.arguments);

            window.transform.SetAsLastSibling();

            _openedWindows.Add(window);
            OnWindowOpenStarted?.Invoke(window);

            if (window.OpenWindowAnimation == null) OnWindowOpenCompleted?.Invoke(window);
            else
            {
                LockInput();
                window.OpenWindowAnimation.Animate(window, _ =>
                {
                    UnlockInput();
                    OnWindowOpenCompleted?.Invoke(window);
                });
            }
        }

        private Window GetWindow(Type windowType)
        {
            if (_windowsPool.TryGetValue(windowType, out var pooledWindows))
            {
                if (pooledWindows.Count > 0)
                {
                    var window = pooledWindows.Pop();
                    window.gameObject.SetActive(true);
                    return window;
                }
            }

            return _windowFactory.Create(windowType, _windowRoot);
        }

        //--------------------------------------------------------------------------------------------------------------------------
        // Close window logic

        public void CloseAll() => _openedWindows.ForEach(CloseWindow);

        public void CloseWindow(Window window)
        {
            if (!_openedWindows.Contains(window))
            {
                Debug.LogWarning("Warning! Tried to close not opened or unknown window!");
                return;
            }

            OnWindowCloseStarted?.Invoke(window);
            LockInput();
            if (window.CloseWindowAnimation == null) HandleWindowClose(window);
            else window.CloseWindowAnimation.Animate(window, HandleWindowClose);
        }

        private void HandleWindowClose(Window window)
        {
            UnlockInput();
            _openedWindows.Remove(window);
            OnWindowCloseCompleted?.Invoke(window);

            if (window.IsPoolable)
            {
                window.gameObject.SetActive(false);

                var windowType = window.GetType();
                if (_windowsPool.TryGetValue(windowType, out var windowsPool))
                {
                    windowsPool.Push(window);
                }
                else
                {
                    var stack = new Stack<Window>();
                    stack.Push(window);
                    _windowsPool.Add(windowType, stack);
                }
            }
            else Destroy(window.gameObject);
        }

        //--------------------------------------------------------------------------------------------------------------------------
        // Block input logic

        private int _lockInputCounter;

        public bool IsInputLocked => _lockInputCounter > 0;

        public void LockInput()
        {
            if (_lockInputCounter == 0) _blockInputGameObject.SetActive(true);
            _lockInputCounter++;
        }

        public void UnlockInput()
        {
            if (_lockInputCounter == 0)
            {
                Debug.LogWarning("Warning! Trying to unlock not locked input!");
                return;
            }

            _lockInputCounter--;
            if (_lockInputCounter == 0) _blockInputGameObject.SetActive(false);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}