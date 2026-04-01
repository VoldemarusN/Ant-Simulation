using System.Collections.Generic;
using UnityEngine;
using Views;

public abstract class Bug<TTarget> : FoodTarget where TTarget : FoodTarget
{
    public bool HasFoodTarget => _foodTarget;

    [SerializeField] private float _speed;
    private TTarget _foodTarget;

    public TTarget FindNearestFood(IEnumerable<TTarget> targets)
    {
        var minDistance = _foodTarget
            ? Vector3.Distance(transform.position, _foodTarget.transform.position)
            : float.MaxValue;

        foreach (var target in targets)
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > minDistance) continue;

            minDistance = distance;
            _foodTarget = target;
        }

        return _foodTarget;
    }

    public void MoveToTarget()
    {
        if (_foodTarget == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _foodTarget.transform.position,
            _speed * Time.deltaTime);
    }
}