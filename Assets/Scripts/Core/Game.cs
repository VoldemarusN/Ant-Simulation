using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ObservableCollections;
using R3;
using UnityEngine;
using Views;
using Views.Bug;
using Zenject;

namespace Core
{
    public class Game : IInitializable, IDisposable
    {
        private readonly SimulationSettings _settings;
        private readonly BaseAntFactory _antFactory;
        private readonly ILevelInfo _levelInfo;
        private readonly VegetableFoodFactory _vegetableFoodFactory;
        private readonly LevelPositionService _levelPositionService;

        private readonly CancellationDisposable _cancellationDisposable;

        public Game(LevelPositionService levelPositionService, SimulationSettings settings, VegetableFoodFactory vegetableFoodFactory,
            BaseAntFactory antFactory,
            ILevelInfo levelInfo)
        {
            _antFactory = antFactory;
            _levelInfo = levelInfo;
            _vegetableFoodFactory = vegetableFoodFactory;
            _settings = settings;
            _levelPositionService = levelPositionService;
            _cancellationDisposable = new CancellationDisposable();
        }

        public void Initialize()
        {
            //spawn food
            Observable.Interval(TimeSpan.FromSeconds(_settings.FoodSpawnPeriod))
                .Subscribe(_ =>
                {
                    var (x, y, z) = _levelPositionService.GetRandomPoint();
                    var foodInstance = _vegetableFoodFactory.Spawn(new Vector3(x, y, z));
                    _levelInfo.Vegetables.Add(foodInstance);
                })
                .RegisterTo(_cancellationDisposable.Token);

            //spawn worker if there is no bugs
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    var totalBugs = _levelInfo.Workers.Count + _levelInfo.Predators.Count;
                    if (totalBugs == 0)
                    {
                        var bug = _antFactory.CreateWorkerBug();
                        bug.View.UpdatePosition(_levelPositionService.GetRandomPosition());
                        _levelInfo.Workers.Add(bug);
                    }
                })
                .RegisterTo(_cancellationDisposable.Token);
        }

        public void Dispose()
        {
            _levelPositionService?.Dispose();
            _cancellationDisposable?.Dispose();
        }
    }
}