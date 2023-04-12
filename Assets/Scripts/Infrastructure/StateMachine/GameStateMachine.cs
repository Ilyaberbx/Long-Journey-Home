using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using Logic;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        public Dictionary<Type, IExitableState> StatesMap { get; set; } = new Dictionary<Type, IExitableState>();
        private IExitableState _activeState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState GetState<TState>() where TState : class, IExitableState
            => StatesMap[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }
    }
}