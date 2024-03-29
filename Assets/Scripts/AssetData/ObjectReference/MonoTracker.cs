﻿using UnityEngine;

namespace AssetManagement
{
    public class MonoTracker : MonoBehaviour
    {
        public delegate void DelegateDestroyed(MonoTracker tracker);
        public event DelegateDestroyed OnDestroyed;

        public object key { get; set; }

        void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}