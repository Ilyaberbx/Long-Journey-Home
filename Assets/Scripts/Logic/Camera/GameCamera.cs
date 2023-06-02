using Cinemachine;
using Enums;
using UnityEngine;

namespace Logic.Camera
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private GameCameraType _cameraType;
        public GameCameraType CameraType => _cameraType;
        public CinemachineVirtualCamera Camera { get; private set; }
        

        private void Awake() 
            => OnAwake();

        protected virtual void OnAwake() 
            => Camera = GetComponent<CinemachineVirtualCamera>();
        
    }
}