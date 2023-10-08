using System;
using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.TextEffects;

namespace Views.TextEffectFactories
{
    public class LineScoreTextEffectFactory : MonoBehaviour
    {
        [SerializeField] private LineScoreTextEffect prefabLineScoreTextEffect;
        [SerializeField] private float tileSize;
        [SerializeField] private int preloadLineScoreTextEffectCount;

        [SerializeField] private Vector2 basePositionSmall;
        [SerializeField] private Vector2 basePositionMedium;
        [SerializeField] private Vector2 basePositionLarge;

        private ObjectPool<LineScoreTextEffect> _objectPoolLineScoreTextEffects;
        
        private Vector2 _currentBasePosition = Vector2.zero;

        private void Awake()
        {
            _objectPoolLineScoreTextEffects = new ObjectPool<LineScoreTextEffect>(
                () =>
                {
                    var lineScoreTextEffect = Instantiate(prefabLineScoreTextEffect, transform);
                    lineScoreTextEffect.OnFinished.Subscribe(finishedLineScoreTextEffect =>
                    {
                        _objectPoolLineScoreTextEffects.Release(finishedLineScoreTextEffect);
                    }).AddTo(lineScoreTextEffect.gameObject);
                    return lineScoreTextEffect;
                },
                takenLineScoreTextEffect => takenLineScoreTextEffect.Take(),
                releasedLineScoreTextEffect => releasedLineScoreTextEffect.Release(),
                destroyedLineScoreTextEffect => Destroy(destroyedLineScoreTextEffect.gameObject));
        }

        private void Start()
        {
            var preloadedLineScoreTextEffects = new List<LineScoreTextEffect>();

            for (var i = 0; i < preloadLineScoreTextEffectCount; i++)
            {
                preloadedLineScoreTextEffects.Add(_objectPoolLineScoreTextEffects.Get());
            }

            foreach (var preloadedLineScoreTextEffect in preloadedLineScoreTextEffects)
            {
                _objectPoolLineScoreTextEffects.Release(preloadedLineScoreTextEffect);
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

        public void GenerateTextEffect(Vector2Int cellPosition, int score, TextEffectMaterialType materialType)
        {
            var basePosition = new Vector3(cellPosition.x * tileSize + _currentBasePosition.x, 0.0f,
                cellPosition.y * tileSize + _currentBasePosition.y);
            var lineScoreTextEffect = _objectPoolLineScoreTextEffects.Get();
            lineScoreTextEffect.StartEffect(basePosition, score, materialType);
        }
    }
}