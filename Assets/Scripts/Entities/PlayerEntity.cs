using CM.Entities.Motors;
using CM.GameObjects.Dynamic;
using CM.GameObjects.Visual;
using CM.Input;
using CM.Input.Configs;
using UnityEngine;
using Zenject;

namespace CM.Entities
{
    public class PlayerEntity : MonoBehaviour, IEntity, IHavePosition
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera _camera;

        private DiContainer _container;
        private PlayerInput _playerInput;
        private EntityData _entityData;
        private InputConfig _inputConfig;

        private MovementMotor _movementMotor;
        private CameraMotor _cameraMotor;

        private IEntityVisual[] _entityVisuals;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _entityVisuals = new IEntityVisual[]
            {
                new EntitySoundsHandler()
            };
        }

        private void Start()
        {
            for (var i = 0; i < _entityVisuals.Length; i++)
                _entityVisuals[i].Init(_container, this);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            var cachedPosition = transform.position;
            
            _playerInput.Update();
            _movementMotor.Simulate(deltaTime, _playerInput.MovementDirection, _playerInput.MovementViewDirectionX);
            _cameraMotor.Simulate(deltaTime, _playerInput.MovementViewDirectionY);

            if (transform.position != cachedPosition)
            {
                for (var i = 0; i < _entityVisuals.Length; i++)
                    _entityVisuals[i].OnIterateStep(deltaTime);
            }
        }

        public void Init(DiContainer container, EntityData data, IInput input)
        {
            _container = container;
            _playerInput = (PlayerInput)input;
            _entityData = data;
            _inputConfig = container.Resolve<InputConfig>();

            _movementMotor = new MovementMotor(characterController, transform, _entityData, _inputConfig);
            _cameraMotor = new CameraMotor(_camera.transform, _inputConfig);
        }

        public void DoDeath()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}