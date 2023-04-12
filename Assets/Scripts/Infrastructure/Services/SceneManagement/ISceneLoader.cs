using System;

namespace Infrastructure.Services.SceneManagement
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}