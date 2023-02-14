using Interfaces;
using Logic;
using Logic.Camera;
using Logic.Player;
using Logic.Spawners;
using ProjectSolitude.Enum;
using SceneManagment;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitPointTag = "PlayerInitPoint";
        private const string PovPoint = "POVPoint";
        private const string EnemySpawnerTag = "EnemySpawner";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string payLoad)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit() 
            => _loadingCurtain.Hide();
        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            InitSpawners();
            var player = InitPlayer();
            InitHud(player);
            var camera = CameraFollowPlayer(GameObject.FindGameObjectWithTag(PovPoint).transform);
            InjectCameraAnimator(player, camera);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InjectCameraAnimator(GameObject player, GameObject camera)
        {
            player.GetComponent<HeroHealth>().Construct(camera.GetComponentInParent<ICameraAnimator>());
            player.GetComponent<HeroDeath>().Construct(camera.GetComponentInParent<ICameraAnimator>());
        }

        private void InitHud(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHud();
            PlayerUIActor uiActor = hud.GetComponent<PlayerUIActor>();
            uiActor.Construct(player.GetComponent<HeroHealth>(),player.GetComponentInChildren<FlashLight>());
        }

        private GameObject InitPlayer()
        {
            var initPoint = GameObject.FindGameObjectWithTag(PlayerInitPointTag);
            GameObject player = _gameFactory.CreatePlayer(initPoint.transform.position);
            return player;
        }

        private GameObject CameraFollowPlayer(Transform target)
        {
            var cameraChanger = Camera.main.
                GetComponentInParent<GameCamerasChanger>();
            return cameraChanger.ConstructCamera(GameCameraType.PlayerCamera, target,true);
        }
        
    }
}
