using UnityEngine;

namespace Logic.Shatter
{
    public class ShatteringObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _piecesConstantForce;
        [SerializeField] private Rigidbody[] _piecesRigidbodies;
        [SerializeField] private ConstantForce[] _piecesConstantForces;

        public void Shatter()
        {
            TogglePiecesKinematic(false);
            ChangePiecesConstantForce(_piecesConstantForce);
        }

        public void StopShatter()
        {
            TogglePiecesKinematic(true);
            ChangePiecesConstantForce(Vector3.zero);
        }

        private void ChangePiecesConstantForce(Vector3 piecesConstantForce)
        {
            foreach (ConstantForce constantForce in _piecesConstantForces)
                constantForce.force = piecesConstantForce;
        }

        private void TogglePiecesKinematic(bool value)
        {
            foreach (Rigidbody rigidbody in _piecesRigidbodies)
                rigidbody.isKinematic = value;
        }
    }
}