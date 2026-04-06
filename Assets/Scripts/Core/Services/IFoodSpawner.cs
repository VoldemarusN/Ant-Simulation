using System;

namespace Core.Services
{
    public interface IFoodSpawner : IDisposable
    {
        void StartSpawning();
        void StopSpawning();
    }
}