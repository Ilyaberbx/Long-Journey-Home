using ProjectSolitude.CameraAdditions;
using ProjectSolitude.Enum;
using ProjectSolitude.Infrastructure.PersistentProgress;
using ProjectSolitude.Infrastructure.SceneManagment;
using ProjectSolitude.Interfaces;
using ProjectSolitude.Logic;
using UnityEngine;

namespace ProjectSolitude.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitPointTag = "PlayerInitPoint";
        private const string PovPoint = "POVPoint";
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
            var initPoint = GameObject.FindGameObjectWithTag(PlayerInitPointTag);
            _gameFactory.CreatePlayer(initPoint.transform.position);
            _gameFactory.CreateHud();

            CameraFollowPlayer(GameObject.FindGameObjectWithTag(PovPoint).transform);
        }

        private void CameraFollowPlayer(Transform target)
        {
            var cameraChanger = Camera.main.
                GetComponentInParent<GameCamerasChanger>();
            cameraChanger.SetCamera(GameCameraType.PlayerCamera, target,true);
        }
        
    }
}
