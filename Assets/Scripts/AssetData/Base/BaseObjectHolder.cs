using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AssetData
{
    public abstract class BaseObjectHolder<T> : SerializedScriptableObject
    {
        #region Field

        [SerializeField] private Dictionary<string, T> objectReferencesById;

        #endregion

        #region Properties
        
        public Dictionary<string, T> ObjectReferencesById => objectReferencesById;

        #endregion

        public T GetReferenceWithId(string id)
        {
            if (!objectReferencesById.TryGetValue(id, out T reference))
            {
                throw new Exception($"Not available value for key {id}");
            } 
        
            return reference;
        }
    }
}