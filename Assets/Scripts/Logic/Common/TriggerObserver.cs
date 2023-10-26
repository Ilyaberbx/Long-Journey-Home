using System;
using UnityEngine;

namespace Logic.Common
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        private Collider _collider;
        private void Awake() 
            => _collider = GetComponent<Collider>();
        public event Action<Collider> OnTriggerEntered;
        public event Action<Collider> OnTriggerExited; 
        private void OnTriggerEnter(Collider other)
            => OnTriggerEntered?.Invoke(other);

        private void OnTriggerExit(Collider other) 
            => OnTriggerExited?.Invoke(other);

        public void Disable()
            => _collider.enabled = false;

        public void Enable() 
            => _collider.enabled = true;
    }
}
