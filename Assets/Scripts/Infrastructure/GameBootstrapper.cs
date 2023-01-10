using ProjectSolitude.Interfaces;
using ProjectSolitude.Logic;
using UnityEngine;

namespace ProjectSolitude.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}
