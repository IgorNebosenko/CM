namespace CM.Input.CameraController.Effects
{
    public interface ICameraEffect
    {
        void Execute(float time, float intensity, ICameraController controller);
        void Simulate(float deltaTime);
    }
}