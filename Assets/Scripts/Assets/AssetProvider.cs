using Core.Bug;
using Core.Food;
using UnityEngine;

namespace Assets
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