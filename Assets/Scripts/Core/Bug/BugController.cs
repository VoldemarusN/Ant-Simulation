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

        private readonly ISearchFoodStrategy _searchFoodStrategy;
        private readonly IMoveStrategy _moveStrategy;
        private readonly CompositeDisposable _cancellationDisposable;


        public BugController(BugView view, ISearchFoodStrategy searchFoodStrategy, IMoveStrategy moveStrategy)
        {
            View = view;
            _searchFoodStrategy = searchFoodStrategy;
            _moveStrategy = moveStrategy;
            _cancellationDisposable = new CompositeDisposable();

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
        }

        public void Dispose()
        {
            _cancellationDisposable?.Dispose();
        }
    }
}