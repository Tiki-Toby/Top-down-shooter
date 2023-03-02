using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HairyEngine.HairyCamera
{
    [Serializable]
    public class TargetController
    {
        [SerializeField] List<CameraTarget> targets;
        private Vector2 _minSizes;
        
        public Vector3 PrevCenter { get; private set; }
        public Vector3 CurrentCenter { get; private set; }
        public Vector3 Velocity => CurrentCenter - PrevCenter;

        public Vector2 MinScreenSize => _minSizes;
        public float OrthographicSize => Mathf.Max(_minSizes.y, _minSizes.x * Screen.currentResolution.height / Screen.currentResolution.width / 1.7f);
        
        public bool IsMovement
        {
            get
            {
                Vector3 deltaCenter = CurrentCenter - PrevCenter;
                deltaCenter.y = 0;
                return deltaCenter.magnitude > 0.02f;
            }
        }


        public TargetController()
        {
            targets = new List<CameraTarget>();
            CurrentCenter = Vector3.zero;
            PrevCenter = Vector3.zero;
            _minSizes = Vector3.zero;
        }

        public void Update()
        {
            PrevCenter = CurrentCenter;
            if (targets.Count == 0)
                return;

            CurrentCenter = Vector2.zero;
            Vector3 position = targets[0].TargetPosition;
            Vector2 width = Vector2.one * position.x;
            Vector2 height = Vector2.one * position.y;
            
            foreach (CameraTarget cameraTarget2D in targets)
            {
                if (cameraTarget2D.TargetTransform == null)
                {
                    targets.Remove(cameraTarget2D);
                    continue;
                }

                position = cameraTarget2D.TargetPosition;
                CurrentCenter += cameraTarget2D.TargetPosition;
                
                if (position.x > width.y)
                    width.y = position.x;
                else if (position.x < width.x)
                    width.x = position.x;

                if (position.y > height.y)
                    height.y = position.y;
                else if (position.y < height.x)
                    height.x = position.y;
            }

            CurrentCenter /= targets.Count;
            _minSizes.x = width.y - width.x;
            _minSizes.y = height.y - height.x;
        }

        public void AddTarget(Transform target)
        {
            targets.Add(new CameraTarget(target));
            Update();
            PrevCenter = CurrentCenter;
        }
        
        public void SetNewCurrentPosition(Vector3 newCurrentCenterPosition) =>
            CurrentCenter = newCurrentCenterPosition;
        
        public void RemoveTarget(Transform target)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].TargetTransform.Equals(target))
                {
                    targets.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
