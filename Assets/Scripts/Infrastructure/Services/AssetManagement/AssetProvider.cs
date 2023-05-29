using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, AsyncOperationHandle> _completedStaticCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _staticHandles =
            new Dictionary<string, List<AsyncOperationHandle>>();


        public async Task Initialize()
            => await Addressables.InitializeAsync().Task;

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
            if (_completedStaticCache.TryGetValue(label, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as IList<T>;

            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);

            return await RunWithAllCacheOnComplete(handle, label);
        }


        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);
            }

            _completedCache.Clear();
            _handles.Clear();

        }

        public void CleanUpStatic()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _staticHandles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);
            }

            _completedStaticCache.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string key) where T : class
        {
            handle.Completed += completeHandle
                => _completedCache[key] = completeHandle;

            AddHandle(key, handle);

            return await handle.Task;
        }

        private async Task<IList<T>> RunWithAllCacheOnComplete<T>(AsyncOperationHandle<IList<T>> handle, string key)
            where T : class
        {
            handle.Completed += completeHandle
                => _completedStaticCache[key] = completeHandle;

            AddStaticHandle(key, handle);

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


        private void AddStaticHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_staticHandles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _staticHandles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }
    }
}