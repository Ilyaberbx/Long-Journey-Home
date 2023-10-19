using System.Collections;
using Logic.Common;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _follow;
        [SerializeField] private float _followingDuration;

        private bool _isFollowing;
        private Coroutine _aggroCoroutine;
        protected Transform _victimTransform;

        private void Start()
        {
            _triggerObserver.OnTriggerEntered += TriggerEnter;
            _triggerObserver.OnTriggerExited += TriggerExit;
            _follow.enabled = false;
        }

        private void TriggerEnter(Collider collider)
        {
            if(_isFollowing) return;

            _victimTransform = collider.transform;
            
            Aggro();
        }

        protected virtual void Aggro()
        {
            StopPrevAggro();
            SwitchFollowActive(true);
            _triggerObserver.gameObject.SetActive(false);
        }

        private void TriggerExit(Collider collider)
        {
            if(!_isFollowing) return;

            _aggroCoroutine = StartCoroutine(StopFollowingAfterCoolDownRoutine());
        }

        private void StopPrevAggro()
        {
            _isFollowing = true;
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine); 
                _aggroCoroutine = null;
            }
        }
        private void SwitchFollowActive(bool value) 
            => _follow.enabled = value;
        
        private IEnumerator StopFollowingAfterCoolDownRoutine()
        {
            yield return new WaitForSeconds(_followingDuration);
            _isFollowing = false;
            SwitchFollowActive(false);
        }
    }
}