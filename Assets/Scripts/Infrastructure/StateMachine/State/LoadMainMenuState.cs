﻿using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.MusicService;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.Settings;
using Logic;
using Logic.Vignette;
using UI.Services.Factory;
using UI.Services.Window;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class LoadMainMenuState : IState
    {
        private const string MainMenu = "MainMenu";
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;
        private readonly ISettingsService _settingsService;
        private readonly IVignetteService _vignetteService;
        private readonly IMusicService _musicService;

        public LoadMainMenuState(IGameStateMachine stateMachine,IWindowService windowService, 
            ISceneLoader sceneLoader,IUIFactory uiFactory, 
            LoadingCurtain loadingCurtain,IPauseService pauseService,ISettingsService settingsService
            ,IVignetteService vignetteService,IMusicService musicService)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _pauseService = pauseService;
            _settingsService = settingsService;
            _vignetteService = vignetteService;
            _musicService = musicService;
        }

        public void Enter()
        {
            _musicService.Stop();
            _pauseService.CleanUp();
            _loadingCurtain.Show();
            _sceneLoader.Load(MainMenu,OnLoaded);
        }

        public void Exit() 
            => _loadingCurtain.Hide();

        private async void OnLoaded()
        {
            _musicService.PlayMusic(MusicType.MainMenu);
            _vignetteService.Reset();
            _pauseService.SetPaused(false);
            _pauseService.CanBePaused = false;
            await _settingsService.Init();
            await _uiFactory.CreateUIRoot();
            await _windowService.Open(WindowType.MainMenu);
            _stateMachine.Enter<GameLoopState>();
        }
    }
}