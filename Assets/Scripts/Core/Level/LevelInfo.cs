using System.Collections.Generic;
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
            foreach (var w in Workers) yield return w.View.transform;
            foreach (var p in Predators) yield return p.View.transform;
            foreach (var v in Vegetables) yield return v.transform;
        }
    }
}