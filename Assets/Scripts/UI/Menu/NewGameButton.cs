using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Menu
{
    public class NewGameButton : BaseButton
    {
        [SerializeField] private LoadLastSaveButton _lastSaveButton;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IPersistentProgressService progressService) 
            => _progressService = progressService;

        public override void Execute()
        {
            _progressService.ClearUp();
            _lastSaveButton.Execute();
        }
    }
}