using UnityEngine;

namespace ProjectSolitude.Logic
{
    public class Gravity : MonoBehaviour
    {
        private const float GroundedGravityConst = -2f;
        private const float GravityCoefficientConst = 1.05f;

        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private CheckPoint _checkingGroundPoint;

        private CharacterController _characterController;
        private Vector3 _velocity;

        public Vector3 GetVelocity() 
            => _velocity;

        public void SetVelocity(Vector3 velocity) 
            => _velocity = velocity;

        public float GetGravity()
            => _gravity;
        private void Awake() => Inititalize();

        private void Update() => CalculateGravity();


        public bool TryCatchGround()
        {
            Collider[] hits = Physics.OverlapSphere(_checkingGroundPoint.Position, _checkingGroundPoint.Radius);

            foreach (var check in hits)
            {
                if (check.transform != null)
                    
                    if (check.transform.GetComponent<Ground>() != null) 
                        return true;
            }

            return false;
        }

        private void Inititalize()
        {
            _characterController = GetComponent<CharacterController>();
            _gravity *= GravityCoefficientConst;
        }


        private void CalculateGravity()
        {
            if (TryCatchGround() && _velocity.y < 0) _velocity.y = GroundedGravityConst;
            
            _velocity.y -= _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }
}