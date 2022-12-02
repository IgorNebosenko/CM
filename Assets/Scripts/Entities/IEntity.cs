using System;
using CM.Input;
using Zenject;

namespace CM.Entities
{
    public interface IEntity : IDisposable
    {
        void Init(DiContainer container, EntityData data, IInput input);
        void DoDeath();
    }
}