using UnityEngine;

namespace Logic.Gravity
{
    public class Ground : MonoBehaviour
    {
        public SurfaceType SurfaceType => _surfaceType;
        
        [SerializeField] private SurfaceType _surfaceType;
    }
}