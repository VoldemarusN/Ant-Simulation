using Views;

namespace Core.Bug.Strategies
{
    public interface IMoveStrategy
    {
        void Move();
        void SetTarget(FoodTarget target);
    }
}