﻿using System.Threading.Tasks;
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
        private readonly DiContainer _container;
        private Transform _uiRoot;
        private IPersistentProgressService _progressService;


        public UIFactory(IAssetProvider assets, IStaticDataService staticData, DiContainer container)
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

        public async Task<MenuWindow> CreateMainMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.MainMenu);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            MenuWindow window = _container.InstantiatePrefabForComponent<MenuWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<SettingsWindow> CreateSettingsWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Settings);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            SettingsWindow window = _container.InstantiatePrefabForComponent<SettingsWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<PauseWindow> CreatePauseMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Pause);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            PauseWindow window = _container.InstantiatePrefabForComponent<PauseWindow>(prefab, _uiRoot);
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
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            EnvelopeWindow window = _container.InstantiatePrefabForComponent<EnvelopeWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<GameOverWindow> CreateGameOverMenu()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.GameOver);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            GameOverWindow window = _container.InstantiatePrefabForComponent<GameOverWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<EndingWindow> CreateEndingWindow()
        {
            WindowConfig config = _staticData.GetWindowData(WindowType.Ending);
            GameObject prefab = await _assets.Load<GameObject>(config.Prefab);
            EndingWindow window = _container.InstantiatePrefabForComponent<EndingWindow>(prefab, _uiRoot);
            return window;
        }

        public async Task<AchievementView> CreateAchievementView(AchievementType type)
        {
            GameObject prefab = await _assets.Load<GameObject>(AchievementViewKey);
            AchievementView view = _container.InstantiatePrefabForComponent<AchievementView>(prefab);
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