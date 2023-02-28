using Infrastructure.Services;
using Interfaces;
using Logic.Enemy;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += e => SafetySave();

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= e => SafetySave();

        private void SafetySave()
            => ServiceLocator.Container.Single<ISaveLoadService>().SaveProgress();
    }
}