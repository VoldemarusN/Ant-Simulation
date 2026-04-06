using System.Collections.Generic;
using System.Linq;
using Core.Bug;
using Core.Food;
using UniRx;
using UnityEngine;

namespace Core.Level
{
    public class LevelInfo : ILevelInfo
    {
        public List<VegetableTarget> Vegetables { get; } = new();

        public List<BugController> Workers { get; } = new();

        public List<BugController> Predators { get; } = new();
        
        public ReactiveProperty<int> EatenWorkersCount { get; } = new();
        public ReactiveProperty<int> EatenPredatorsCount { get; } = new();

        public IEnumerable<Transform> GetAllSpawnedObjects()
        {
            var spawnedObjects = new List<Transform>(Workers.Count + Predators.Count + Vegetables.Count);
            spawnedObjects.AddRange(Workers.Select(x => x.View.transform));
            spawnedObjects.AddRange(Predators.Select(x => x.View.transform));
            spawnedObjects.AddRange(Vegetables.Select(x => x.transform));
            return spawnedObjects;
        }
    }
}