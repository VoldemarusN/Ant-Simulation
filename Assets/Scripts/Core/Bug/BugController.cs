using System;
using System.Collections.Generic;
using Core.Bug.Strategies;
using UniRx;
using UnityEngine;
using Zenject;
using Views.Bug.Strategies;
using Views.Bug.Strategies.Implementations;

namespace Views.Bug
{
    public class BugController : IDisposable
    {
        public BugView View { get; }
        public ReactiveProperty<int> FoodCount { get; } = new();
        public Subject<BugController> Reproduced { get; } = new();

        private readonly ISearchFoodStrategy _searchFoodStrategy;
        private readonly IMoveStrategy _moveStrategy;
        private readonly IReproduceStrategy _reproduceStrategy;

        private readonly CompositeDisposable _cancellationDisposable;

        public BugController(BugView view, ISearchFoodStrategy searchFoodStrategy, IMoveStrategy moveStrategy, IReproduceStrategy reproduceStrategy)
        {
            View = view;
            _searchFoodStrategy = searchFoodStrategy;
            _moveStrategy = moveStrategy;
            _reproduceStrategy = reproduceStrategy;
            _cancellationDisposable = new CompositeDisposable();

            FoodCount.Subscribe(_reproduceStrategy.SetFoodCount);

            _reproduceStrategy.Reproduced.Subscribe(Reproduced);
            _reproduceStrategy.Reproduced.Subscribe(_ => FoodCount.Value = 0);

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

        private void TryToEatTarget()
        {
            if (_searchFoodStrategy.Target && Vector3.Distance(_searchFoodStrategy.Target.transform.position, View.transform.position) < 0.5f)
            {
                _searchFoodStrategy.Target.Eat();
                _reproduceStrategy.SetFoodCount(++FoodCount.Value);
            }
        }

        public void Dispose()
        {
            _cancellationDisposable?.Dispose();
        }
    }
}