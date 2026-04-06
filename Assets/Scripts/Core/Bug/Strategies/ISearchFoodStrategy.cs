using Core.Food;

namespace Core.Bug.Strategies
{
    public interface ISearchFoodStrategy
    {
        public FoodTarget Target { get; set; }
        void SearchFood();
    }
}