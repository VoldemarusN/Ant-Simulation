using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Simulation Settings", menuName = "Simulation Settings")]
    public class SimulationSettings : ScriptableObject
    {
        public float WorkerSpeed => _workerSpeed;
        public float PredatorSpeed => _predatorSpeed;
        public float SpawnSafeZone => _spawnSafeZone;
        public float FoodSpawnPeriod => _foodSpawnPeriod;


        [Header("Bug settings")] [SerializeField]
        private float _workerSpeed;

        [SerializeField] private float _predatorSpeed;

        [Header("Spawn")] [SerializeField] private float _spawnSafeZone;


        [SerializeField] private float _foodSpawnPeriod;
    }
}