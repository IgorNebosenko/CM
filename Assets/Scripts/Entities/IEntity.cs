using CM.Input;

namespace CM.Entities
{
    public interface IEntity
    {
        void Init(EntityData data, IInput input);
        void DoDeath();
    }
}