using System.Collections.Generic;
using Core.Food;
using UniRx;
using UnityEngine;

namespace Core.Bug.Behaviors
{
    public class SearchNearestFoodBehavior : IBugBehavior
    {
        private readonly IEnumerable<FoodTarget>[] _sources;
        private CompositeDisposable _disposable;

        public SearchNearestFoodBehavior(params IEnumerable<FoodTarget>[] sources)
        {
            _sources = sources;
        }

        public void Initialize(BugController owner)
        {
            _disposable = new CompositeDisposable();
            int counter = 0;

            Observable.EveryUpdate()
                .Where(_ => ++counter % 5 == 0)
                .Subscribe(_ => Search(owner))
                .AddTo(_disposable);
        }

        private void Search(BugController owner)
        {
            FoodTarget nearest = null;
            var minDistance = float.MaxValue;

            foreach (var source in _sources)
            {
                foreach (var target in source)
                {
                    if (target == owner.View) continue;

                    var distance = Vector3.Distance(owner.View.transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearest = target;
                    }
                }
            }

            owner.CurrentTarget = nearest;
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
