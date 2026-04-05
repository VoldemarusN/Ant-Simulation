using System;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using Views;

namespace Views.Bug.Strategies.Implementations
{
    public class SearchVegetableFoodStrategy : SearchNearestFoodStrategy, IDisposable
    {
        private readonly ObservableList<VegetableTarget> _targets;

        public SearchVegetableFoodStrategy(BugView bugView, ObservableList<VegetableTarget> targets) : base(bugView, new IEnumerable<FoodTarget>[] { targets })
        {
            _targets = targets;
            _targets.CollectionChanged += OnTargetsChanged;
        }

        public void Dispose()
        {
            _targets.CollectionChanged -= OnTargetsChanged;
        }

        private void OnTargetsChanged(in NotifyCollectionChangedEventArgs<VegetableTarget> e)
        {
            SearchFood();
        }
    }
}