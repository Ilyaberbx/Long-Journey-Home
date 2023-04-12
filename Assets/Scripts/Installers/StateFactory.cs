using System;
using Infrastructure.Interfaces;
using Infrastructure.StateMachine;
using Zenject;

namespace Installers
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
            => _container = container;

        public IExitableState Create(IGameStateMachine machine, Type type)
        {
            IExitableState state = _container.Instantiate(type, new[] { machine }) as IExitableState;
            machine.StatesMap.Add(type,state);
            return state;
        }
    }
}