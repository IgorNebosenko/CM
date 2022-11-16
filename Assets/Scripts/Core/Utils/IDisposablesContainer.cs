using System;

namespace CM.Core.Utils
{
    public interface IDisposablesContainer
    {
        void RegisterForDispose(IDisposable disposable);
    }
}