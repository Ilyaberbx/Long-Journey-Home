using Enums;

namespace Logic.Camera
{
    public interface IGameCamerasChangerService
    {
        void ChangeCamerasPriority(GameCameraType type);

        GameCamera CurrentGameCamera();
    }
}