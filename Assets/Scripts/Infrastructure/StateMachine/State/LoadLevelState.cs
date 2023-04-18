using System.Threading.Tasks;
using Cinemachine;
using Data;
using Enums;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.PersistentProgress;
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

        public async void Enter(string payLoad)
        {
            _gameFactory.CleanUp();
            await _gameFactory.WarmUp();
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit()
            => _loadingCurtain.Hide();

        private async void OnLoaded()
        {
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
            CinemachineVirtualCamera camera = CameraFollowPlayer(GameObject.FindGameObjectWithTag(PovPoint).transform)
                .GetComponent<CinemachineVirtualCamera>();
            InitPlayerInteractWithCamera(player, camera);
        }

        private async Task InitSpawners()
        {
            LevelData levelData = LevelData();

            foreach (EnemySpawnerData enemySpawnerData in levelData.EnemySpawners)
                await _gameFactory.CreateEnemySpawner(enemySpawnerData.Position, enemySpawnerData.Id, enemySpawnerData.EnemyType);

            foreach (LootSpawnerData lootSpawnerData in levelData.LootSpawners)
                await _gameFactory.CreateLootSpawner(lootSpawnerData.Position, lootSpawnerData.Id, lootSpawnerData.Rotation, lootSpawnerData.Data);
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
            player.GetComponent<HeroDeath>().SetCameraAnimator(camera.GetComponentInParent<ICameraAnimator>());
            player.GetComponent<HeroWindowOpener>().Init(camera.GetCinemachineComponent<CinemachinePOV>());
        }

        private async Task InitHud(GameObject player)
        {
            GameObject hud = await _gameFactory.CreateHud();
            PlayerUIActor uiActor = hud.GetComponent<PlayerUIActor>();
            uiActor.Construct(
                player.GetComponent<HeroHealth>(),
                player.GetComponent<HeroLight>(),
                player.GetComponent<IDialogueActor>(),
                player.GetComponent<IFreeze>(),
                player.GetComponent<IInteractor>());
            
        }

        private async Task<GameObject> InitPlayer()
        {
            LevelData levelData = LevelData();
            Vector3 initPoint = levelData.PlayerInitPoint;
            GameObject player = await _gameFactory.CreatePlayer(initPoint);
            return player;
        }

        private GameCamera CameraFollowPlayer(Transform target)
        {
            GameCamerasChanger cameraChanger = Camera.main.GetComponentInParent<GameCamerasChanger>();
            return cameraChanger.ConstructCamera(GameCameraType.PlayerCamera, target, true);
        }
    }
}