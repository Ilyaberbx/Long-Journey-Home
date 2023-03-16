using System;
using System.Collections;
using Infrastructure.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader
    {
        private ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        => _coroutineRunner.StartCoroutine(LoadScene(name,onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }


            AsyncOperation wait = SceneManager.LoadSceneAsync(name);

            while (!wait.isDone)
              yield return null;

            onLoaded?.Invoke();
        }
    }
}
