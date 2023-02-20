using System.Threading.Tasks;
using AssetManagement;
using AssetManagement.Extensions;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFlow.Client.Infrastructure
{
    public class AssetInstantiator : IAssetInstantiator
    {
        public async Task<GameObject> InstantiateAsync(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (!objectRef.TryInstantiateSync(position, rotation, parent, out GameObject instanceGO))
            {
                objectRef.TryInstantiateOrLoadAsync(position, rotation, parent, out AsyncOperationHandle<GameObject> handle);

                await handle.Task;
                instanceGO = handle.Result;
            }
            
            return instanceGO;
        }

        public async Task<GameObject> InstantiateAsync(ObjectReference objectRef, Transform parent = null)
        {
            Vector3 pos = parent == null ? Vector3.zero : parent.position;
            Quaternion rot = parent == null ? Quaternion.identity : parent.rotation;
        
            return await InstantiateAsync(objectRef, pos, rot, parent);
        }

        public async Task<T> InstantiateAsync<T>(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            if (!objectRef.TryInstantiateSync(position, rotation, parent, out T instanceComponent))
            {
                objectRef.TryInstantiateOrLoadAsync(position, rotation, parent, out AsyncOperationHandle<T> handle);

                await handle.Task;
                instanceComponent = handle.Result;
            }
            return instanceComponent;
        }
        
        public async Task<T> InstantiateAsync<T>(ObjectReference objectRef, Transform parent = null) where T : Component
        {
            Vector3 pos = parent == null ? Vector3.zero : parent.position;
            Quaternion rot = parent == null ? Quaternion.identity : parent.rotation;
            
            return await InstantiateAsync<T>(objectRef, pos, rot, parent);
        }
        
        public GameObject Instantiate(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (!objectRef.TryInstantiateSync(position, rotation, parent, out GameObject instanceGO))
            {
                objectRef.TryInstantiateOrLoadAsync(position, rotation, parent, out AsyncOperationHandle<GameObject> handle);

                handle.WaitForCompletion();
                instanceGO = handle.Result;
            }
            return instanceGO;
        }

        public GameObject Instantiate(ObjectReference objectRef, Transform parent = null)
        {
            Vector3 pos = parent == null ? Vector3.zero : parent.position;
            Quaternion rot = parent == null ? Quaternion.identity : parent.rotation;
        
            return Instantiate(objectRef, pos, rot, parent);
        }

        public T Instantiate<T>(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            if (!objectRef.TryInstantiateSync(position, rotation, parent, out T instanceComponent))
            {
                objectRef.TryInstantiateOrLoadAsync(position, rotation, parent, out AsyncOperationHandle<T> handle);

                handle.WaitForCompletion();
                instanceComponent = handle.Result;
            }
            
            return instanceComponent;
        }
        
        public T Instantiate<T>(ObjectReference objectRef, Transform parent = null) where T : Component
        {
            Vector3 pos = parent == null ? Vector3.zero : parent.position;
            Quaternion rot = parent == null ? Quaternion.identity : parent.rotation;
            
            return Instantiate<T>(objectRef, pos, rot, parent);
        }
        
        public T Instantiate<T>(GameObject prefab, Transform parent = null)
        {
            GameObject go = Object.Instantiate(prefab, parent);
            go.transform.localScale = prefab.transform.localScale;

            return go.GetComponent<T>();
        }
        
        public T Instantiate<T>(T prefab, Transform parent = null) where T : Component
        {
            T component = Object.Instantiate(prefab, parent);
            component.transform.localScale = prefab.transform.localScale;
            
            return component;
        }

        public T Instantiate<T>(T prefab, Vector3 position, Transform parent = null) where T : Component
        {
            return Instantiate(prefab, position, Quaternion.identity, parent);
        }
        
        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : Component
        {
            T go = Object.Instantiate(prefab, position, rotation, parent);
            go.transform.localScale = prefab.transform.localScale;

            return go;
        }
        
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject go = Object.Instantiate(prefab, position, rotation, parent);
            go.transform.localScale = prefab.transform.localScale;

            return go;
        }
        
        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            GameObject go = Object.Instantiate(prefab, parent);
            go.transform.localScale = prefab.transform.localScale;

            return go;
        }
    }
}