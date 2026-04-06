using System.Collections.Generic;
using System.Linq;
using Assets;
using Core.Bug.Settings;
using Core.Bug.Strategies.Implementations;
using Core.Food;
using Core.Level;

namespace Core.Bug.Factory
{
    public class AntFactory : IAntFactory
    {
        private readonly WorkerSettings _workerSettings;
        private readonly PredatorSettings _predatorSettings;
        private readonly ILevelInfo _levelInfo;

        private readonly ObjectPoolFacade<BugView> _workerPool;
        private readonly ObjectPoolFacade<BugView> _predatorPool;

        private readonly HashSet<BugController> _workerSet = new();
        private readonly HashSet<BugController> _predatorSet = new();

        public AntFactory(AssetProvider assetProvider, WorkerSettings workerSettings, PredatorSettings predatorSettings, ILevelInfo levelInfo)
        {
            _workerSettings = workerSettings;
            _predatorSettings = predatorSettings;
            _levelInfo = levelInfo;

            _workerPool = new ObjectPoolFacade<BugView>(assetProvider.WorkerBugPrefab, 50);
            _predatorPool = new ObjectPoolFacade<BugView>(assetProvider.PredatorBugPrefab, 50);
        }

        public BugController CreateWorkerBug()
        {
            var view = _workerPool.Rent();
            var searchStrategy = new SearchVegetableFoodStrategy(view, _levelInfo.Vegetables);
            var moveStrategy = new BasicMoveStrategy(view, _workerSettings.Speed);
            var reproduceStrategy = new WorkerReproduceStrategy(_workerSettings, _levelInfo, this);
            var controller = new BugController(view, searchStrategy, moveStrategy, reproduceStrategy);
            controller.Initialize();
            _workerSet.Add(controller);
            return controller;
        }

        public BugController CreatePredatorBug()
        {
            var view = _predatorPool.Rent();
            var searchStrategy =
                new SearchNearestFoodStrategy(view,
                    new IEnumerable<FoodTarget>[]
                    {
                        _levelInfo.Vegetables, _levelInfo.Predators.Select(x => x.View),
                        _levelInfo.Workers.Select(x => x.View)
                    });
            var moveStrategy = new BasicMoveStrategy(view, _predatorSettings.Speed);
            var reproduceStrategy = new PredatorReproduceStrategy(_predatorSettings, this);
            var controller = new BugController(view, searchStrategy, moveStrategy, reproduceStrategy);
            controller.Initialize(_predatorSettings.LifetimeInSeconds);
            _predatorSet.Add(controller);
            return controller;
        }

        public void DestroyBug(BugController bug)
        {
            bug.Dispose();

            if (_workerSet.Remove(bug))
                _workerPool.Return(bug.View);
            else if (_predatorSet.Remove(bug))
                _predatorPool.Return(bug.View);
        }

        public bool IsWorker(BugController bug) => _workerSet.Contains(bug);
        public bool IsPredator(BugController bug) => _predatorSet.Contains(bug);
    }
}