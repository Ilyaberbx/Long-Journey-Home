using Extensions;
using Logic.Common;
using Logic.Level;
using UnityEngine;

namespace Logic
{
    public class PlayActionsOnTrigger : MonoBehaviour
    {
        [SerializeField] private bool _disableOnTrigger;
        [SerializeField] private TriggerObserver _triggerObserver;
        private IAction[] _actions;
        
        private void Awake()
        {
            _actions = GetComponentsInChildren<IAction>();
            _triggerObserver.OnTriggerEntered += PlayActions;
        }

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= PlayActions;

        private void PlayActions(Collider collider)
        {
            if(_disableOnTrigger)
                _triggerObserver.Disable();
            
            _actions.ExecuteAll();
        }
    }
}