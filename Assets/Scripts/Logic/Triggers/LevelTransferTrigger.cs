using System;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Enemy;
using Logic.Spawners;
using UnityEngine;

namespace Logic.Triggers
{
    public class LevelTransferTrigger : BaseMarker
    {
        private const string PlayerTag = "Player";

        [SerializeField] private string _transferTo;
        [SerializeField] private TriggerObserver _triggerObserver;
        private IGameStateMachine _stateMachine;
        private bool _triggered;

        private void Awake()
        {
            _stateMachine = ServiceLocator.Container.Single<IGameStateMachine>();
            _triggerObserver.OnTriggerEntered += Triggered;
        }

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Triggered;

        private void Triggered(Collider other)
        {
            if (_triggered)
                return;

            if (!other.CompareTag(PlayerTag)) return;
            
            _stateMachine.Enter<LoadLevelState, string>(_transferTo);
            _triggered = true;
        }
    }
}