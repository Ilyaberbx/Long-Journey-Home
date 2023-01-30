using UnityEngine;

namespace Logic
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform _checkPointTransform;
        [SerializeField] private float _radius;
        public float Radius => _radius;
        public Vector3 Position => _checkPointTransform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_checkPointTransform.position,_radius);
            Gizmos.color = Color.white;
        }
    }
}