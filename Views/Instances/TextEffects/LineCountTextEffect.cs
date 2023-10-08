using System;
using DG.Tweening;
using Enums;
using ScriptableObjects;
using TMPro;
using UniRx;
using UnityEngine;

namespace Views.Instances.TextEffects
{
    public class LineCountTextEffect : MonoBehaviour
    {
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _emissionSaturation = Shader.PropertyToID("_EmissionSaturation");
        
        [SerializeField] private Transform transformTextEffect;
        
        [SerializeField] private MeshRenderer meshRendererTextMeshPro;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialTextEffectLineCount;

        [SerializeField] private ElectricParams electricParams;

        [SerializeField] private float startPositionY;
        [SerializeField] private float endPositionY;

        [SerializeField] private float durationMove;
        [SerializeField] private Ease easeMove;
        
        [SerializeField] private float durationFade;
        [SerializeField] private Ease easeFade;
        
        [SerializeField] private float startSaturation;
        [SerializeField] private float endSaturation;
        [SerializeField] private float durationSaturation;
        [SerializeField] private Ease easeSaturation;

        [SerializeField] private float fontSizeLineCount;
        
        private readonly Subject<LineCountTextEffect> _subjectOnFinished = new();
        public IObservable<LineCountTextEffect> OnFinished => _subjectOnFinished;

        private Sequence _sequence;

        private void Start()
        {
            meshRendererTextMeshPro.material = materialTextEffectLineCount;
        }

        public void Take()
        {
            meshRendererTextMeshPro.enabled = true;
        }

        public void Release()
        {
            meshRendererTextMeshPro.enabled = false;
        }

        public void StartEffect(Vector3 basePosition, int lineCount, ElectricStatus electricStatus)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(transformTextEffect.DOLocalMoveY(endPositionY, durationMove)
                    .From(startPositionY)
                    .SetEase(easeMove))
                .Join(meshRendererTextMeshPro.material.DOFloat(0.0f, _alpha, durationFade)
                    .From(1.0f)
                    .SetEase(easeFade))
                .Join(meshRendererTextMeshPro.material.DOFloat(endSaturation, _emissionSaturation, durationSaturation)
                    .From(startSaturation)
                    .SetEase(easeSaturation))
                .OnStart(() =>
                {
                    transformTextEffect.localPosition = basePosition;
                    meshRendererTextMeshPro.material.SetFloat(_emissionHue, electricParams.GetHue(electricStatus));
                    textMeshPro.text = "<size=" + fontSizeLineCount + ">" + lineCount + "</size>";
                })
                .OnComplete(() =>
                {
                    _subjectOnFinished.OnNext(this);
                }).SetLink(gameObject);
        }
    }
}