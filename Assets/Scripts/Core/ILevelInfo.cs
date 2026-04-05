using System.Collections.Generic;
using UnityEngine;
using Views;
using Views.Bug;

namespace Core
{
    public interface ILevelInfo
    {
        List<VegetableTarget> Vegetables { get; }
        List<BugController> Workers { get; }
        List<BugController> Predators { get; }

        IEnumerable<Transform> GetAllSpawnedObjects();
    }
}