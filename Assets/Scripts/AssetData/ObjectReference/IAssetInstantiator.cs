using System.Threading.Tasks;
using AssetManagement;
using UnityEngine;

namespace GameFlow.Client.Infrastructure
{
    public interface IAssetInstantiator
    {
        Task<GameObject> InstantiateAsync(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null);
        Task<GameObject> InstantiateAsync(ObjectReference objectRef, Transform parent = null);
        Task<T> InstantiateAsync<T>(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component;
        Task<T> InstantiateAsync<T>(ObjectReference objectRef, Transform parent = null) where T : Component;
        T Instantiate<T>(GameObject prefab, Transform parent = null);
        T Instantiate<T>(T prefab, Transform parent = null) where T : Component;
        T Instantiate<T>(T prefab, Vector3 position, Transform parent = null) where T : Component;

        T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : Component;

        GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
        GameObject Instantiate(GameObject prefab, Transform parent = null);
        GameObject Instantiate(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null);
        GameObject Instantiate(ObjectReference objectRef, Transform parent = null);
        T Instantiate<T>(ObjectReference objectRef, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component;
        T Instantiate<T>(ObjectReference objectRef, Transform parent = null) where T : Component;
    }
}