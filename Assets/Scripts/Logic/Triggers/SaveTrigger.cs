using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Triggers
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private bool _isSaved;

        private void Awake()
            => _triggerObserver.OnTriggerEntered += e => SafetySave();

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= e => SafetySave();

        private void SafetySave()
        {
            if (_isSaved) return;

            ServiceLocator.Container.Single<ISaveLoadService>().SaveProgress();
            _isSaved = true;
        }
    }
}