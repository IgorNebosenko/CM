using System;
using CM.Input;
using CM.Input.Configs;

namespace CM.Entities
{
    public interface IEntity : IDisposable
    {
        void Init(EntityData data, IInput input, InputConfig inputConfig);
        void DoDeath();
    }
}