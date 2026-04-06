using System.Collections.Generic;
using Core.Bug;
using Core.Food;
using UniRx;
using UnityEngine;

namespace Core.Level
{
    public interface ILevelInfo
    {
        List<VegetableTarget> Vegetables { get; }
        List<BugController> Workers { get; }
        List<BugController> Predators { get; }
        
        ReactiveProperty<int> EatenWorkersCount { get; }
        ReactiveProperty<int> EatenPredatorsCount { get; }

        IEnumerable<Transform> GetAllSpawnedObjects();
    }
}