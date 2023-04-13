using UnityEngine;
using Zenject;

namespace Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
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
            return _container.InstantiatePrefab(prefab, at ,Quaternion.identity,null);
        }
    }
}