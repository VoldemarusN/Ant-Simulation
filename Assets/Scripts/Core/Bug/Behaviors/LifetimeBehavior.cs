using System;
using UniRx;

namespace Core.Bug.Behaviors
{
    public class LifetimeBehavior : IBugBehavior
    {
        private readonly float _lifetime;
        private CompositeDisposable _disposable;

        public LifetimeBehavior(float lifetime)
        {
            _lifetime = lifetime;
        }

        public void Initialize(BugController owner)
        {
            _disposable = new CompositeDisposable();

            owner.FoodCount
                .Select(_ => Observable.Timer(TimeSpan.FromSeconds(_lifetime)))
                .Switch()
                .Subscribe(_ => owner.View.Eat())
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
