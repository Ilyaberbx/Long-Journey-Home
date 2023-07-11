using System.Collections.Generic;
using Logic.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Level
{
    public class LightAndObjectsEnabler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objects;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private List<Light> _lights;

        private void Awake()
            => _triggerObserver.OnTriggerEntered += Triggered;

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= Triggered;

        private void Triggered(Collider _)
        {
            foreach (Light light in _lights)
                light.enabled = true;

            foreach (GameObject obj in _objects)
                obj.SetActive(true);
        }
    }
}