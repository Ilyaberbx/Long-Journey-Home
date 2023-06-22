using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic.Common
{
    public class ToggleJump : MonoBehaviour
    {
        [SerializeField] private bool _toggleJumpValue;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += DisableJumpAbility;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= DisableJumpAbility;

        private void DisableJumpAbility(Collider player)
        {
            if (player.TryGetComponent(out HeroMover mover)) 
                mover.CanJump = _toggleJumpValue;
        }
    }
}