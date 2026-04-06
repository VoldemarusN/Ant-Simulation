using System;
using Core.Bug;

namespace Core.Services
{
    public interface IBugManager : IDisposable
    {
        void StartManagement();
        void StopManagement();
        void RegisterBug(BugController bug);
    }
}