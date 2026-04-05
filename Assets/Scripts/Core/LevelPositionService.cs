using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class LevelPositionService : IDisposable
    {
        private const int MAX_RANDOM_ATTEMPTS = 128;

        private readonly GameObject _floor;
        private readonly SimulationSettings _settings;
        private readonly ILevelInfo _levelInfo;

        private readonly CancellationTokenSource _disposeCts = new();
        private Bounds _floorBounds;

        public LevelPositionService(GameObject floor, SimulationSettings settings, ILevelInfo levelInfo)
        {
            _floor = floor;
            _settings = settings;
            _levelInfo = levelInfo;

            _floorBounds = GetFloorBounds();
        }

        /// <param name="spawnedObjects">
        /// На радиусе вблизи этих объектов, спавнится ничего не будет
        /// </param>
        /// <returns></returns>
        public Vector3 GetRandomPosition()
        {
            if (_disposeCts.IsCancellationRequested) return Vector3.zero;

            var safeZoneSqr = _settings.SpawnSafeZone * _settings.SpawnSafeZone;

            for (var i = 0; i < MAX_RANDOM_ATTEMPTS; i++)
            {
                var randomPosition = GetRandomPoint();
                var intersects = HasIntersection(randomPosition, _levelInfo.GetAllSpawnedObjects().Select(x => x.position), safeZoneSqr);

                if (!intersects)
                {
                    return new Vector3(randomPosition.x, _floor.transform.position.y, randomPosition.z);
                }
            }

            return _floorBounds.center;
        }

        private Bounds GetFloorBounds()
        {
            var position = _floor.transform.position;
            var scale = _floor.transform.lossyScale;
            var size = new Vector3(Mathf.Abs(scale.x), 0f, Mathf.Abs(scale.z));
            return new Bounds(position, size);
        }

        private static bool HasIntersection((float x, float y, float z) candidate, IEnumerable<Vector3> positions, float safeZoneSqr)
        {
            foreach (var existingPosition in positions)
            {
                var dx = candidate.x - existingPosition.x;
                var dz = candidate.z - existingPosition.z;
                var sqrDistance = dx * dx + dz * dz;

                if (sqrDistance < safeZoneSqr)
                {
                    return true;
                }
            }

            return false;
        }

        public (float x, float y, float z) GetRandomPoint()
        {
            var x = _floorBounds.center.x + (UnityEngine.Random.value - 0.5f) * _floorBounds.size.x;
            var y = _floor.transform.position.y;
            var z = _floorBounds.center.z + (UnityEngine.Random.value - 0.5f) * _floorBounds.size.z;
            return (x, y, z);
        }

        public void Dispose()
        {
            _disposeCts.Cancel();
            _disposeCts.Dispose();
        }
    }
}