using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetManagement
{
    [Serializable]
    public class ObjectReference
    {
        #region Fields
        
        [DrawWithUnity]
        [SerializeField] private AssetReference reference;

        #endregion
        
        #region Properties
        
        public AssetReference Reference
        {
            get { return reference; }
        }
        
        #endregion
        
        #region Class life cycle
        
        public ObjectReference(AssetReference assetReference)
        {
            reference = assetReference;
        }
        
        #endregion
    }
}