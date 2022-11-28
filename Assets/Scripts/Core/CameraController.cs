    using UnityEngine;

namespace CM.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private bool enableCursor;
        private void Awake()
        {
            Cursor.lockState = enableCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = enableCursor;
        }
    }
}