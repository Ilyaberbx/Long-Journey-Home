using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using UI.Services.Window;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets,IStaticDataService _staticData)
        {
            _assets = assets;
            this._staticData = _staticData;
        }

        public void CreateInventory()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Inventory);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateUIRoot() 
            => _uiRoot =_assets.Instantiate(AssetsPath.UIRoot).transform;
    }
}