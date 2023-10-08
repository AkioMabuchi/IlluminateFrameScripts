using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances.TextEffects;

namespace Views.TextEffectFactories
{
    public class LineCountTextEffectFactory : MonoBehaviour
    {
        [SerializeField] private LineCountTextEffect prefabLineCountTextEffect;
        [SerializeField] private float tileSize;
        [SerializeField] private int preloadLineCountTextEffectCount;

        [SerializeField] private Vector2 basePositionSmall;
        [SerializeField] private Vector2 basePositionMedium;
        [SerializeField] private Vector2 basePositionLarge;
        
        private ObjectPool<LineCountTextEffect> _objectPoolLineCountTextEffects;

        private Vector2 _currentBasePosition = Vector2.zero;
        private void Awake()
        {
            _objectPoolLineCountTextEffects = new ObjectPool<LineCountTextEffect>(
                () =>
                {
                    var lineCountTextEffect = Instantiate(prefabLineCountTextEffect, transform);
                    lineCountTextEffect.OnFinished.Subscribe(finishedLineCountTextEffect =>
                    {
                        _objectPoolLineCountTextEffects.Release(finishedLineCountTextEffect);
                    }).AddTo(lineCountTextEffect.gameObject);
                    return lineCountTextEffect;
                },
                takenLineCountTextEffect => takenLineCountTextEffect.Take(),
                releasedLineCountTextEffect => releasedLineCountTextEffect.Release(),
                destroyedLineCountTextEffect => Destroy(destroyedLineCountTextEffect.gameObject));
        }

        private void Start()
        {
            var preloadedLineCountTextEffects = new List<LineCountTextEffect>();

            for (var i = 0; i < preloadLineCountTextEffectCount; i++)
            {
                preloadedLineCountTextEffects.Add(_objectPoolLineCountTextEffects.Get());
            }

            foreach (var preloadedLineCountTextEffect in preloadedLineCountTextEffects)
            {
                _objectPoolLineCountTextEffects.Release(preloadedLineCountTextEffect);
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

        public void GenerateTextEffect(Vector2Int cellPosition, int lineCount, ElectricStatus electricStatus)
        {
            var basePosition = new Vector3(cellPosition.x * tileSize + _currentBasePosition.x, 0.0f,
                cellPosition.y * tileSize + _currentBasePosition.y);
            var lineCountTextEffect = _objectPoolLineCountTextEffects.Get();
            lineCountTextEffect.StartEffect(basePosition, lineCount, electricStatus);
        }
    }
}