using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Views.Instances.TextEffects;

namespace Views.TextEffectFactories
{
    public class IlluminateScoreTextEffectFactory : MonoBehaviour
    { 
        [SerializeField] private IlluminateScoreTextEffect prefabIlluminateScoreTextEffect; 
        [SerializeField] private int preloadIlluminateScoreTextEffectCount;

        [SerializeField] private Vector2 basePositionSmall;
        [SerializeField] private Vector2 basePositionMedium;
        [SerializeField] private Vector2 basePositionLarge;

        private ObjectPool<IlluminateScoreTextEffect> _objectPoolIlluminateScoreTextEffects;
        
        private Vector2 _currentBasePosition = Vector2.zero;

        private void Awake()
        {
            _objectPoolIlluminateScoreTextEffects = new ObjectPool<IlluminateScoreTextEffect>(
                () =>
                {
                    var illuminateScoreTextEffect = Instantiate(prefabIlluminateScoreTextEffect, transform);
                    illuminateScoreTextEffect.OnFinished.Subscribe(finishedIlluminateScoreTextEffect =>
                    {
                        _objectPoolIlluminateScoreTextEffects.Release(finishedIlluminateScoreTextEffect);
                    }).AddTo(illuminateScoreTextEffect.gameObject);
                    return illuminateScoreTextEffect;
                },
                takenIlluminateScoreTextEffect => takenIlluminateScoreTextEffect.Take(),
                releasedIlluminateScoreTextEffect => releasedIlluminateScoreTextEffect.Release(),
                destroyedIlluminateScoreTextEffect => Destroy(destroyedIlluminateScoreTextEffect.gameObject));
        }

        private void Start()
        {
            var preloadedIlluminateScoreTextEffects = new List<IlluminateScoreTextEffect>();

            for (var i = 0; i < preloadIlluminateScoreTextEffectCount; i++)
            {
                preloadedIlluminateScoreTextEffects.Add(_objectPoolIlluminateScoreTextEffects.Get());
            }

            foreach (var preloadedIlluminateScoreTextEffect in preloadedIlluminateScoreTextEffects)
            {
                _objectPoolIlluminateScoreTextEffects.Release(preloadedIlluminateScoreTextEffect);
            }
        }

        public void Initialize(FrameSize frameSize)
        {
            _currentBasePosition = frameSize switch
            {
                FrameSize.Small => basePositionSmall,
                FrameSize.Medium => basePositionMedium,
                FrameSize.Large => basePositionLarge,
                _ => Vector2.zero
            };
        }

        public void GenerateTextEffect(int score)
        {
            var basePosition = new Vector3(_currentBasePosition.x, 0.0f, _currentBasePosition.y);
            var illuminateScoreTextEffect = _objectPoolIlluminateScoreTextEffects.Get();
            illuminateScoreTextEffect.StartEffect(basePosition, score);
        }
    }
}