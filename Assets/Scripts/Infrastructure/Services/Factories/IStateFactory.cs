using System;
using Infrastructure.Interfaces;
using Infrastructure.StateMachine;

namespace Infrastructure.Services.Factories
{
    public interface IStateFactory
    {
        IExitableState Create(IGameStateMachine machine, Type type);
    }
}