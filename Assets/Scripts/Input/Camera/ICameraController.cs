using CM.Input.CameraController.Effects;

namespace CM.Input.CameraController
{
    public interface ICameraController
    {
        void Reset();
        void SetCameraEffect(ICameraEffect effect);
    }
}