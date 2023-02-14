using System;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public class BaseCameraScript : MonoBehaviour
    {
        public CameraHandler BaseCameraController
        {
            get
            {
                if (_mainCamera != null) return _mainCamera;

                _mainCamera = GetComponent<CameraHandler>();

                if (_mainCamera == null && Camera.main != null)
                    _mainCamera = CameraHandler.Instance;

                if (_mainCamera == null)
                    throw new Exception("CameraHandler not found!");

                return _mainCamera;
            }
        }

        [SerializeField]
        private CameraHandler _mainCamera;
    }
}
