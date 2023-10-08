using System;
using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.TextEffects;

namespace Views.TextEffectFactories
{
    public class NowhereTextEffectFactory : MonoBehaviour
    {
        [SerializeField] private NowhereTextEffect prefabNowhereTextEffect;
        [SerializeField] private int preloadNowhereTextEffectCount;

        [SerializeField] private Vector2 basePositionSmall;
        [SerializeField] private Vector2 basePositionMedium;
        [SerializeField] private Vector2 basePositionLarge;

        [SerializeField] private Localizer localizer;
        
        private ObjectPool<NowhereTextEffect> _objectPoolNowhereTextEffects;

        private Vector2 _currentBasePosition = Vector2.zero;
        private void Awake()
        {
            _objectPoolNowhereTextEffects = new ObjectPool<NowhereTextEffect>(
                () =>
                {
                    var nowhereTextEffect = Instantiate(prefabNowhereTextEffect, transform);
                    nowhereTextEffect.OnFinished.Subscribe(finishedNowhereTextEffect =>
                    {
                        _objectPoolNowhereTextEffects.Release(finishedNowhereTextEffect);
                    }).AddTo(nowhereTextEffect.gameObject);
                    return nowhereTextEffect;
                },
                takenNowhereTextEffect => takenNowhereTextEffect.Take(),
                releasedNowhereTextEffectCount => releasedNowhereTextEffectCount.Release(),
                destroyedNowhereTextEffectCount => Destroy(destroyedNowhereTextEffectCount.gameObject));
        }

        private void Start()
        {
            var preloadedNowhereTextEffects = new List<NowhereTextEffect>();

            for (var i = 0; i < preloadNowhereTextEffectCount; i++)
            {
                preloadedNowhereTextEffects.Add(_objectPoolNowhereTextEffects.Get());
            }

            foreach (var preloadedNowhereTextEffect in preloadedNowhereTextEffects)
            {
                _objectPoolNowhereTextEffects.Release(preloadedNowhereTextEffect);
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

        public void GenerateTextEffect()
        {
            var basePosition = new Vector3(_currentBasePosition.x, 0.0f, _currentBasePosition.y);
            var nowhereTextEffect = _objectPoolNowhereTextEffects.Get();
            nowhereTextEffect.StartEffect(basePosition, localizer.CurrentLocale.TextEffectNoPlace);
        }
    }
}