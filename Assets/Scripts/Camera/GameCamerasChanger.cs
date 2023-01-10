using ProjectSolitude.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSolitude.CameraAdditions
{
    public class GameCamerasChanger : MonoBehaviour
    {
        [SerializeField]  private List<GameCamera> _cameras;

        public void SetCamera(GameCameraType type,Transform target = null,bool isLookAt = false)
        {
            if(target != null)
            {
                foreach (var camera in _cameras)
                {
                    if(isLookAt)
                    camera.Camera.LookAt = camera.CameraType == type ? target : null;
                    
                    camera.Camera.Follow = camera.CameraType == type ? target : null;
                }
            }
            ChangeCamerasPriority(type);
        }
        private void ChangeCamerasPriority(GameCameraType type)
        {
            foreach (var camera in _cameras)
                camera.Camera.Priority = camera.CameraType == type ? 1 : 0;
        }

    }
}
