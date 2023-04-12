using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Logic.Enemy;
using UnityEngine;
using Zenject;

namespace Logic.Triggers
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private bool _isSaved;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService) 
            => _saveLoadService = saveLoadService;

        private void Awake()
            => _triggerObserver.OnTriggerEntered += e => Save();

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= e => Save();

        private void Save()
        {
            if (_isSaved) return;

            _saveLoadService.SaveProgress();
            _isSaved = true;
        }
    }
}