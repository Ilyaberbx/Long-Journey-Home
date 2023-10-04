using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;

namespace Infrastructure.StateMachine.State
{
    public class GameLoopState : IState
    {
        private readonly IAssetProvider _assetProvider;

        public GameLoopState(IGameStateMachine gameStateMachine, IAssetProvider assetProvider) 
            => _assetProvider = assetProvider;

        public void Exit() 
            => _assetProvider.CleanUp();

        public void Enter()
        { }
    }
}