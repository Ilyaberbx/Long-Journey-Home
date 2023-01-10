using System;
using System.Collections.Generic;
using ProjectSolitude.Infrastructure.AssetManagment;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriter { get; } = new List<ISavedProgressWriter>();
        
        public GameObject HeroGameObject { get; private set; }
        

        public event Action OnHeroCreated;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            HeroGameObject =  InstantiateRegistered(AssetsPath.PlayerPrefabPath, at);
            OnHeroCreated?.Invoke();
            return HeroGameObject;
        }

        public void CreateHud()
            => InstantiateRegistered(AssetsPath.HudPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriter.Clear();
        }
        
        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            GameObject obj = _assetProvider.Instantiate(path, at);
            RegisterProgressWatchers(obj);
            return obj;
        }
        private GameObject InstantiateRegistered(string path)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(obj);
            return obj;
        }

        private void RegisterProgressWatchers(GameObject obj)
        {
            foreach (ISavedProgressReader progressReader in obj.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader obj)
        {
            if(obj is ISavedProgressWriter writer)
                ProgressWriter.Add(writer);
            
            ProgressReaders.Add(obj);
        }
    }
}