using System.Collections.Generic;

namespace CM.Core.Managers
{
    public class ManagersRunner : IManagersRunner, IManagerProvider
    {
        private readonly List<IManager> _managers;

        public ManagersRunner(List<IManager> managers)
        {
            _managers = managers;
        }
        
        public T Get<T>() where T : IManager
        {
            return (T) _managers.Find(m => m is T);
        }

        public void Simulate<T>(float deltaTime) where T : ISimulatedManager
        {
            foreach (IManager t in _managers)
            {
                if (t is T m)
                    m.Simulate(deltaTime);
            }
        }

        public void ClearManager()
        {
            _managers.ForEach(m => m.Destroy());
            _managers.Clear();
        }

        public void Add<T>(T manager) where T : IManager
        {
            _managers.Add(manager);
        }
    }
}