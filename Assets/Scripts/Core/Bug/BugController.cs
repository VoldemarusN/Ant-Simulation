using System;
using Core.Bug.Strategies;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Bug
{
    public class BugController : IDisposable
    {
        public BugView View { get; }
        public Subject<BugController> Reproduced { get; } = new();

        private readonly ISearchFoodStrategy _searchFoodStrategy;
        private readonly IMoveStrategy _moveStrategy;
        private readonly IReproduceStrategy _reproduceStrategy;

        private ReactiveProperty<int> FoodCount { get; } = new();
        private CompositeDisposable _cancellationDisposable;

        public BugController(BugView view, ISearchFoodStrategy searchFoodStrategy, IMoveStrategy moveStrategy, IReproduceStrategy reproduceStrategy)
        {
            View = view;
            _searchFoodStrategy = searchFoodStrategy;
            _moveStrategy = moveStrategy;
            _reproduceStrategy = reproduceStrategy;

            _reproduceStrategy.Reproduced.Subscribe(Reproduced);
            _reproduceStrategy.Reproduced.Subscribe(_ => FoodCount.Value = 0);
            FoodCount.Subscribe(_reproduceStrategy.SetFoodCount);
        }

        public void Initialize(float? lifetime = null)
        {
            _cancellationDisposable = new CompositeDisposable();

            if (lifetime.HasValue && lifetime.Value > 0)
            {
                FoodCount
                    .Select(_ => Observable.Timer(TimeSpan.FromSeconds(lifetime.Value)))
                    .Switch()
                    .Subscribe(_ => View.Eat())
                    .AddTo(_cancellationDisposable);
            }

            int counter = 0;
            Observable.EveryUpdate()
                .Where(_ => ++counter % 5 == 0)
                .Subscribe(_ =>
                {
                    _searchFoodStrategy.SearchFood();
                    _moveStrategy.SetTarget(_searchFoodStrategy.Target);
                })
                .AddTo(_cancellationDisposable);

            Observable.EveryUpdate().Subscribe(_ => _moveStrategy.Move())
                .AddTo(_cancellationDisposable);

            Observable.EveryUpdate().Subscribe(_ => TryToEatTarget())
                .AddTo(_cancellationDisposable);
        }

        public void Dispose()
        {
            FoodCount.Value = 0;
            _cancellationDisposable?.Dispose();
        }

        private void TryToEatTarget()
        {
            if (_searchFoodStrategy.Target && Vector3.Distance(_searchFoodStrategy.Target.transform.position, View.transform.position) < 3f)
            {
                _searchFoodStrategy.Target.Eat();
                _searchFoodStrategy.Target = null;

                FoodCount.Value++;
            }
        }
    }
}