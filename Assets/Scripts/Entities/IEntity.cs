using System;
using CM.Input;

namespace CM.Entities
{
    public interface IEntity : IDisposable
    {
        void Init(EntityData data, IInput input);
        void DoDeath();
    }
}