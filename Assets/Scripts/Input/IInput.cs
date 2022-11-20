using CM.Input.CameraController;

namespace CM.Input
{
    public interface IInput
    {
        void Init();
        void Simulate(float deltaTime);
    }
}