using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetData
{
    public abstract class BaseObjectReferenceHolder<T> : ScriptableObject where T : Object
    {
        #region Field

        [SerializeField] private InspectableDictionary<string, T> objectReferencesById;

        #endregion

        #region Properties
        
        public InspectableDictionary<string, T> ObjectReferencesById => objectReferencesById;

        #endregion

        private void OnValidate()
        {
            
        }

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