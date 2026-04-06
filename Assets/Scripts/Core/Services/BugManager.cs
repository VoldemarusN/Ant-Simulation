using System;
using Core.Bug;
using Core.Bug.Factory;
using Core.Level;
using Core.Services;
using UniRx;

namespace Core.Services
{
    public class BugManager : IBugManager
    {
        private readonly IAntFactory _antFactory;
        private readonly ILevelInfo _levelInfo;
        private readonly LevelPositionService _levelPositionService;
        
        private readonly CompositeDisposable _disposables = new();

        public BugManager(
            IAntFactory antFactory,
            ILevelInfo levelInfo,
            LevelPositionService levelPositionService)
        {
            _antFactory = antFactory;
            _levelInfo = levelInfo;
            _levelPositionService = levelPositionService;
        }

        public void StartManagement()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => CheckForEmptyPopulation())
                .AddTo(_disposables);
        }

        public void StopManagement()
        {
            _disposables.Clear();
        }

        public void RegisterBug(BugController bug)
        {
            bug.Reproduced
                .Subscribe(HandleBugReproduction)
                .AddTo(_disposables);
            bug.View.Eaten
                .Subscribe(_ => HandleBugEat(bug))
                .AddTo(_disposables);
        }

        private void CheckForEmptyPopulation()
        {
            var totalBugs = _levelInfo.Workers.Count + _levelInfo.Predators.Count;
            if (totalBugs == 0)
            {
                SpawnInitialWorker();
            }
        }

        private void SpawnInitialWorker()
        {
            var bug = _antFactory.CreateWorkerBug();
            bug.View.WarpTo(_levelPositionService.GetRandomPosition());
            _levelInfo.Workers.Add(bug);
            RegisterBug(bug);
        }

        private void HandleBugReproduction(BugController newBug)
        {
            newBug.View.WarpTo(_levelPositionService.GetRandomPosition());
            RegisterBug(newBug);
        }

        private void HandleBugEat(BugController bug)
        {
            _antFactory.DestroyBug(bug);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}