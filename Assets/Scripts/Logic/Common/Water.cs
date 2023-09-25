using Logic.Player;
using UnityEngine;

namespace Logic.Common
{
    public class Water : MonoBehaviour
    {
        [SerializeField] private float _freezeValue;
        private void OnCollisionEnter(Collision collision)
        {
            if (IsFreezable(collision, out IFreezable freeze))
                ExecuteFreeze(freeze);
        }

        private void ExecuteFreeze(IFreezable freeze) 
            => freeze.DecreaseCurrentWarmLevel(_freezeValue);

        private bool IsFreezable(Collision collision, out IFreezable freeze) 
            => collision.gameObject.TryGetComponent(out freeze);
    }
}