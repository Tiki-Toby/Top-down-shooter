using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public class CameraHandler : MonoBehaviour
    {
        public float high = 10f;
        public bool followHorizontal = true;
        public float horizontalFollowSmoothness = 10f;

        public bool followVertical = true;
        public float verticalFollowSmoothness = 10f;

        public float zoomFollowSmoothness = 20f;

        public float offsetX;
        public float offsetY;
        public bool isOffsetConsider = true;
        
        [SerializeField] private bool isCenterOnTargetOnStart;
        [SerializeField] private TargetController targetController;

        private Vector3? _exclusiveTargetPosition;
        private List<BaseCameraScript> _cameraScripts;
        private List<Vector3> _influences = new List<Vector3>();

        private static CameraHandler _instance;
        private Transform _transform;
        private Camera _gameCamera;
        private Vector3 _prevCameraPos;

        private List<IPreMove> _preMoveActions;
        private List<IPostMove> _postMoveActions;
        private List<IPostZoom> _postZoomActions;
        private List<IDeltaPositionChanger> _deltaPositionChangers;
        private List<IPositionChanged> _positionChangers;
        private List<IViewSizeDeltaChange> _deltaViewSizeChangers;
        private List<IViewSizeChanged> _viewSizeChangers;

        public Camera GameCamera => _gameCamera;
        public TargetController Targets => targetController;
        public float PrevVelocity { get; private set; }
        public Vector3 CameraTargetPosition { get; private set; }
        public float CameraTargetSize { get; private set; }
        public Vector2 ScreenSizeInWorldCoordinates { get; private set; }
        public Vector3 CameraPosition => _transform.position;
        
        public static CameraHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(CameraHandler)) as CameraHandler;
                return _instance;
            }
        }

        private void Awake()
        {
            _transform = transform;
            _prevCameraPos = _transform.position;
            _instance = this;
            _cameraScripts = new List<BaseCameraScript>();
            _gameCamera = GetComponent<Camera>();
        }
        
        private void Start()
        {
            foreach (BaseCameraScript component in _transform.GetComponents<BaseCameraScript>())
                _cameraScripts.Add(component);
            InitExtensionDelegates();

            CalculateScreenSize();
            CameraTargetSize = 10;
            SetScreenSize(CameraTargetSize);
            if (isCenterOnTargetOnStart)
                CenterOnTarget();
        }
        
        public void CenterOnTarget()
        {
            targetController.Update();
            _transform.position = new Vector3(targetController.currentCenter.x, targetController.currentCenter.y, -high);
        }

        private void Update()
        {
            targetController.Update();
            Move();
            Zoom();
        }
        
        private void Zoom()
        {
            // Cycle through the size delta changers
            var deltaSize = 0f;
            foreach (IViewSizeDeltaChange viewSizeDeltaChange in _deltaViewSizeChangers)
                deltaSize = viewSizeDeltaChange.AdjustSize(deltaSize);

            // Calculate the new size
            var newSize = ScreenSizeInWorldCoordinates.y + deltaSize;
            // Cycle through the size overriders
            foreach (IViewSizeChanged viewSizeChanger in _viewSizeChangers)
            {
                newSize = viewSizeChanger.HandleSizeChanged(newSize);
            }

            // Apply the new size
            if (Math.Abs(newSize - ScreenSizeInWorldCoordinates.y) > 0.001f)
                SetScreenSize(newSize);

            foreach (IPostZoom postZoomAction in _postZoomActions)
            {
                postZoomAction.HandleZoomChange(ScreenSizeInWorldCoordinates);
            }
        }
        
        private void Move()
        {
            _prevCameraPos = _transform.position;

            foreach (IPreMove preMoveAction in _preMoveActions)
                preMoveAction.HandleStartMove(targetController.currentCenter);

            if (_exclusiveTargetPosition.HasValue)
            {
                _exclusiveTargetPosition -= _transform.position;
                CameraTargetPosition = _exclusiveTargetPosition.Value;
                _exclusiveTargetPosition = null;
            }
            else
            {
                var cameraTargetPositionX = followHorizontal ? targetController.currentCenter.x : _transform.position.x;
                var cameraTargetPositionY = followVertical ? targetController.currentCenter.y : _transform.position.y;
                CameraTargetPosition = new Vector3(cameraTargetPositionX, cameraTargetPositionY, -high);
                
                //Calculate influences
                foreach (Vector3 influnce in _influences)
                    CameraTargetPosition += influnce;
                _influences.Clear();
            }
            
            //Add offset
            if(isOffsetConsider)
                CameraTargetPosition += new Vector3(followHorizontal ? offsetX : 0, followVertical ? offsetY : 0);
            
            // Calculate the base delta movement
            var horizontalDeltaMovement = Mathf.Lerp(_transform.position.x, CameraTargetPosition.x, horizontalFollowSmoothness * Time.deltaTime);
            var verticalDeltaMovement = Mathf.Lerp(transform.position.y, CameraTargetPosition.y, verticalFollowSmoothness * Time.deltaTime);

            horizontalDeltaMovement -= _transform.position.x;
            verticalDeltaMovement -= _transform.position.y;
            var deltaPosition = new Vector3(horizontalDeltaMovement, verticalDeltaMovement);

            PrevVelocity = deltaPosition.magnitude;

            foreach (IDeltaPositionChanger positionChanger in _deltaPositionChangers)
                deltaPosition = positionChanger.AdjustDelta(deltaPosition);

            Vector3 newPosition = _transform.position + deltaPosition;
            foreach (IPositionChanged positionChanger in _positionChangers)
                newPosition = positionChanger.HandlePositionChange(newPosition);

            _transform.position = newPosition;

            foreach (IPostMove postMoveAction in _postMoveActions)
                postMoveAction.HandleStopMove(newPosition);
        }
        
        void SetScreenSize(float newSize)
        {
            if (GameCamera.orthographic)
            {
                GameCamera.orthographicSize = newSize;
            }
            else
            {
                _transform.localPosition = new Vector3(
                    _transform.localPosition.x,
                    _transform.localPosition.y,
                    newSize / Mathf.Tan(GameCamera.fieldOfView * 0.5f * Mathf.Deg2Rad));
            }

            ScreenSizeInWorldCoordinates = new Vector2(newSize * GameCamera.aspect, newSize);
        }
        
        public void ApplyInfluence(Vector2 influence)
        {
            if (Time.deltaTime < .0001f || float.IsNaN(influence.x) || float.IsNaN(influence.y))
                return;

            _influences.Add(influence);
        }

        public void AddExtension(BaseCameraScript extension)
        {
            _cameraScripts.Add(extension);
            InitExtensionDelegates();
        }

        public List<T> SortCameraComponents<T>() where T : ICameraComponent
        {
            List<T> components = new List<T>();
            foreach (object cameraScript in _cameraScripts)
                if (cameraScript is T)
                    components.Add((T)cameraScript);
            return components.OrderBy(a => a.PriorityOrder).ToList();
        }

        public void AddTarget(Transform target)
        {
            targetController.AddTarget(target);
        }
        
        private void CalculateScreenSize()
        {
            GameCamera.ResetAspect();
            var p1 = GameCamera.ViewportToWorldPoint(new Vector3(0, 0, GameCamera.nearClipPlane));
            var p2 = GameCamera.ViewportToWorldPoint(new Vector3(1, 0, GameCamera.nearClipPlane));
            var p3 = GameCamera.ViewportToWorldPoint(new Vector3(1, 1, GameCamera.nearClipPlane));

            var width = (p2 - p1).magnitude / 2f;
            var hight = (p3 - p2).magnitude / 2f;
            ScreenSizeInWorldCoordinates = new Vector2(width, hight);
            CameraTargetSize = hight;
        }
        
        private void InitExtensionDelegates()
        {
            _preMoveActions = SortCameraComponents<IPreMove>();
            _postMoveActions = SortCameraComponents<IPostMove>();
            _postZoomActions = SortCameraComponents<IPostZoom>();
            _deltaPositionChangers = SortCameraComponents<IDeltaPositionChanger>();
            _positionChangers = SortCameraComponents<IPositionChanged>();
            _deltaViewSizeChangers = SortCameraComponents<IViewSizeDeltaChange>();
            _viewSizeChangers = SortCameraComponents<IViewSizeChanged>();
        }
    }
}