using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using UI.Elements;
using UI.Inventory;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;
        private Transform _uiRoot;
        private IPersistentProgressService _progressService;

        public UIFactory(IAssetProvider assets,IStaticDataService staticData,DiContainer container)
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
        }

        public async Task<InventoryWindow> CreateInventory()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Inventory);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            InventoryWindow window = _container.InstantiatePrefabForComponent<InventoryWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task CreateUIRoot()
        {
            GameObject uiRootPrefab = await _assets.Load<GameObject>(AssetsAddress.UIRoot);
            _uiRoot = Object.Instantiate(uiRootPrefab).transform;
        }
    }
}