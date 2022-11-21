namespace CM.Core.Management
{
    public interface IManager
    {
        void Destroy();
    }

    public interface ISimulatedManager : IManager
    {
        void Simulate(float deltaTime);
    }

    public interface IUpdateManager : ISimulatedManager
    {
    }

    public interface IFixedUpdateManager : ISimulatedManager
    {
    }

    public interface ILateUpdateManager : ISimulatedManager
    {
    }
}