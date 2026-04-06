using Core.Bug.Factory;
using Core.Bug.Settings;
using UniRx;

namespace Core.Bug.Strategies.Implementations
{
    public class PredatorReproduceStrategy : IReproduceStrategy
    {
        public Subject<BugController> Reproduced { get; set; } = new();

        private readonly PredatorSettings _predatorSettings;
        private readonly IAntFactory _antFactory;

        public PredatorReproduceStrategy(PredatorSettings predatorSettings, IAntFactory antFactory)
        {
            _predatorSettings = predatorSettings;
            _antFactory = antFactory;
        }

        public void SetFoodCount(int count)
        {
            if (count >= _predatorSettings.FoodAmountToSplit)
            {
                var instance = _antFactory.CreatePredatorBug();
                Reproduced.OnNext(instance);
            }
        }
    }
}