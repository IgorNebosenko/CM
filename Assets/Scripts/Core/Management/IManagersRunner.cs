namespace CM.Core.Management
{
    public interface IManagersRunner
    {
        void Simulate<T>(float deltaTime) where T : ISimulatedManager;
    }
}