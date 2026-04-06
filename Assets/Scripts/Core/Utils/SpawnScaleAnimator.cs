using System;
using UniRx;
using UnityEngine;

namespace Core.Utils
{
    public class SpawnScaleAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 1f;

        private Vector3 _initialScale;
        private IDisposable _scaleDisposable;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;

            float progress = 0f;
            _scaleDisposable?.Dispose();
            _scaleDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    progress += Time.deltaTime / _animationDuration;
                    transform.localScale = Vector3.Lerp(Vector3.zero, _initialScale, progress);

                    if (progress >= 1f)
                    {
                        transform.localScale = _initialScale;
                        _scaleDisposable?.Dispose();
                    }
                });
        }

        private void OnDisable()
        {
            _scaleDisposable?.Dispose();
        }
    }
}
