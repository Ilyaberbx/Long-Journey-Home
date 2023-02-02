using UnityEngine;

namespace Logic
{
    public class SelfDestroying : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeDestroy;

        private void Awake() 
            => Destroy(this,_delayBeforeDestroy);
    }
}