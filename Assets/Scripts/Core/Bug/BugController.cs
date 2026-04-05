using System;
using System.Collections.Generic;
using R3;
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
        private readonly CancellationDisposable _cancellationDisposable;


        public BugController(BugView view, ISearchFoodStrategy searchFoodStrategy, IMoveStrategy moveStrategy)
        {
            View = view;
            _searchFoodStrategy = searchFoodStrategy;
            _moveStrategy = moveStrategy;
            _cancellationDisposable = new CancellationDisposable();

            Observable.EveryUpdate().Take(5).Subscribe(_ => _searchFoodStrategy.SearchFood()).RegisterTo(_cancellationDisposable.Token);
            Observable.EveryUpdate().Subscribe(_ => _moveStrategy.Move()).RegisterTo(_cancellationDisposable.Token);
        }

        public void Dispose()
        {
            _cancellationDisposable?.Dispose();
        }
    }
}