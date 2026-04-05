using System;
using Core;
using Core.Bug.Factory;
using Core.Bug.Strategies;
using UniRx;
using Random = UnityEngine.Random;

namespace Views.Bug.Strategies.Implementations
{
    public class WorkerReproduceStrategy : IReproduceStrategy
    {
        public Subject<BugController> Reproduced { get; set; } = new();

        private readonly WorkerSettings _workerSettings;
        private readonly ILevelInfo _levelInfo;
        private readonly IAntFactory _antFactory;

        public WorkerReproduceStrategy(WorkerSettings workerSettings, ILevelInfo levelInfo, IAntFactory antFactory)
        {
            _workerSettings = workerSettings;
            _levelInfo = levelInfo;
            _antFactory = antFactory;
        }

        public void SetFoodCount(int count)
        {
            if (count >= _workerSettings.FoodAmountToSplit)
            {
                if (_levelInfo.Workers.Count < _workerSettings.BugAmountToCreatePredator)
                {
                    if (Random.Range(0, 100) < _workerSettings.ChanceToCreatePredator * 100)
                    {
                        var instance = _antFactory.CreatePredatorBug();
                        _levelInfo.Predators.Add(instance);
                        Reproduced.OnNext(instance);
                    }
                    else
                    {
                        var instance = _antFactory.CreateWorkerBug();
                        _levelInfo.Workers.Add(instance);
                        Reproduced.OnNext(instance);
                    }
                }
            }
        }
    }
}