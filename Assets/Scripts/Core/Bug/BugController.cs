using System;
using System.Collections.Generic;
using Core.Bug.Behaviors;
using Core.Food;
using UniRx;

namespace Core.Bug
{
    public class BugController : IDisposable
    {
        public BugView View { get; }
        public Subject<BugController> Reproduced { get; } = new();
        public ReactiveProperty<int> FoodCount { get; } = new();

        public FoodTarget CurrentTarget { get; set; }

        private readonly IReadOnlyList<IBugBehavior> _behaviors;
        private CompositeDisposable _disposable;

        public BugController(BugView view, IReadOnlyList<IBugBehavior> behaviors)
        {
            View = view;
            _behaviors = behaviors;
        }

        public void Initialize()
        {
            _disposable = new CompositeDisposable();

            foreach (var behavior in _behaviors)
                behavior.Initialize(this);
        }

        public void Dispose()
        {
            foreach (var behavior in _behaviors)
                behavior.Dispose();

            FoodCount.Dispose();
            _disposable?.Dispose();
        }
    }
}