using System;

namespace Infrastructure.Services.EventBus
{
    public interface IEventBusService
    {
        void CleanUp();
        void Subscribe(IGlobalSubscriber subscriber);
        void Unsubscribe(IGlobalSubscriber subscriber);

        void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber;
    }
}