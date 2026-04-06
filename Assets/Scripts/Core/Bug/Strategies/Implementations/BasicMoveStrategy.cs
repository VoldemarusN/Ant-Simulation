using Core.Food;

namespace Core.Bug.Strategies.Implementations
{
    public class BasicMoveStrategy : IMoveStrategy
    {
        private readonly BugView _bugView;

        private FoodTarget _target;

        public BasicMoveStrategy(BugView bugView, float speed)
        {
            _bugView = bugView;
            _bugView.SetAgentSpeed(speed);
        }

        public void SetTarget(FoodTarget target)
        {
            _target = target;
        }

        public void Move()
        {
            if (_target == null) return;

            _bugView.MoveTo(_target.transform.position);
        }
    }
}