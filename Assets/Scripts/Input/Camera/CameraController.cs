using System;
using CM.Input.CameraController.Effects;
using UnityEngine;
using Zenject;

namespace CM.Input.CameraController
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private Camera camera;
        [SerializeField] private float sensitivity = 1f;
        
        [Inject(Id = "cameraJoystick")] 
        private Joystick _cameraJoystick;

        private void Awake()
        {
            if (camera == null)
                camera = GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            if (_cameraJoystick.Direction == Vector2.zero)
                return;

            var angle = Vector2.Angle(Vector2.up, _cameraJoystick.Direction);
            if (_cameraJoystick.Horizontal < 0)
                angle *= -1;

            camera.transform.eulerAngles = Vector3.up * angle * sensitivity;
        }

        public void Reset()
        {
            camera.transform.rotation.SetLookRotation(Vector3.zero);
        }

        public void SetCameraEffect(ICameraEffect effect)
        {
            throw new NotImplementedException();
        }
    }
}