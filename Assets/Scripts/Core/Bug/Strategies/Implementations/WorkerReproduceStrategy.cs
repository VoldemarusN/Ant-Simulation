using Core.Bug.Factory;
using Core.Bug.Settings;
using Core.Level;
using UniRx;
using Random = UnityEngine.Random;

namespace Core.Bug.Strategies.Implementations
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
                if (_levelInfo.Workers.Count > _workerSettings.BugAmountToCreatePredator)
                {
                    if (Random.Range(0, 100) < _workerSettings.ChanceToCreatePredator * 100)
                    {
                        var predator = _antFactory.CreatePredatorBug();
                        _levelInfo.Predators.Add(predator);
                        Reproduced.OnNext(predator);
                        return;
                    }
                }

                var worker = _antFactory.CreateWorkerBug();
                _levelInfo.Workers.Add(worker);
                Reproduced.OnNext(worker);
            }
        }
    }
}