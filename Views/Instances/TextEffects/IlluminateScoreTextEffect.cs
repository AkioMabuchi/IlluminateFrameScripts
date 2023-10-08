using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Views.Instances.TextEffects
{
    public class IlluminateScoreTextEffect : MonoBehaviour
    {
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        private static readonly int _emissionColor = Shader.PropertyToID("_EmissionColor");

        [SerializeField] private Transform transformTextEffect;

        [SerializeField] private MeshRenderer meshRendererTextMeshPro;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialTextEffectIlluminated;
        
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

        [SerializeField] private float fontSizeIlluminated;
        [SerializeField] private float fontSizeScore;
        [SerializeField] private float fontSizePts;

        private readonly Subject<IlluminateScoreTextEffect> _subjectOnFinished = new();
        public IObservable<IlluminateScoreTextEffect> OnFinished => _subjectOnFinished;

        private Sequence _sequence;

        private void Start()
        {
            meshRendererTextMeshPro.material = materialTextEffectIlluminated;
        }

        public void Take()
        {
            meshRendererTextMeshPro.enabled = true;
        }

        public void Release()
        {
            meshRendererTextMeshPro.enabled = false;
        }

        public void StartEffect(Vector3 basePosition, int score)
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
                    textMeshPro.text = "<size=" + fontSizeIlluminated + ">Illuminated!!</size>\n" +
                                       "<size=" + fontSizeScore + ">" + score + "</size>" +
                                       "<size=" + fontSizePts + ">pts!</size>";
                })
                .OnComplete(() =>
                {
                    _subjectOnFinished.OnNext(this);
                }).SetLink(gameObject);
        }
    }
}