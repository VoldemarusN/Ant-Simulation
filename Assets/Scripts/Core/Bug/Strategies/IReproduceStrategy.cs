using UniRx;
using Views.Bug;

namespace Core.Bug.Strategies
{
    public interface IReproduceStrategy
    {
        Subject<BugController> Reproduced { get; set; }
        void SetFoodCount(int count);
    }
}