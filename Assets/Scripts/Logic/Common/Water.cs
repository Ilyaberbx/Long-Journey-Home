using Logic.Player;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Logic.Common
{
    public class Water : MonoBehaviour
    {
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private float _freezeValue;

        private void Start() 
            => _soundOperations.PlaySound<LoopSoundOperator>();

        private void OnTriggerStay(Collider collision)
        {
            if (IsFreezable(collision, out IFreezable freeze))
                ExecuteFreeze(freeze);
        }

        private void ExecuteFreeze(IFreezable freeze) 
            => freeze.DecreaseCurrentWarmLevel(_freezeValue);

        private bool IsFreezable(Collider collision, out IFreezable freeze) 
            => collision.TryGetComponent(out freeze);
    }
}