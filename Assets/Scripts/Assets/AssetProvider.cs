using UnityEngine;
using Views.Bug.Strategies;
using Views.Bug.Strategies.Implementations;

namespace Views.Bug
{
    [CreateAssetMenu(fileName = "BugAssetProvider", menuName = "Ant Simulation/Bug Asset Provider")]
    public class AssetProvider : ScriptableObject
    {
        [Header("Prefabs")] [SerializeField] private BugView _workerBugPrefab;
        [SerializeField] private BugView _predatorBugPrefab;
        [SerializeField] private VegetableTarget _vegetablePrefab;

        public BugView WorkerBugPrefab => _workerBugPrefab;
        public BugView PredatorBugPrefab => _predatorBugPrefab;
        public VegetableTarget VegetablePrefab => _vegetablePrefab;
    }
}