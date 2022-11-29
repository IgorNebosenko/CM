using CM.Input.Configs;
using DG.Tweening;
using UnityEngine;

namespace CM.Entities.Motors
{
    public class CameraMotor
    {
        private Transform _camera;
        private InputConfig _inputConfig;
        
        private float cameraRotation;

        public CameraMotor(Transform camera, InputConfig inputConfig)
        {
            _camera = camera;
            _inputConfig = inputConfig;
        }

        public void Simulate(float deltaTime, float additionalAngleLook)
        {
            additionalAngleLook *= _inputConfig.lookYSensitivity * deltaTime;
            cameraRotation -= additionalAngleLook;
            cameraRotation = Mathf.Clamp(cameraRotation, 
                -_inputConfig.lookYAngleClamp, _inputConfig.lookYAngleClamp);
            _camera.DOLocalRotate(Vector3.right * cameraRotation, deltaTime);
        }
    }
}