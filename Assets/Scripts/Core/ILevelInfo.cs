using System.Collections.Generic;
using ObservableCollections;
using UnityEngine;
using Views;
using Views.Bug;

namespace Core
{
    public interface ILevelInfo
    {
        ObservableList<VegetableTarget> Vegetables { get; }
        ObservableList<BugController> Workers { get; }
        ObservableList<BugController> Predators { get; }

        IEnumerable<Transform> GetAllSpawnedObjects();
    }
}