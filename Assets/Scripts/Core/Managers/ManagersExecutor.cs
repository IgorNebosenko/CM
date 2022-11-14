using UnityEngine;
using Zenject;

namespace CM.Core.Managers
{
    public class ManagersExecutor : MonoBehaviour
    {
        [SerializeField] private bool simulate = true;

        [Inject] private IManagersRunner _managersRunner;

        private void Update()
        {
            if (simulate)
                _managersRunner.Simulate<IUpdateManager>(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (simulate)
                _managersRunner.Simulate<IFixedUpdateManager>(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if (simulate)
                _managersRunner.Simulate<ILateUpdateManager>(Time.deltaTime);
        }
    }
}