using System;
using Core.Level;
using Core.Services;
using UniRx;

namespace Core.Services
{
    public class UIUpdater : IUIUpdater
    {
        private readonly GameUI _gameUI;
        private readonly ILevelInfo _levelInfo;
        
        private readonly CompositeDisposable _disposables = new();

        public UIUpdater(GameUI gameUI, ILevelInfo levelInfo)
        {
            _gameUI = gameUI;
            _levelInfo = levelInfo;
        }

        public void StartUpdating()
        {
            _levelInfo.EatenWorkersCount
                .Subscribe(count => _gameUI.SetWorkerCount(count))
                .AddTo(_disposables);

            _levelInfo.EatenPredatorsCount
                .Subscribe(count => _gameUI.SetPredatorCount(count))
                .AddTo(_disposables);
        }

        public void StopUpdating()
        {
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}