using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Menu
{
    public class NewGameButton : BaseButton
    {
        [SerializeField] private LoadLastSaveButton _lastSaveButton;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoad;

        [Inject]
        public void Construct(IPersistentProgressService progressService,ISaveLoadService saveLoad)
        {
            _progressService = progressService;
            _saveLoad = saveLoad;
        }

        public override void Execute()
        {
            _saveLoad.CleanUpProgress();
            _progressService.PlayerProgress = _progressService.DefaultProgress();
            _lastSaveButton.Execute();
        }
    }
}