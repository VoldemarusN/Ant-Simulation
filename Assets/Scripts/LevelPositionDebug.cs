using UnityEngine;
using Zenject;
using Core;

public class LevelPositionDebug : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool _generate;
    [SerializeField] private int _pointsCount = 20;

    private LevelPositionService _service;
    private SimulationSettings _settings;

    private Vector3[] _points;

    [Inject]
    public void Construct(LevelPositionService service, SimulationSettings settings)
    {
        _service = service;
        _settings = settings;
    }

    private void OnDrawGizmos()
    {
        if (_service == null) return;

        // Нажал галочку — сгенерили точки один раз
        if (_generate)
        {
            _generate = false;

            _points = new Vector3[_pointsCount];

            for (int i = 0; i < _pointsCount; i++)
            {
                _points[i] = _service.GetRandomPosition();
            }
        }

        if (_points == null) return;

        foreach (var point in _points)
        {
            if (point == Vector3.zero) continue;

            // точка
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point, 0.25f);

            // safe zone
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
            Gizmos.DrawWireSphere(point, _settings.SpawnSafeZone);
        }
    }
}