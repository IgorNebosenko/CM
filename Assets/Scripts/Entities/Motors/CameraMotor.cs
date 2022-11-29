using DG.Tweening;
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
            additionalAngleLook *= _lookYSensitivity * deltaTime;
            cameraRotation -= additionalAngleLook;
            cameraRotation = Mathf.Clamp(cameraRotation, -CameraAngleClamp, CameraAngleClamp);
            _camera.DOLocalRotate(Vector3.right * cameraRotation, deltaTime);
        }
    }
}