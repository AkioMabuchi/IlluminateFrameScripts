using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Views.Instances.TextEffects
{
    public class NowhereTextEffect : MonoBehaviour
    {
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        private static readonly int _emissionColor = Shader.PropertyToID("_EmissionColor");

        [SerializeField] private Transform transformTextEffect;

        [SerializeField] private MeshRenderer meshRendererTextMeshPro;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialTextEffectNowhere;
        
        [SerializeField] private float startPositionY;
        [SerializeField] private float endPositionY;

        [SerializeField] private float durationMove;
        [SerializeField] private Ease easeMove;

        [SerializeField] private float durationFade;
        [SerializeField] private Ease easeFade;

        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;
        [SerializeField] private float durationColor;
        [SerializeField] private Ease easeColor;

        [SerializeField] private float fontSizeNowhere;

        private readonly Subject<NowhereTextEffect> _subjectOnFinished = new();
        public IObservable<NowhereTextEffect> OnFinished => _subjectOnFinished;

        private Sequence _sequence;

        private void Start()
        {
            meshRendererTextMeshPro.material = materialTextEffectNowhere;
        }

        public void Take()
        {
            meshRendererTextMeshPro.enabled = true;
        }

        public void Release()
        {
            meshRendererTextMeshPro.enabled = false;
        }

        public void StartEffect(Vector3 basePosition, string text)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(transformTextEffect.DOLocalMoveY(endPositionY, durationMove)
                    .From(startPositionY)
                    .SetEase(easeMove))
                .Join(meshRendererTextMeshPro.material.DOFloat(0.0f, _alpha, durationFade)
                    .From(1.0f)
                    .SetEase(easeFade))
                .Join(meshRendererTextMeshPro.material.DOColor(endColor, _emissionColor, durationColor)
                    .From(startColor)
                    .SetEase(easeColor))
                .OnStart(() =>
                {
                    transformTextEffect.localPosition = basePosition;
                    textMeshPro.text = "<size=" + fontSizeNowhere + ">" + text + "</size>";
                })
                .OnComplete(() =>
                {
                    _subjectOnFinished.OnNext(this);
                }).SetLink(gameObject);
        }
    }
}