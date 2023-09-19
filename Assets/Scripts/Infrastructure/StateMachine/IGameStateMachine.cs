using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Infrastructure.StateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayloadedState<TPayLoad>;
        Dictionary<Type, IExitableState> StatesMap { get; set; }

        void Enter<TState, TPayLoad, TVPayload>(TPayLoad payLoad, TVPayload secondPayLoad)
            where TState : class, IPayloadedState<TPayLoad, TVPayload>;
    }
}