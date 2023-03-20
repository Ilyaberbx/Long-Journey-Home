using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using UI.Elements;
using UI.Services.Window;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;
        private IPersistentProgressService _progressService;

        public UIFactory(IAssetProvider assets,IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateInventory()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Inventory);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() 
            => _uiRoot =_assets.Instantiate(AssetsPath.UIRoot).transform;
    }
}