using Cinemachine;
using Enums;

namespace Logic.Camera
{
    public interface ICameraService
    {
        void ChangeCamerasPriority(GameCameraType type);

        GameCamera CurrentGameCamera();
        CinemachineBrain Brain { get; }
        GameCamera GetCameraByType(GameCameraType type);
    }
}