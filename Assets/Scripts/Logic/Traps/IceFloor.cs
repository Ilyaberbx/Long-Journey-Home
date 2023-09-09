using Extensions;
using Logic.Shatter;
using UnityEngine;

namespace Logic.Traps
{
    public class IceFloor : MonoBehaviour
    {
        [SerializeField] private ShatteringObject _shatteredIceFloorPrefab;
        private ShatteringObject _shatteredIceFloor;

        public void Shatter() 
            => _shatteredIceFloor =  gameObject.Shatter(_shatteredIceFloorPrefab, true);

        public void StopShattering() 
            => _shatteredIceFloor.StopShatter();
    }
}