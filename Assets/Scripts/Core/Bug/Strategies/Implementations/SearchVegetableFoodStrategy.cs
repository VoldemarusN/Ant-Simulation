using System;
using System.Collections.Generic;
using System.Linq;
using Views;

namespace Views.Bug.Strategies.Implementations
{
    public class SearchVegetableFoodStrategy : SearchNearestFoodStrategy
    {
        private readonly List<VegetableTarget> _targets;

        public SearchVegetableFoodStrategy(BugView bugView, List<VegetableTarget> targets) : base(bugView, new IEnumerable<FoodTarget>[] { targets })
        {
            _targets = targets;
        }
    
    }
}