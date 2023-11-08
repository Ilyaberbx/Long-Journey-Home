using System.Threading.Tasks;
using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.StaticData;
using UI.Elements;
using UI.Ending;
using UI.Envelope;
using UI.GameOver;
using UI.Inventory;
using UI.Menu;
using UI.Pause;
using UI.Services.Window;
using UI.Settings;
using UI.Tutorial;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string AchievementViewKey = "AchievementPopUp";
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInstantiator _instantiator;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IInstantiator instantiator)
        {
            _assets = assets;
            _staticData = staticData;
            _instantiator = instantiator;
        }

        public async Task<InventoryWindow> CreateInventory()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Inventory);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            InventoryWindow window = _instantiator.InstantiatePrefabForComponent<InventoryWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<MenuWindow> CreateMainMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.MainMenu);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            MenuWindow window = _instantiator.InstantiatePrefabForComponent<MenuWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<SettingsWindow> CreateSettingsWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Settings);
            GameObject prefab = await _assets.Load(config.Prefab);
            SettingsWindow window = _instantiator.InstantiatePrefabForComponent<SettingsWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<PauseWindow> CreatePauseMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Pause);
            GameObject prefab = await _assets.Load(config.Prefab);
            PauseWindow window = _instantiator.InstantiatePrefabForComponent<PauseWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<GameObject> CreateCurtain()
        {
            GameObject eyeCurtain = await _assets.Load<GameObject>(AssetsAddress.EyeCurtain);
            return Object.Instantiate(eyeCurtain, _uiRoot);
        }

        public async Task<EnvelopeWindow> CreateEnvelopeWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Envelope);
            GameObject prefab = await _assets.Load(config.Prefab);
            EnvelopeWindow window = _instantiator.InstantiatePrefabForComponent<EnvelopeWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<GameOverWindow> CreateGameOverMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.GameOver);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            GameOverWindow window = _instantiator.InstantiatePrefabForComponent<GameOverWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<EndingWindow> CreateEndingWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Ending);
            GameObject prefab = await _assets.Load(config.Prefab);
            EndingWindow window = _instantiator.InstantiatePrefabForComponent<EndingWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<TutorialWindow> CreateTutorialWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Tutorial);
            GameObject prefab = await _assets.Load(config.Prefab);
            TutorialWindow window = _instantiator.InstantiatePrefabForComponent<TutorialWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<AchievementView> CreateAchievementView(AchievementType type)
        {
            GameObject prefab = await _assets.Load<GameObject>(AchievementViewKey);
            AchievementView view = _instantiator.InstantiatePrefabForComponent<AchievementView>(prefab);
            AchievementData data = _staticData.GetAchievementData(type);
            AchievementDto dataDto = new AchievementDto() { Icon = data.Icon, Text = data.Text};
            
            view.UpdateUI(dataDto);
            return view;
        }

        public async Task CreateUIRoot()
        {
            GameObject uiRootPrefab = await _assets.Load<GameObject>(AssetsAddress.UIRoot);
            _uiRoot = Object.Instantiate(uiRootPrefab).transform;
        }
    }
}