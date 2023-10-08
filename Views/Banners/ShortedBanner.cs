using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Banners
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShortedBanner : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Image[] imageLines;

        [SerializeField] private TextMeshProUGUI textMeshProMain;
        [SerializeField] private TextMeshProUGUI textMeshProSubL;
        [SerializeField] private TextMeshProUGUI textMeshProSubR;
        
        [SerializeField] private Sprite[] spritesLinesShorted;
        [SerializeField] private Sprite[] spritesLinesFatal;

        [SerializeField] private Color colorShortedA;
        [SerializeField] private Color colorShortedB;
        [SerializeField] private Color colorFatalA;
        [SerializeField] private Color colorFatalB;
        
        [SerializeField] private float inverseLerpSpeedShorted;
        [SerializeField] private float inverseLerpSpeedFatal;
        
        [SerializeField] private float changeLineIntervalTime;
        
        [SerializeField] private float durationFadeOut;
        [SerializeField] private Ease easeFadeOut;

        [SerializeField] private Localizer localizer;
        
        private ShortedStatus _shortedStatus = ShortedStatus.None;
        private int _fatalIntervalCount;
        
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    switch (_shortedStatus)
                    {
                        case ShortedStatus.Shorted:
                        {
                            var l = Mathf.InverseLerp(-1.0f, 1.0f, Mathf.Sin(Time.time * inverseLerpSpeedShorted));
                            var colorLine = Color.Lerp(colorShortedA, colorShortedB, l);
                            var colorText = Color.Lerp(colorShortedB, colorShortedA, l);
                            foreach (var imageLine in imageLines)
                            {
                                imageLine.color = colorLine;
                            }

                            textMeshProMain.color = colorText;
                            textMeshProSubL.color = colorText;
                            textMeshProSubR.color = colorText;
                            
                            break;
                        }
                        case ShortedStatus.Fatal:
                        {
                            var l = Mathf.InverseLerp(-1.0f, 1.0f, Mathf.Sin(Time.time * inverseLerpSpeedFatal));
                            var colorLine = Color.Lerp(colorFatalA, colorFatalB, l);
                            var colorText = Color.Lerp(colorFatalB, colorFatalA, l);
                            foreach (var imageLine in imageLines)
                            {
                                imageLine.color = colorLine;
                            }

                            textMeshProMain.color = colorText;
                            textMeshProSubL.color = colorText;
                            textMeshProSubR.color = colorText;
                            
                            break;
                        }
                    }

                }).AddTo(gameObject);

            Observable.Interval(TimeSpan.FromSeconds(changeLineIntervalTime))
                .Subscribe(_ =>
                {
                    switch (_shortedStatus)
                    {
                        case ShortedStatus.Shorted:
                        {
                            foreach (var imageLine in imageLines)
                            {
                                imageLine.sprite =
                                    spritesLinesShorted[UnityEngine.Random.Range(0, spritesLinesShorted.Length)];
                            }

                            break;
                        }
                        case ShortedStatus.Fatal:
                        {
                            foreach (var imageLine in imageLines)
                            {
                                imageLine.sprite =
                                    spritesLinesFatal[_fatalIntervalCount % spritesLinesFatal.Length];
                            }

                            _fatalIntervalCount++;
                            break;
                        }
                    }
                }).AddTo(gameObject);
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show(ShortedStatus shortedStatus)
        {
            canvasGroup.alpha = 1.0f;

            switch (shortedStatus)
            {
                case ShortedStatus.Shorted:
                {
                    textMeshProMain.text = localizer.CurrentLocale.ShortedBannerMainShorted;
                    textMeshProSubL.text = localizer.CurrentLocale.ShortedBannerSubLShorted;
                    textMeshProSubR.text = localizer.CurrentLocale.ShortedBannerSubRShorted;
                    break;
                }
                case ShortedStatus.Fatal:
                {
                    textMeshProMain.text = localizer.CurrentLocale.ShortedBannerMainFatal;
                    textMeshProSubL.text = localizer.CurrentLocale.ShortedBannerSubLFatal;
                    textMeshProSubR.text = localizer.CurrentLocale.ShortedBannerSubRFatal;
                    break;
                }
            }

            _fatalIntervalCount = 0;
            _shortedStatus = shortedStatus;
        }

        public void FadeOut()
        {
            canvasGroup.DOFade(0.0f, durationFadeOut)
                .SetEase(easeFadeOut)
                .OnComplete(() =>
                {
                    _shortedStatus = ShortedStatus.None;
                })
                .SetLink(gameObject);
        }
    }
}
