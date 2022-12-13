using UnityEngine;

namespace CM.Core.Utils
{
    public class DestroyAtRelease : MonoBehaviour
    {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        private void Awake()
        {
            Destroy(gameObject);
        }
#endif
    }
}
