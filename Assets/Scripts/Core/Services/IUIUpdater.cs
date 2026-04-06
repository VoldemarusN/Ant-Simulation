using System;

namespace Core.Services
{
    public interface IUIUpdater : IDisposable
    {
        void StartUpdating();
        void StopUpdating();
    }
}