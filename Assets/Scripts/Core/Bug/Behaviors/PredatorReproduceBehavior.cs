using Core.Bug.Factory;
using Core.Bug.Settings;
using UniRx;

namespace Core.Bug.Behaviors
{
    public class PredatorReproduceBehavior : IBugBehavior
    {
        private readonly PredatorSettings _settings;
        private readonly IAntFactory _factory;
        private CompositeDisposable _disposable;

        public PredatorReproduceBehavior(PredatorSettings settings, IAntFactory factory)
        {
            _settings = settings;
            _factory = factory;
        }

        public void Initialize(BugController owner)
        {
            _disposable = new CompositeDisposable();

            owner.FoodCount
                .Where(count => count >= _settings.FoodAmountToSplit)
                .Subscribe(_ =>
                {
                    owner.FoodCount.Value = 0;
                    var newBug = _factory.CreatePredatorBug();
                    owner.Reproduced.OnNext(newBug);
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
