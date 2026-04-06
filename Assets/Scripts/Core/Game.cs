using System;
using Core.Services;
using Zenject;

namespace Core
{
    public class Game : IInitializable, IDisposable
    {
        private readonly IFoodSpawner _foodSpawner;
        private readonly IBugManager _bugManager;
        private readonly IUIUpdater _uiUpdater;

        public Game(
            IFoodSpawner foodSpawner,
            IBugManager bugManager,
            IUIUpdater uiUpdater)
        {
            _foodSpawner = foodSpawner;
            _bugManager = bugManager;
            _uiUpdater = uiUpdater;
        }

        public void Initialize()
        {
            StartAllSystems();
        }

        private void StartAllSystems()
        {
            _foodSpawner.StartSpawning();
            _bugManager.StartManagement();
            _uiUpdater.StartUpdating();
        }

        private void StopAllSystems()
        {
            _foodSpawner.StopSpawning();
            _bugManager.StopManagement();
            _uiUpdater.StopUpdating();
        }

        public void Dispose()
        {
            StopAllSystems();
            
            _foodSpawner?.Dispose();
            _bugManager?.Dispose();
            _uiUpdater?.Dispose();
        }
    }
}