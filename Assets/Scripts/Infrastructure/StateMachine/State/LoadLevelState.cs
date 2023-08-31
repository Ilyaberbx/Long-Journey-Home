using System.Threading.Tasks;
using Cinemachine;
using Data;
using Enums;
using Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Pause;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;
using Logic;
using Logic.Animations;
using Logic.Camera;
using Logic.Gravity;
using Logic.Player;
using UI.Elements;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine.State
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PovPoint = "POVPoint";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly IPauseService _pauseService;

        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService persistentProgressService,
            IStaticDataService staticData, IUIFactory uiFactory, IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
            _pauseService = pauseService;
        }

        public async void Enter(string payLoad)
        {
            _gameFactory.CleanUp();
            _pauseService.CleanUp();
            _pauseService.CanBePaused = false;
            await _gameFactory.WarmUp();
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
            _pauseService.CanBePaused = true;
            _pauseService.SetPaused(false);
        }

        private async void OnLoaded()
        {
            _persistentProgressService.PlayerProgress.IsFirstLoad = false;
            _gameFactory.CreateContainerForCreatedObjects();
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot()
            => await _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.PlayerProgress);
        }

        private async Task InitGameWorld()
        {
            await InitSpawners();
            GameObject player = await InitPlayer();
            await InitHud(player);
            await InitDialogueView();
            CinemachineVirtualCamera camera = InitCamera();
            InitPlayerInteractWithCamera(player, camera);
        }

        private async Task InitDialogueView() 
            => await _gameFactory.CreateDialogueView();


        private CinemachineVirtualCamera InitCamera()
        {
            GameCamera inGameCamera = CameraFollowPlayer(GameObject.FindGameObjectWithTag(PovPoint).transform);

            if (inGameCamera is IPauseHandler cameraPause)
                _pauseService.Register(cameraPause);

            return inGameCamera.Camera;
        }

        private async Task InitSpawners()
        {
            LevelData levelData = LevelData();

            foreach (EnemySpawnerData enemySpawnerData in levelData.EnemySpawners)
            {
                await _gameFactory.CreateEnemySpawner(enemySpawnerData.Position, enemySpawnerData.Id,
                    enemySpawnerData.EnemyType, enemySpawnerData.IsRegisterInContainer);
            }

            foreach (LootSpawnerData lootSpawnerData in levelData.LootSpawners)
                await _gameFactory.CreateLootSpawner(lootSpawnerData.Position, lootSpawnerData.Id,
                    lootSpawnerData.Rotation, lootSpawnerData.Prefab);
        }

        private LevelData LevelData()
        {
            string sceneKey = CurrentLevelName();
            LevelData levelData = _staticData.GetLevelData(sceneKey);
            return levelData;
        }

        private void InitPlayerInteractWithCamera(GameObject player, CinemachineVirtualCamera camera)
        {
            ICameraAnimator cameraAnimator = camera.GetComponentInParent<ICameraAnimator>();
            player.GetComponent<HeroHealth>().Construct(cameraAnimator);
            player.GetComponent<Gravity>().Construct(cameraAnimator);
            player.GetComponent<HeroDeath>().SetCameraAnimator(camera.GetComponentInParent<ICameraAnimator>());
            player.GetComponent<HeroCameraWrapper>().Init(camera.GetCinemachineComponent<CinemachinePOV>());
            ApplyEquipmentToCamera(player, camera);
        }

        private void ApplyEquipmentToCamera(GameObject player, CinemachineVirtualCamera camera)
        {
            Transform equipment = player.GetComponent<HeroEquiper>().EquipmentContainer;
            equipment.SetParent(camera.transform,true);
            equipment.localPosition = Vector3.zero.AddZ(2);
        }

        private async Task InitHud(GameObject player)
        {
            GameObject hud = await _gameFactory.CreateHud();
            PlayerUIActor uiActor = hud.GetComponent<PlayerUIActor>();
            uiActor.Construct(
                player.GetComponent<HeroHealth>(),
                player.GetComponent<HeroLight>(),
                player.GetComponent<IFreeze>(),
                player.GetComponent<IInteractor>());
            
            player.GetComponent<HeroHudWrapper>()
                .SetHud(hud.GetComponent<IHud>());
        }

        private async Task<GameObject> InitPlayer()
        {
            LevelData levelData = LevelData(); 
            Vector3 initPoint = levelData.PlayerInitPoint;
            GameObject player = await _gameFactory.CreatePlayer(initPoint);
            return player;
        }

        private GameCamera CameraFollowPlayer(Transform player)
        {
            CameraService cameraChangerService = Camera.main.GetComponentInParent<CameraService>();
            return cameraChangerService.ConstructCamera(GameCameraType.PlayerCamera, player, true);
        }

        private string CurrentLevelName()
            => SceneManager.GetActiveScene().name;
    }
}