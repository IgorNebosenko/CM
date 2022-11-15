using CM.Input.CameraController;

namespace CM.Input
{
    public interface IInput
    {
        void Init(ICameraInput cameraInput);
        void Simulate(float deltaTime);
    }
}