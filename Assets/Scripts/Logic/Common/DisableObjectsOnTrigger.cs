using System.Collections.Generic;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Common
{

    public class DisableObjectsOnTrigger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objectsToDisable;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += DisableObjects;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= DisableObjects;

        private void DisableObjects(Collider obj)
        {
            foreach (GameObject objectToDisable in _objectsToDisable) 
                objectToDisable.SetActive(false);
        }
    }
}