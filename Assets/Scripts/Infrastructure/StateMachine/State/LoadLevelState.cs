using Cinemachine;
using Data;
using Enums;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;
using Logic;
using Logic.Animations;
using Logic.Camera;
using Logic.DialogueSystem;
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

        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService persistentProgressService,
            IStaticDataService staticData, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
        }

        public void Enter(string payLoad)
        {
            _gameFactory.CleanUp();
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit()
            => _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot()
            => _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject player = InitPlayer();
            InitHud(player);
            CinemachineVirtualCamera camera = CameraFollowPlayer(GameObject.FindGameObjectWithTag(PovPoint).transform)
                .GetComponent<CinemachineVirtualCamera>();
            InitPlayerInteractWithCamera(player, camera);
        }

        private void InitSpawners()
        {
            LevelData levelData = LevelData();

            foreach (EnemySpawnerData enemySpawnerData in levelData.EnemySpawners)
                _gameFactory.CreateEnemySpawner(enemySpawnerData.Position, enemySpawnerData.Id, enemySpawnerData.EnemyType);

            foreach (LootSpawnerData lootSpawnerData in levelData.LootSpawners)
                _gameFactory.CreateLootSpawner(lootSpawnerData.Position, lootSpawnerData.Id, lootSpawnerData.Rotation, lootSpawnerData.Data);
        }

        private LevelData LevelData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelData levelData = _staticData.GetLevelData(sceneKey);
            return levelData;
        }

        private void InitPlayerInteractWithCamera(GameObject player, CinemachineVirtualCamera camera)
        {
            player.GetComponent<HeroHealth>().Construct(camera.GetComponentInParent<ICameraAnimator>());
            player.GetComponent<HeroDeath>().Construct(camera.GetComponentInParent<ICameraAnimator>());
            player.GetComponent<HeroWindowOpener>().Init(camera.GetCinemachineComponent<CinemachinePOV>());
        }

        private void InitHud(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHud();
            PlayerUIActor uiActor = hud.GetComponent<PlayerUIActor>();
            uiActor.Construct(
                player.GetComponent<HeroHealth>(),
                player.GetComponent<HeroLight>(),
                player.GetComponent<IDialogueActor>(),
                player.GetComponent<IFreeze>(),
                player.GetComponent<IInteractor>());
            
        }

        private GameObject InitPlayer()
        {
            LevelData levelData = LevelData();
            Vector3 initPoint = levelData.PlayerInitPoint;
            GameObject player = _gameFactory.CreatePlayer(initPoint);
            return player;
        }

        private GameObject CameraFollowPlayer(Transform target)
        {
            GameCamerasChanger cameraChanger = Camera.main.GetComponentInParent<GameCamerasChanger>();
            return cameraChanger.ConstructCamera(GameCameraType.PlayerCamera, target, true);
        }
    }
}