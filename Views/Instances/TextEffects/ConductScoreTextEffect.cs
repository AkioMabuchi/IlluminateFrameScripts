using System;
using DG.Tweening;
using Enums;
using ScriptableObjects;
using TMPro;
using UniRx;
using UnityEngine;

namespace Views.Instances.TextEffects
{
    public class ConductScoreTextEffect : MonoBehaviour
    {
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _emissionSaturation = Shader.PropertyToID("_EmissionSaturation");

        [SerializeField] private Transform transformTextEffect;

        [SerializeField] private MeshRenderer meshRendererTextMeshPro;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialTextEffectConductScore;

        [SerializeField] private ElectricParams electricParams;
        
        [SerializeField] private float startPositionY;
        [SerializeField] private float endPositionY;

        [SerializeField] private float durationMove;
        [SerializeField] private Ease easeMove;

        [SerializeField] private float durationFade;
        [SerializeField] private Ease easeFade;

        [SerializeField] private float startSaturationBulb;
        [SerializeField] private float startSaturationNormal;
        [SerializeField] private float startSaturationCorrect;
        [SerializeField] private float startSaturationIncorrect;
        [SerializeField] private float endSaturation;
        [SerializeField] private float durationSaturation;
        [SerializeField] private Ease easeSaturation;

        [SerializeField] private float fontSizeBulbLineCount;
        [SerializeField] private float fontSizeBulbLines;
        [SerializeField] private float fontSizeBulbScore;
        [SerializeField] private float fontSizeBulbPts;
        [SerializeField] private float fontSizeNormalLineCount;
        [SerializeField] private float fontSizeNormalLines;
        [SerializeField] private float fontSizeNormalScore;
        [SerializeField] private float fontSizeNormalPts;
        [SerializeField] private float fontSizeCorrectLineCount;
        [SerializeField] private float fontSizeCorrectLines;
        [SerializeField] private float fontSizeCorrectCorrectPolarity;
        [SerializeField] private float fontSizeCorrectScore;
        [SerializeField] private float fontSizeCorrectPts;
        [SerializeField] private float fontSizeIncorrectLineCount;
        [SerializeField] private float fontSizeIncorrectLines;
        [SerializeField] private float fontSizeIncorrectScore;
        [SerializeField] private float fontSizeIncorrectPts;
        
        private readonly Subject<ConductScoreTextEffect> _subjectOnFinished = new();
        public IObservable<ConductScoreTextEffect> OnFinished => _subjectOnFinished;

        private Sequence _sequence;

        private void Start()
        {
            meshRendererTextMeshPro.material = new Material(materialTextEffectConductScore);
        }

        public void Take()
        {
            meshRendererTextMeshPro.enabled = true;
        }

        public void Release()
        {
            meshRendererTextMeshPro.enabled = false;
        }

        public void StartEffect(Vector3 basePosition, int score, int lineCount, ElectricStatus electricStatus, 
            ConductScoreTextEffectMode effectMode)
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
                    .From(effectMode switch
                    {
                        ConductScoreTextEffectMode.Bulb => startSaturationBulb,
                        ConductScoreTextEffectMode.Normal => startSaturationNormal,
                        ConductScoreTextEffectMode.Correct => startSaturationCorrect,
                        ConductScoreTextEffectMode.Incorrect => startSaturationIncorrect,
                        _ => 0.0f
                    })
                    .SetEase(easeSaturation))
                .OnStart(() =>
                {
                    transformTextEffect.localPosition = basePosition;
                    meshRendererTextMeshPro.material.SetFloat(_emissionHue, electricParams.GetHue(electricStatus));
                    switch (effectMode)
                    {
                        case ConductScoreTextEffectMode.Bulb:
                        {
                            textMeshPro.text = "<size=" + fontSizeBulbLineCount + ">" + lineCount + "</size>" +
                                               "<size=" + fontSizeBulbLines + ">lines</size>\n" +
                                               "<size=" + fontSizeBulbScore + ">" + score + "</size>" +
                                               "<size=" + fontSizeBulbPts + ">pts</size>";
                            break;
                        }
                        case ConductScoreTextEffectMode.Normal:
                        {
                            textMeshPro.text = "<size=" + fontSizeNormalLineCount + ">" + lineCount + "</size>" +
                                               "<size=" + fontSizeNormalLines + ">lines</size>\n" +
                                               "<size=" + fontSizeNormalScore + ">" + score + "</size>" +
                                               "<size=" + fontSizeNormalPts + ">pts</size>";
                            break;
                        }
                        case ConductScoreTextEffectMode.Correct:
                        {
                            textMeshPro.text = "<size=" + fontSizeCorrectLineCount + ">" + lineCount + "</size>" +
                                               "<size=" + fontSizeCorrectLines + ">lines</size>\n" +
                                               "<size=" + fontSizeCorrectCorrectPolarity +
                                               ">CORRECT POLARITY</size>\n" +
                                               "<size=" + fontSizeCorrectScore + ">" + score + "</size>" +
                                               "<size=" + fontSizeCorrectPts + ">pts</size>";
                            break;
                        }
                        case ConductScoreTextEffectMode.Incorrect:
                        {
                            textMeshPro.text = "<size=" + fontSizeIncorrectLineCount + ">" + lineCount + "</size>" +
                                               "<size=" + fontSizeIncorrectLines + ">lines</size>\n" +
                                               "<size=" + fontSizeIncorrectScore + ">" + score + "</size>" +
                                               "<size=" + fontSizeIncorrectPts + ">pts</size>";
                            break;
                        }
                    }
                })
                .OnComplete(() =>
                {
                    _subjectOnFinished.OnNext(this);
                }).SetLink(gameObject);
        }
    }
}