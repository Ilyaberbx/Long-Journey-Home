using System;
using UnityEngine;

namespace Logic.Raycast
{
    public abstract class RaycastObserver<T> : MonoBehaviour where T : MonoBehaviour
    {
        public event Action<T> OnRayHit;
        [SerializeField] private float _rayCastDistance;

        private Ray _ray;

        private void Update()
            => Execute();

        private void Execute()
        {
            if (!TryHitRay(out RaycastHit hit)) 
                return;
            
            if (!CheckRayCastHit(hit, out T value))
                return;

            InformRayHit(value);
        }

        private bool TryHitRay(out RaycastHit hit)
        {
            _ray = new Ray(transform.position, -transform.up * _rayCastDistance);
            return Physics.Raycast(_ray, out hit);
        }

        private void InformRayHit(T value)
            => OnRayHit?.Invoke(value);

        private bool CheckRayCastHit(RaycastHit hit, out T value)
            => hit.transform.TryGetComponent(out value);

        private void OnDrawGizmos()
            => Gizmos.DrawRay(_ray);
    }
}