using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Infrastructure.Services.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async void Load(string name, Action onLoaded = null)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(name);
            await handle.Task;

            onLoaded?.Invoke();
        }
        
    }
}
