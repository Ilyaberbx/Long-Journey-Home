using Infrastructure.Services.PersistentProgress;
using Logic.Common;
using Logic.Enemy;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private TriggerObserver _triggerObserver;

        private bool _isViewed;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IPersistentProgressService progressService) 
            => _progressService = progressService;

        private void Awake()
        {
            _isViewed = IsPassed();
            _triggerObserver.OnTriggerEntered += Trigger;
        }

        private bool IsPassed() 
            => _progressService.Progress.DialogueData.Passed.Contains(_uniqueId.Id);

        private void Pass()
            => _progressService.Progress.DialogueData.Passed.Add(_uniqueId.Id);

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Trigger;

        private void Trigger(Collider obj)
        {
            if (_isViewed) return;

            if (!obj.TryGetComponent(out IDialogueActor actor)) return;
            
            actor.StartDialogue(_dialogue);
            _isViewed = true;
            Pass();
        }
    }
}