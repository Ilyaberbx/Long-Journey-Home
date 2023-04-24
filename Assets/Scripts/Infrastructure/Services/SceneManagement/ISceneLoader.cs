using System;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.SceneManagement
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
        void Init(ICoroutineRunner coroutineRunner);
    }
}