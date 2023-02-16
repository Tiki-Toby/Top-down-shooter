using System;
using System.Collections.Generic;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    [Serializable]
    public class TargetController
    {
        private Vector2 minSizes;
        
        public Vector3 prevCenter { get; private set; }
        public Vector3 currentCenter { get; private set; }
        public Vector3 velocity => currentCenter - prevCenter;
        public bool IsMovement
        {
            get
            {
                Vector3 deltaCenter = currentCenter - prevCenter;
                deltaCenter.y = 0;
                return deltaCenter.magnitude > 0.02f;
            }
        }

        [SerializeField] List<CameraTarget2D> _targets;

        public TargetController()
        {
            _targets = new List<CameraTarget2D>();
            currentCenter = Vector3.zero;
            prevCenter = Vector3.zero;
            minSizes = Vector3.zero;
        }

        public void Update()
        {
            prevCenter = currentCenter;
            if (_targets.Count == 0)
                return;

            currentCenter = Vector2.zero;
            Vector3 position = _targets[0].TargetPosition;
            Vector2 width = Vector2.one * position.x;
            Vector2 height = Vector2.one * position.y;
            foreach (CameraTarget2D cameraTarget2D in _targets)
            {
                if (cameraTarget2D.TargetTransform == null)
                {
                    _targets.Remove(cameraTarget2D);
                    continue;
                }

                position = cameraTarget2D.TargetPosition;
                currentCenter += cameraTarget2D.TargetPosition;
                if (position.x > width.y)
                    width.y = position.x;
                else if (position.x < width.x)
                    width.x = position.x;

                if (position.y > height.y)
                    height.y = position.y;
                else if (position.y < height.x)
                    height.x = position.y;
            }

            currentCenter /= _targets.Count;
            minSizes.x = width.y - width.x;
            minSizes.x = minSizes.x > 10f ? minSizes.x : 10f;
            minSizes.y = height.y - height.x;
            minSizes.y = minSizes.y > 10f ? minSizes.y : 10f;
        }

        public void AddTarget(Transform target)
        {
            _targets.Add(new CameraTarget2D(target));
            Update();
            prevCenter = currentCenter;
        }
        
        public void SetNewCurrentPosition(Vector3 newCurrentCenterPosition) =>
            currentCenter = newCurrentCenterPosition;
        
        public void RemoveTarget(Transform target)
        {
            foreach(CameraTarget2D cameraTarget in _targets)
                if(cameraTarget.TargetTransform.Equals(target))
                    _targets.Remove(cameraTarget);
        }
    }
}
