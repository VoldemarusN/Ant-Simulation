using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ObservableCollections;
using R3;
using Views;
using Zenject;

namespace Core
{
    public class Game : IInitializable, IDisposable
    {
        private readonly SimulationSettings _settings;
        private readonly BaseAntFactory _antFactory;
        private readonly VegetableFoodFactory _vegetableFoodFactory;
        private readonly LevelPositionService _levelPositionService;

        private CancellationDisposable _cancellationDisposable;

        private ObservableList<WorkerBug> _workerBugs;
        private ObservableList<PredatorBug> _predatorBugs;

        private ObservableList<VegetableTarget> _vegetableTargets;

        public Game(BaseAntFactory antFactory, SimulationSettings settings)
        {
            _antFactory = antFactory;
            _settings = settings;

            _workerBugs = new ObservableList<WorkerBug>();
            _predatorBugs = new ObservableList<PredatorBug>();
        }

        public void Initialize()
        {
            _vegetableTargets.CollectionChanged += OnVegetablesChanged;
            _workerBugs.CollectionChanged

            //spawn food
            Observable.Timer(TimeSpan.FromSeconds(_settings.FoodSpawnPeriod))
                .Subscribe(_ =>
                    {
                        var foodInstance = _vegetableFoodFactory.Spawn(_levelPositionService.GetRandomPosition());
                        _vegetableTargets.Add(foodInstance);
                    }
                )
                .RegisterTo(_cancellationDisposable.Token);

            //spawn worker if there is no bugs

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    var totalBugs = _workerBugs.Count + _predatorBugs.Count;
                    if (totalBugs == 0)
                    {
                        var bug = _antFactory.CreateWorkerBug();
                        bug.transform.position = _levelPositionService.GetRandomPosition();
                        _workerBugs.Add(bug);
                    }
                })
                .RegisterTo(_cancellationDisposable.Token);

            //move bugs
            Observable.EveryUpdate().Subscribe(_ =>
            {
                foreach (var workerBug in _workerBugs)
                {
                    if (!workerBug.HasFoodTarget)
                    {
                        workerBug.FindNearestFood(_vegetableTargets);
                    }

                    workerBug.MoveToTarget();
                }
            }).RegisterTo(_cancellationDisposable.Token);
        }

        public void Dispose()
        {
            _levelPositionService?.Dispose();
            _cancellationDisposable?.Dispose();
        }

        private void OnVegetablesChanged(in NotifyCollectionChangedEventArgs<VegetableTarget> changedArgs)
        {
            if (changedArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var worker in _workerBugs.Where(b => !b.HasFoodTarget))
                    worker.FindNearestFood(_vegetableTargets);

                foreach (var predator in _predatorBugs.Where(b => !b.HasFoodTarget))
                    predator.FindNearestFood(_vegetableTargets);
            }
            else if (changedArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var bug in _workerBugs)
                {
                    bug.FindNearestFood(Enumerable.Repeat(changedArgs.NewItem, 1));
                }
            }
        }
    }
}