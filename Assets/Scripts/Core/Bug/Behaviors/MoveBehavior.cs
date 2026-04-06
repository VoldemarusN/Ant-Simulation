using UniRx;
using UnityEngine;

namespace Core.Bug.Behaviors
{
    public class MoveBehavior : IBugBehavior
    {
        private readonly float _speed;
        private CompositeDisposable _disposable;

        public MoveBehavior(float speed)
        {
            _speed = speed;
        }

        public void Initialize(BugController owner)
        {
            owner.View.SetAgentSpeed(_speed);
            _disposable = new CompositeDisposable();

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (owner.CurrentTarget != null)
                        owner.View.MoveTo(owner.CurrentTarget.transform.position);
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
