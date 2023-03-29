using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class ServiceLocator 
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Container => _instance ??= new ServiceLocator();

        public void RegisterService<TService>(TService implementation)  where TService : IService
            => Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService
            => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
        
    }
    
}