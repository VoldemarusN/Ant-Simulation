using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Level
{
    public class LevelPositionService : IDisposable
    {
        private const int MAX_RANDOM_ATTEMPTS = 128;

        private readonly Renderer _renderer;
        private readonly SimulationSettings _settings;
        private readonly ILevelInfo _levelInfo;

        private readonly CancellationTokenSource _disposeCts = new();

        public LevelPositionService(Renderer renderer, SimulationSettings settings, ILevelInfo levelInfo)
        {
            _renderer = renderer;
            _settings = settings;
            _levelInfo = levelInfo;
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
                    return new Vector3(randomPosition.x, _renderer.transform.position.y, randomPosition.z);
                }
            }

            return default;
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

        public Vector3 GetRandomPoint()
        {
            var bounds = _renderer.bounds;

            var x = Random.Range(bounds.min.x, bounds.max.x);
            var z = Random.Range(bounds.min.z, bounds.max.z);
            var y = bounds.center.y;

            return new Vector3(x, y, z);
        }

        public void Dispose()
        {
            _disposeCts.Cancel();
            _disposeCts.Dispose();
        }
    }
}