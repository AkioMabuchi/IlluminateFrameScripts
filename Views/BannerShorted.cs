using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BannerShorted : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        [SerializeField] private List<Image> imageLines;
        [SerializeField] private TextMeshProUGUI textMeshProMain;
        
        [SerializeField] private Sprite[] spritesLines;

        [SerializeField] private Color colorA;
        [SerializeField] private Color colorB;
        [SerializeField] private float inverseLerpSpeed;
        [SerializeField] private float changeLineIntervalTime = 1.0f;
        [SerializeField] private float durationFadeOut;

        private bool _isShown;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _isShown)
                .Subscribe(_ =>
                {
                    var l = Mathf.InverseLerp(-1.0f, 1.0f, Mathf.Sin(Time.time * inverseLerpSpeed));
                    var colorLine = Color.Lerp(colorA, colorB, l);
                    var colorText = Color.Lerp(colorB, colorA, l);
                    foreach (var imageLine in imageLines)
                    {
                        imageLine.color = colorLine;
                    }

                    textMeshProMain.color = colorText;
                }).AddTo(gameObject);

            Observable.Interval(TimeSpan.FromSeconds(changeLineIntervalTime))
                .Where(_ => _isShown)
                .Subscribe(_ =>
                {
                    foreach (var imageLine in imageLines)
                    {
                        imageLine.sprite = spritesLines[UnityEngine.Random.Range(0, spritesLines.Length)];
                    }
                }).AddTo(gameObject);
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            _isShown = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1.0f;

            _isShown = true;
        }

        public void FadeOut()
        {
            canvasGroup.DOFade(0.0f, durationFadeOut)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    _isShown = false;
                })
                .SetLink(gameObject);
        }
    }
}
