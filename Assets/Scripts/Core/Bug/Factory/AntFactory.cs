using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Bug.Factory;
using uPools;
using Views;
using Views.Bug;
using Views.Bug.Strategies;
using Views.Bug.Strategies.Implementations;

public class AntFactory : IAntFactory
{
    private readonly WorkerSettings _workerSettings;
    private readonly PredatorSettings _predatorSettings;
    private readonly ILevelInfo _levelInfo;

    private readonly ObjectPoolFacade<BugView> _workerPool;
    private readonly ObjectPoolFacade<BugView> _predatorPool;

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
        var searchStrategy = new SearchVegetableFoodStrategy(view, _levelInfo.Vegetables as List<VegetableTarget>);
        var moveStrategy = new BasicMoveStrategy(view, _workerSettings.Speed);
        var reproduceStrategy = new WorkerReproduceStrategy(_workerSettings, _levelInfo, this);
        var controller = new BugController(view, searchStrategy, moveStrategy, reproduceStrategy);

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
        var reproduceStrategy = new PredatorReproduceStrategy(_predatorSettings, _levelInfo, this);
        var controller = new BugController(view, searchStrategy, moveStrategy, reproduceStrategy);

        return controller;
    }

    public void DestroyBug(BugController bug)
    {
        bug.Dispose();
        _workerPool.Return(bug.View);
    }
}