using System;
using Infrastructure.Interfaces;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.Services.Factories
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
            => _container = container;

        public IExitableState Create(IGameStateMachine machine, Type type)
        {
            IExitableState state = _container.Instantiate(type) as IExitableState;
            machine.StatesMap.Add(type,state);
            return state;
        }
    }
}