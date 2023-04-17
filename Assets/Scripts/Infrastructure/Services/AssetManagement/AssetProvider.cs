using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        private readonly DiContainer _container;

        public AssetProvider(DiContainer container)
            => _container = container;

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            var key = assetReference.AssetGUID;
            
            if (_completedCache.TryGetValue(key, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            handle.Completed += h
                => _completedCache[assetReference.AssetGUID] = h;

            AddHandle(key, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }

        public void CleanUp()
        {
            foreach (var resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles)
                Addressables.Release(handle);
        }
    }
}