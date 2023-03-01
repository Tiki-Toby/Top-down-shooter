using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace HairyEngine.HairyCamera
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private float high = 10f;
        [SerializeField] private bool followHorizontal = true;
        [SerializeField] private float horizontalFollowSmoothness = 10f;
         
        [SerializeField] private bool followVertical = true;
        [SerializeField] private float verticalFollowSmoothness = 10f;

        [SerializeField] private float cameraTargetSize = 10f;
        [SerializeField] private float zoomLerpSmoothness = 20f;
         
        [SerializeField] private float offsetX;
        [SerializeField] private float offsetY;
        [SerializeField] private bool isOffsetConsider = true;
        
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
        private List<IPositionChanged> _positionChangers;
        private List<IViewSizeChanged> _viewSizeChangers;

        public Camera GameCamera => _gameCamera;
        public TargetController Targets => targetController;
        public float PrevVelocity { get; private set; }
        public Vector3 CameraTargetPosition { get; private set; }
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
            SetScreenSize(cameraTargetSize);
            if (isCenterOnTargetOnStart)
                CenterOnTarget();
        }
        
        public void CenterOnTarget()
        {
            targetController.Update();
            _transform.position = new Vector3(targetController.CurrentCenter.x, targetController.CurrentCenter.y, -high);
        }

        private void Update()
        {
            targetController.Update();
            Move();
            Zoom();
        }
        
        private void Zoom()
        {
            var newTargetSize = Mathf.Max(Targets.OrthographicSize, cameraTargetSize);
            
            foreach (IViewSizeChanged viewSizeChanger in _viewSizeChangers)
            {
                newTargetSize = viewSizeChanger.HandleSizeChanged(newTargetSize);
            }
            
            if (Math.Abs(newTargetSize - ScreenSizeInWorldCoordinates.y) <= 0.001f)
                return;

            float newSize = Mathf.Lerp(ScreenSizeInWorldCoordinates.y, newTargetSize, zoomLerpSmoothness);
            SetScreenSize(newSize);
        }
        
        private void Move()
        {
            _prevCameraPos = _transform.position;

            foreach (IPreMove preMoveAction in _preMoveActions)
                preMoveAction.HandleStartMove(targetController.CurrentCenter);

            if (_exclusiveTargetPosition.HasValue)
            {
                _exclusiveTargetPosition -= _transform.position;
                CameraTargetPosition = _exclusiveTargetPosition.Value;
                _exclusiveTargetPosition = null;
            }
            else
            {
                var cameraTargetPositionX = followHorizontal ? targetController.CurrentCenter.x : _transform.position.x;
                var cameraTargetPositionY = followVertical ? targetController.CurrentCenter.y : _transform.position.y;
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
            var position = _transform.position;
            var horizontalDeltaMovement = Mathf.Lerp(position.x, CameraTargetPosition.x, horizontalFollowSmoothness * Time.deltaTime);
            var verticalDeltaMovement = Mathf.Lerp(transform.position.y, CameraTargetPosition.y, verticalFollowSmoothness * Time.deltaTime);

            horizontalDeltaMovement -= position.x;
            verticalDeltaMovement -= position.y;
            var deltaPosition = new Vector3(horizontalDeltaMovement, verticalDeltaMovement);

            PrevVelocity = deltaPosition.magnitude;

            Vector3 newPosition = position + deltaPosition;
            foreach (IPositionChanged positionChanger in _positionChangers)
                newPosition = positionChanger.HandlePositionChange(newPosition);

            _transform.position = newPosition;
        }
        
        void SetScreenSize(float newSize)
        {
            if (GameCamera.orthographic)
            {
                GameCamera.orthographicSize = newSize;
            }
            else
            {
                var localPosition = _transform.localPosition;
                localPosition = new Vector3(
                    localPosition.x,
                    localPosition.y,
                    newSize / Mathf.Tan(GameCamera.fieldOfView * 0.5f * Mathf.Deg2Rad));
                
                _transform.localPosition = localPosition;
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
            cameraTargetSize = hight;
        }
        
        private void InitExtensionDelegates()
        {
            _preMoveActions = SortCameraComponents<IPreMove>();
            _positionChangers = SortCameraComponents<IPositionChanged>();
            _viewSizeChangers = SortCameraComponents<IViewSizeChanged>();
        }
    }
}