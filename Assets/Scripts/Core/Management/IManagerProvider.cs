namespace CM.Core.Management
{
    public interface IManagerProvider
    {
        T Get<T>() where T : IManager;
        void ClearManager();
        void Add<T>(T manager) where T : IManager;
    }
}