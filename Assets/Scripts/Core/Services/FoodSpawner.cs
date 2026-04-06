using System;
using Core.Food;
using Core.Level;
using Core.Services;
using UniRx;
using UnityEngine;

namespace Core.Services
{
    public class FoodSpawner : IFoodSpawner
    {
        private readonly SimulationSettings _settings;
        private readonly VegetableFoodFactory _vegetableFoodFactory;
        private readonly LevelPositionService _levelPositionService;
        private readonly ILevelInfo _levelInfo;
        
        private readonly CompositeDisposable _disposables = new();

        public FoodSpawner(
            SimulationSettings settings,
            VegetableFoodFactory vegetableFoodFactory,
            LevelPositionService levelPositionService,
            ILevelInfo levelInfo)
        {
            _settings = settings;
            _vegetableFoodFactory = vegetableFoodFactory;
            _levelPositionService = levelPositionService;
            _levelInfo = levelInfo;
        }

        public void StartSpawning()
        {
            Observable.Interval(TimeSpan.FromSeconds(_settings.FoodSpawnPeriod))
                .Subscribe(_ => SpawnFood())
                .AddTo(_disposables);
        }

        public void StopSpawning()
        {
            _disposables.Clear();
        }

        private void SpawnFood()
        {
            var pos = _levelPositionService.GetRandomPoint();
            var foodInstance = _vegetableFoodFactory.Spawn(pos);
            _levelInfo.Vegetables.Add(foodInstance);
            
            foodInstance.Eaten
                .Subscribe(OnFoodEaten)
                .AddTo(_disposables);
        }

        private void OnFoodEaten(FoodTarget food)
        {
            _levelInfo.Vegetables.Remove(food as VegetableTarget);
            _vegetableFoodFactory.Despawn(food as VegetableTarget);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}