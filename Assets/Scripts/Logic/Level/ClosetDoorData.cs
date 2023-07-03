using UnityEngine;

namespace Logic.Level
{

    [System.Serializable]
    public class ClosetDoorData
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _closeAngle;
        [SerializeField] private float _openAngle;

        public Transform Transform => _transform;
        public float OpenAngle => _openAngle;
        public float CloseAngle => _closeAngle;
    }
}