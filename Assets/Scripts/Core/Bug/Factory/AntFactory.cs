using System.Collections.Generic;
using System.Linq;
using Core;
using ObservableCollections;
using UnityEngine;
using uPools;
using Views;
using Views.Bug;
using Views.Bug.Strategies;
using Views.Bug.Strategies.Implementations;

public class AntFactory : BaseAntFactory
{
    private readonly WorkerSettings _workerSettings;
    private readonly PredatorSettings _predatorSettings;
    private readonly ILevelInfo _levelInfo;
    private readonly Vector3 _spawnPosition;


    private readonly ObjectPoolFacade<BugView> _workerPool;
    private readonly ObjectPoolFacade<BugView> _predatorPool;

    public AntFactory(AssetProvider assetProvider, WorkerSettings workerSettings, PredatorSettings predatorSettings, ILevelInfo levelInfo,
        Vector3 spawnPosition)
    {
        _workerSettings = workerSettings;
        _predatorSettings = predatorSettings;
        _levelInfo = levelInfo;
        _spawnPosition = spawnPosition;

        _workerPool = new ObjectPoolFacade<BugView>(assetProvider.WorkerBugPrefab, 50);
        _predatorPool = new ObjectPoolFacade<BugView>(assetProvider.PredatorBugPrefab, 50);
    }

    public override BugController CreateWorkerBug()
    {
        var view = _workerPool.Rent();
        view.UpdatePosition(_spawnPosition);
        var searchStrategy = new SearchVegetableFoodStrategy(view, _levelInfo.Vegetables as ObservableList<VegetableTarget>);
        var moveStrategy = new BasicMoveStrategy(view, _workerSettings.Speed);
        var controller = new BugController(view, searchStrategy, moveStrategy);

        return controller;
    }

    public override BugController CreatePredatorBug()
    {
        var view = _predatorPool.Rent();
        view.UpdatePosition(_spawnPosition);
        var searchStrategy =
            new SearchNearestFoodStrategy(view,
                new IEnumerable<FoodTarget>[]
                {
                    _levelInfo.Vegetables, _levelInfo.Predators.Select(x => x.View),
                    _levelInfo.Workers.Select(x => x.View)
                });
        var moveStrategy = new BasicMoveStrategy(view, _predatorSettings.Speed);
        var controller = new BugController(view, searchStrategy, moveStrategy);

        return controller;
    }
}