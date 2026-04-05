using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Views;

namespace Views.Bug.Strategies.Implementations
{
    public class SearchNearestFoodStrategy : ISearchFoodStrategy
    {
        public FoodTarget Target { get; set; }

        private readonly BugView _bugView;
        private readonly IEnumerable<FoodTarget>[] _targets;

        public SearchNearestFoodStrategy(BugView bugView, IEnumerable<FoodTarget>[] targets)
        {
            _bugView = bugView;
            _targets = targets;
        }

        public void SearchFood()
        {
            FoodTarget nearestTarget = null;
            var minDistance = float.MaxValue;

            foreach (var target in _targets.SelectMany(x => x))
            {
                var distance = Vector3.Distance(_bugView.transform.position, target.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }

            Target = nearestTarget;
        }
    }
}