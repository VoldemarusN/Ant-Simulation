using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Simulation Settings", menuName = "Simulation Settings")]
    public class SimulationSettings : ScriptableObject
    {
        public float SpawnSafeZone => _spawnSafeZone;
        public float FoodSpawnPeriod => _foodSpawnPeriod;


        [Header("Spawn")] [SerializeField] private float _spawnSafeZone;
        [SerializeField] private float _foodSpawnPeriod;
    }
}