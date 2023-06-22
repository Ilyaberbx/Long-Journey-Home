using UnityEngine;

namespace Logic.Player
{

    public class EquipmentLook : MonoBehaviour
    {
        [SerializeField] private HeroCameraHolder _cameraHolder;
        private Transform _cameraTransform;

        private void Start() 
            => _cameraTransform = _cameraHolder.Camera.transform;

        private void Update()
        {
            transform.rotation =
                Quaternion.Slerp(transform.rotation, _cameraTransform.rotation, 2000f * Time.deltaTime);

            transform.rotation = _cameraTransform.rotation;
        }
    }
}