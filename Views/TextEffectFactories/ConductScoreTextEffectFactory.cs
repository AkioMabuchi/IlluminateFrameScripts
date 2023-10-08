using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Views.Instances.TextEffects;

namespace Views.TextEffectFactories
{
    public class ConductScoreTextEffectFactory : MonoBehaviour
    { 
        [SerializeField] private ConductScoreTextEffect prefabConductScoreTextEffect;
        [SerializeField] private float tileSize; 
        [SerializeField] private int preloadConductScoreTextEffectCount;

        [SerializeField] private Vector2 basePositionSmall;
        [SerializeField] private Vector2 basePositionMedium;
        [SerializeField] private Vector2 basePositionLarge;

        private ObjectPool<ConductScoreTextEffect> _objectPoolConductScoreTextEffects;
        
        private Vector2 _currentBasePosition = Vector2.zero;

        private void Awake()
        {
            _objectPoolConductScoreTextEffects = new ObjectPool<ConductScoreTextEffect>(
                () =>
                {
                    var conductScoreTextEffect = Instantiate(prefabConductScoreTextEffect, transform);
                    conductScoreTextEffect.OnFinished.Subscribe(finishedConductScoreTextEffect =>
                    {
                        _objectPoolConductScoreTextEffects.Release(finishedConductScoreTextEffect);
                    }).AddTo(conductScoreTextEffect.gameObject);
                    return conductScoreTextEffect;
                },
                takenConductScoreTextEffect => takenConductScoreTextEffect.Take(),
                releasedConductScoreTextEffect => releasedConductScoreTextEffect.Release(),
                destroyedConductScoreTextEffect => Destroy(destroyedConductScoreTextEffect.gameObject));
        }

        private void Start()
        {
            var preloadedConductScoreTextEffects = new List<ConductScoreTextEffect>();

            for (var i = 0; i < preloadConductScoreTextEffectCount; i++)
            {
                preloadedConductScoreTextEffects.Add(_objectPoolConductScoreTextEffects.Get());
            }

            foreach (var preloadedConductScoreTextEffect in preloadedConductScoreTextEffects)
            {
                _objectPoolConductScoreTextEffects.Release(preloadedConductScoreTextEffect);
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

        public void GenerateTextEffect(Vector2Int cellPosition, int score, int lineCount, ElectricStatus electricStatus,
            ConductScoreTextEffectMode effectMode)
        {
            var basePosition = new Vector3(cellPosition.x * tileSize + _currentBasePosition.x, 0.0f,
                cellPosition.y * tileSize + _currentBasePosition.y);
            var conductScoreTextEffect = _objectPoolConductScoreTextEffects.Get();
            conductScoreTextEffect.StartEffect(basePosition, score, lineCount, electricStatus, effectMode);
        }
    }
}