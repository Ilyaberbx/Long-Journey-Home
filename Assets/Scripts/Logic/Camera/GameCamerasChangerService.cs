using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace Logic.Camera
{
    public class GameCamerasChangerService : MonoBehaviour, IGameCamerasChangerService
    {
        [SerializeField] private List<GameCamera> _cameras;

        public GameCamera ConstructCamera(GameCameraType type, Transform target = null, bool isLookAt = false)
        {
            GameCamera camera =  DefineCamera(type, target, isLookAt);
            ChangeCamerasPriority(type);
            return camera;
        }

        private GameCamera DefineCamera(GameCameraType type, Transform target, bool isLookAt)
        {
            if (target != null)
            {
                foreach (GameCamera gameCamera in _cameras)
                {
                    if (isLookAt)
                        gameCamera.Camera.LookAt = gameCamera.CameraType == type ? target : null;

                    gameCamera.Camera.Follow = gameCamera.CameraType == type ? target : null;

                    if (gameCamera.Camera.Follow != null)
                        return gameCamera;
                }
            }

            return null;
        }

        public GameCamera CurrentGameCamera() 
            => _cameras.FirstOrDefault(camera => camera.Camera.Priority == 1);

        public void ChangeCamerasPriority(GameCameraType type)
        {
            Debug.Log("Camera changing");
            foreach (GameCamera gameCamera in _cameras)
                gameCamera.Camera.Priority = gameCamera.CameraType == type ? 1 : 0;
        }
    }
}