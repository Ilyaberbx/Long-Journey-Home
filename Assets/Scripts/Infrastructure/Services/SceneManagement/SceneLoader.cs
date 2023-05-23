using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async void Load(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(name, LoadSceneMode.Single);

            await handle.Task;

            onLoaded?.Invoke();
        }
        
    }
}
