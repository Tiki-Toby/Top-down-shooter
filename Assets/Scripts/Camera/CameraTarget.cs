using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    [Serializable]
    public class CameraTarget
    {
        public Vector3 TargetPosition => _target.position;
        public Transform TargetTransform => _target;

        [SerializeField]
        private Transform _target;

        public CameraTarget(Transform target)
        {
            this._target = target;
        }
        public bool Equals(Transform difTarget)
        {
            return _target == difTarget;
        }
    }
}