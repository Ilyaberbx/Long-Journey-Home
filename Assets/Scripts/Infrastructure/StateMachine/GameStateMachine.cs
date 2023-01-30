using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.StateMachine.State;
using Interfaces;
using Logic;
using SceneManagment;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IExitableState> _statesMap;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, ServiceLocator serviceLocator)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);

            _statesMap = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this,sceneLoader, serviceLocator),
                
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader, loadingCurtain, 
                    serviceLocator.Single<IGameFactory>(),
                    serviceLocator.Single<IPersistentProgressService>()),
                
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    serviceLocator.Single<IPersistentProgressService>(),
                    serviceLocator.Single<ISaveLoadService>()),
                
                
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        public void Enter<TState>() where TState : class,IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState,TPayLoad>(TPayLoad payLoad) where TState : class,IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }
        private TState GetState<TState>() where TState : class, IExitableState 
            => _statesMap[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

    }
}
