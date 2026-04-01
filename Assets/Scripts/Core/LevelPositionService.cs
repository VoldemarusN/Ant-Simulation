using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    internal class LevelPositionService : IDisposable
    {
        private const int MAX_RANDOM_ATTEMPTS = 128;

        private readonly GameObject _floor;
        private readonly SimulationSettings _settings;

        private readonly CancellationTokenSource _disposeCts = new();

        public LevelPositionService(GameObject floor, SimulationSettings settings)
        {
            _floor = floor;
            _settings = settings;
        }

        /// <param name="spawnedObjects">
        /// На радиусе вблизи этих объектов, спавнится ничего не будет
        /// </param>
        /// <returns></returns>
        public Vector3 GetRandomPosition()
        {
            if (_disposeCts.IsCancellationRequested) return Vector3.zero;

            var bounds = GetFloorBounds();
            var safeZone = _settings.SpawnSafeZone;
            var safeZoneSqr = safeZone * safeZone;

            for (var i = 0; i < MAX_RANDOM_ATTEMPTS; i++)
            {
                var randomPosition = GetRandomPoint(bounds);

                var intersectsTask = Task.Run(
                    () => HasIntersection(randomPosition, spawnedObjects.Select(x => x.position), safeZoneSqr),
                    _disposeCts.Token
                );
                var intersects = intersectsTask.GetAwaiter().GetResult();
                if (!intersects)
                {
                    return randomPosition;
                }
            }

            return bounds.center;
        }

        private Bounds GetFloorBounds()
        {
            var position = _floor.transform.position;
            var scale = _floor.transform.lossyScale;
            var size = new Vector3(Mathf.Abs(scale.x), 0f, Mathf.Abs(scale.z));
            return new Bounds(position, size);
        }

        private static bool HasIntersection(Vector3 candidate, IEnumerable<Vector3> positions, float safeZoneSqr)
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

        private Vector3 GetRandomPoint(Bounds bounds)
        {
            var x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            var z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
            var y = _floor.transform.position.y;
            return new Vector3(x, y, z);
        }

        public void Dispose()
        {
            _disposeCts.Cancel();
            _disposeCts.Dispose();
        }
    }
}