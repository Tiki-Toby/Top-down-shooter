using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace AssetManagement.Extensions
{
    public static class ObjectReferenceExtension
    {
        public static bool IsLoading(this ObjectReference objectReference)
        {
            return AssetManager.IsLoading(objectReference.Reference);
        }
        
        public static bool IsLoaded(this ObjectReference objectReference)
        {
            return AssetManager.IsLoaded(objectReference.Reference);
        }
        
        public static bool IsInstantiated(this ObjectReference objectReference)
        {
            return AssetManager.IsInstantiated(objectReference.Reference);
        }

        public static int InstantiatedCount(this ObjectReference objectReference)
        {
            return AssetManager.InstantiatedCount(objectReference.Reference);
        }

        public static bool TryGetOrLoadObjectAsync<TObjectType>(this ObjectReference objectReference,
            out AsyncOperationHandle<TObjectType> handle) where TObjectType : Object
        {
            return AssetManager.TryGetOrLoadObjectAsync(objectReference.Reference, out handle, 
                reference => Addressables.LoadAssetAsync<TObjectType>(reference));
        }
        
        public static bool TryGetOrLoadComponentAsync<TComponentType>(this ObjectReference objectReference,
            out AsyncOperationHandle<TComponentType> handle) where TComponentType : Component
        {
            return AssetManager.TryGetOrLoadComponentAsync(objectReference.Reference, out handle, 
                reference => Addressables.LoadAssetAsync<GameObject>(reference));
        }

        public static bool TryGetObjectSync<TObjectType>(this ObjectReference objectReference, out TObjectType result)
            where TObjectType : Object
        {
            return AssetManager.TryGetObjectSync(objectReference.Reference, out result);
        }
        
        public static bool TryGetComponentSync<TComponentType>(this ObjectReference objectReference, 
            out TComponentType result) where TComponentType : Component
        {
            return AssetManager.TryGetComponentSync(objectReference.Reference, out result);
        }

        public static void Unload(this ObjectReference objectReference)
        {
            AssetManager.Unload(objectReference.Reference);
        }

        public static bool TryInstantiateOrLoadAsync(this ObjectReference objectReference, Vector3 position,
            Quaternion rotation, Transform parent, out AsyncOperationHandle<GameObject> handle)
        {
            return AssetManager.TryInstantiateOrLoadAsync(objectReference.Reference, position, rotation, parent,
                out handle);
        }

        public static bool TryInstantiateOrLoadAsync<TComponentType>(this ObjectReference objectReference,
            Vector3 position, Quaternion rotation, Transform parent, out AsyncOperationHandle<TComponentType> handle)
            where TComponentType : Component
        {
            return AssetManager.TryInstantiateOrLoadAsync(objectReference.Reference, position, rotation, parent,
                out handle);
        }

        public static bool TryInstantiateMultiOrLoadAsync(this ObjectReference objectReference, int count, 
            Vector3 position, Quaternion rotation, Transform parent, out AsyncOperationHandle<List<GameObject>> handle)
        {
            return AssetManager.TryInstantiateMultiOrLoadAsync(objectReference.Reference, count, position, rotation,
                parent, out handle);
        }

        public static bool TryInstantiateMultiOrLoadAsync<TComponentType>(this ObjectReference objectReference,
            int count, Vector3 position, Quaternion rotation, Transform parent,
            out AsyncOperationHandle<List<TComponentType>> handle) where TComponentType : Component
        {
            return AssetManager.TryInstantiateMultiOrLoadAsync(objectReference.Reference, count, position, rotation,
                parent, out handle);
        }

        public static bool TryInstantiateSync(this ObjectReference objectReference, Vector3 position, 
            Quaternion rotation, Transform parent, out GameObject result)
        {
            return AssetManager.TryInstantiateSync(objectReference.Reference, position, rotation, parent, out result);
        }

        public static bool TryInstantiateSync<TComponentType>(this ObjectReference objectReference, Vector3 position, 
            Quaternion rotation, Transform parent, out TComponentType result) where TComponentType : Component
        {
            return AssetManager.TryInstantiateSync(objectReference.Reference, position, rotation, parent, out result);
        }

        public static bool TryInstantiateMultiSync(this ObjectReference objectReference, int count, Vector3 position,
            Quaternion rotation, Transform parent, out List<GameObject> result)
        {
            return AssetManager.TryInstantiateMultiSync(objectReference.Reference, count, position, rotation, parent,
                out result);
        }
        
        public static bool TryInstantiateMultiSync<TComponentType>(this ObjectReference objectReference, int count, 
            Vector3 position, Quaternion rotation, Transform parent, out List<TComponentType> result) 
            where TComponentType : Component
        {
            return AssetManager.TryInstantiateMultiSync(objectReference.Reference, count, position, 
                rotation, parent, out result);
        }

        public static void DestroyAllInstances(this ObjectReference objectReference)
        {
            AssetManager.DestroyAllInstances(objectReference.Reference);
        }
        
        public static AsyncOperationHandle DownloadDependencies(this ObjectReference objectReference)
        {
            return AssetManager.DownloadDependencies(objectReference.Reference);
        }

        public static void UnloadDependencies(this ObjectReference objectReference)
        {
            AssetManager.UnloadDependencies(objectReference.Reference);
        }

        public static TObjectType GetObject<TObjectType>(this ObjectReference objectReference) where TObjectType : Object
        {
            if (objectReference.TryGetOrLoadObjectAsync(out AsyncOperationHandle<TObjectType> handle))
            {
                return handle.Result;
            }

            return handle.WaitForCompletion();
        }
    }
}