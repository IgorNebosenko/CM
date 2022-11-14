namespace CM.Core.Managers
{
    public interface IManagersRunner
    {
        void Simulate<T>(float deltaTime) where T : ISimulatedManager;
    }
}