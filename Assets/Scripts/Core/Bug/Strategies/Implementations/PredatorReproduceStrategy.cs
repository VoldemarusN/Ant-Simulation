using Core;
using Core.Bug.Factory;
using Core.Bug.Strategies;
using UniRx;

namespace Views.Bug.Strategies.Implementations
{
    public class PredatorReproduceStrategy : IReproduceStrategy
    {
        public Subject<BugController> Reproduced { get; set; } = new();

        private readonly PredatorSettings _predatorSettings;
        private readonly ILevelInfo _levelInfo;
        private readonly IAntFactory _antFactory;

        public PredatorReproduceStrategy(PredatorSettings predatorSettings, ILevelInfo levelInfo, IAntFactory antFactory)
        {
            _predatorSettings = predatorSettings;
            _levelInfo = levelInfo;
            _antFactory = antFactory;
        }

        public void SetFoodCount(int count)
        {
            if (count >= _predatorSettings.FoodAmountToSplit)
            {
                var instance = _antFactory.CreatePredatorBug();
                _levelInfo.Predators.Add(instance);
                Reproduced.OnNext(instance);
            }
        }
    }
}