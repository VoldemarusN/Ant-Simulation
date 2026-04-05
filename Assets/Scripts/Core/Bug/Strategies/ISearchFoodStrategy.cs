using System.Collections.Generic;
using Views;

public interface ISearchFoodStrategy
{
    public FoodTarget Target { get; set; }
    void SearchFood();
}