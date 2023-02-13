using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetData
{
    public abstract class BaseObjectReferenceHolder<T> : ScriptableObject where T : Object
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