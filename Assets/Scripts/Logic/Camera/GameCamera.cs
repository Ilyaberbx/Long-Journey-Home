using Cinemachine;
using Enums;
using ProjectSolitude.Enum;
using UnityEngine;

namespace Logic.Camera
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private GameCameraType _cameraType;

        public GameCameraType CameraType => _cameraType;
        public CinemachineVirtualCameraBase Camera { get; private set; }

        private void Awake() 
            => Camera = GetComponent<CinemachineVirtualCameraBase>();

        public void Follow(Transform target)
        {
            Camera.LookAt = target;
            Camera.Follow = target;
        }
    }
}
