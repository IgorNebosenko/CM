using CM.Input;
using Zenject;

namespace CM.GameObjects.Visual
{
    public interface IEntityVisual
    {
        void Init(DiContainer container, IHavePosition entityPosition);

        void OnIterateStep(float deltaTime);

        void Destroy();
    }
}