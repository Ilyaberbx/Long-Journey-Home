using Logic.Enemy;
using UnityEngine;

namespace Logic.DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private TriggerObserver _triggerObserver;

        private bool _isViewed;
        private void Awake() 
            => _triggerObserver.OnTriggerEntered += Trigger;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Trigger;

        private void Trigger(Collider obj)
        {
            if (_isViewed) return;

            if (obj.TryGetComponent(out IDialogueActor actor))
            {
                actor.StartDialogue(_dialogue);
                _isViewed = true;
            }

        }
    }
}