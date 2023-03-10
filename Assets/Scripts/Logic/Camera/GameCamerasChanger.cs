using System.Collections.Generic;
using ProjectSolitude.Enum;
using UnityEngine;

namespace Logic.Camera
{
    public class GameCamerasChanger : MonoBehaviour
    {
        [SerializeField] private List<GameCamera> _cameras;

        public GameObject ConstructCamera(GameCameraType type, Transform target = null, bool isLookAt = false)
        {
            var camera =  DefineCamera(type, target, isLookAt);
            ChangeCamerasPriority(type);
            return camera;
        }

        private GameObject DefineCamera(GameCameraType type, Transform target, bool isLookAt)
        {
            if (target != null)
            {
                foreach (GameCamera gameCamera in _cameras)
                {
                    if (isLookAt)
                        gameCamera.Camera.LookAt = gameCamera.CameraType == type ? target : null;

                    gameCamera.Camera.Follow = gameCamera.CameraType == type ? target : null;

                    if (gameCamera.Camera.Follow != null)
                        return gameCamera.gameObject;
                }
            }

            return null;
        }

        private void ChangeCamerasPriority(GameCameraType type)
        {
            foreach (var camera in _cameras)
                camera.Camera.Priority = camera.CameraType == type ? 1 : 0;
        }
    }
}