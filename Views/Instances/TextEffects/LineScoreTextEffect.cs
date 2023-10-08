using System;
using DG.Tweening;
using Enums;
using ScriptableObjects;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Instances.TextEffects
{
    public class LineScoreTextEffect : MonoBehaviour
    {
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _emissionSaturation = Shader.PropertyToID("_EmissionSaturation");

        [SerializeField] private Transform transformTextEffect;

        [SerializeField] private MeshRenderer meshRendererTextMeshPro;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialTextEffectLineScore;

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
        
        [SerializeField] private float fontSizeScore;
        [SerializeField] private float fontSizePts;

        private readonly Subject<LineScoreTextEffect> _subjectOnFinished = new();
        public IObservable<LineScoreTextEffect> OnFinished => _subjectOnFinished;

        private Sequence _sequence;

        private void Start()
        {
            meshRendererTextMeshPro.material = new Material(materialTextEffectLineScore);
        }

        public void Take()
        {
            meshRendererTextMeshPro.enabled = true;
        }

        public void Release()
        {
            meshRendererTextMeshPro.enabled = false;
        }

        public void StartEffect(Vector3 basePosition, int lineCount, TextEffectMaterialType materialType)
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
                    meshRendererTextMeshPro.material.SetFloat(_emissionHue, electricParams.GetHue(materialType));
                    textMeshPro.text = "<size=" + fontSizeScore + ">" + lineCount + "</size>" +
                                       "<size=" + fontSizePts + ">pts</size>";
                })
                .OnComplete(() =>
                {
                    _subjectOnFinished.OnNext(this);
                }).SetLink(gameObject);
        }
    }
}