using UnityEngine;

namespace CM.Entities.Motors
{
    public class CameraMotor
    {
        private Transform _camera;
        private float _lookYSensitivity;

        private const float CameraAngleClamp = 85f;
        private float cameraRotation;

        public CameraMotor(Transform camera, float lookYSensitivity)
        {
            _camera = camera;
            _lookYSensitivity = lookYSensitivity;
        }

        public void Simulate(float deltaTime, float additionalAngleLook)
        {
            _camera.Rotate(Vector3.left * additionalAngleLook * _lookYSensitivity * deltaTime, Space.Self);

            cameraRotation -= additionalAngleLook;
            cameraRotation = Mathf.Clamp(cameraRotation, -CameraAngleClamp, CameraAngleClamp);
            _camera.localEulerAngles = new Vector3(cameraRotation, 0f, 0f);
        }
    }
}