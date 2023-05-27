using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.Services.AssetManagement
{
    public class  AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();


        public void Initialize()
            => Addressables.InitializeAsync();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            string key = assetReference.AssetGUID;

            if (_completedCache.TryGetValue(key, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetReference);
            return await RunWithCacheOnComplete(operationHandle, key);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            return await RunWithCacheOnComplete(handle, address);
        }
        
        public async Task<IList<T>> LoadAll<T>(string label) where T : class
        {
            if (_completedCache.TryGetValue(label, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as IList<T>;

            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);

            return await RunWithAllCacheOnComplete(handle, label);
        }


        public void CleanUp()
        {
            foreach (var resourceHandles in _handles.Values)
            {
                foreach (var handle in resourceHandles)
                    Addressables.Release(handle);
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string key) where T : class
        {
            handle.Completed += completeHandle
                => _completedCache[key] = completeHandle;

            AddHandle(key, handle);

            return await handle.Task;
        }
        private async Task<IList<T>> RunWithAllCacheOnComplete<T>(AsyncOperationHandle<IList<T>> handle, string key) where T : class
        {
            handle.Completed += completeHandle
                => _completedCache[key] = completeHandle;

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
    }
}