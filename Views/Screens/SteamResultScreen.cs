using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Enums.ScreenButtonNames;
using Interfaces;
using Structs;
using TMPro;
using UniRx;
using UnityEngine;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SteamResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private CanvasGroup canvasGroupFrame;
        [SerializeField] private CanvasGroup canvasGroupButtons;
        [SerializeField] private TextMeshProUGUI textMeshProHeading;
        [SerializeField] private TextMeshProUGUI textMeshProScore;
        [SerializeField] private TextMeshProUGUI textMeshProLineCounts;
        [SerializeField] private TextMeshProUGUI textMeshProAchievements;
        
        [SerializeField] private EnhancedScrollerResultAchievements enhancedScrollerResultAchievements;

        public EnhancedScrollerResultAchievements EnhancedScrollerResultAchievements =>
            enhancedScrollerResultAchievements;

        [SerializeField] private ImageButtonResult imageButtonRetry;
        public ImageButtonResult ImageButtonRetry => imageButtonRetry;
        [SerializeField] private ImageButtonResult imageButtonTitle;
        public ImageButtonResult ImageButtonTitle => imageButtonTitle;
        [SerializeField] private ImageButtonResult imageButtonRecords;
        public ImageButtonResult ImageButtonRecords => imageButtonRecords;
        [SerializeField] private ImageButtonResult imageButtonQuit;
        public ImageButtonResult ImageButtonQuit => imageButtonQuit;

        [SerializeField] private Localizer localizer;

        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

        private Sequence _sequenceFade;

        private int _score;
        private readonly List<int> _lineCounts = new();
        private readonly Queue<ILineCountDifference> _queueLineCountDifferences = new();
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void ShareScore(int score)
        {
            _score = score;
        }

        public void EnqueueLineCountDifference(ILineCountDifference lineCountDifference)
        {
            _queueLineCountDifferences.Enqueue(lineCountDifference);
        }

        public void RefreshLineCounts()
        {
            while (_queueLineCountDifferences.Count > 0)
            {
                var lineCountDifference = _queueLineCountDifferences.Dequeue();
                switch (lineCountDifference)
                {
                    case LineCountDifferenceAdd lineCountDifferenceAdd:
                    {
                        _lineCounts.Add(lineCountDifferenceAdd.lineCount);
                        break;
                    }
                    case LineCountDifferenceReset:
                    {
                        _lineCounts.Clear();
                        break;
                    }
                }
            }
        }

        public void Localize()
        {
            imageButtonRetry.Localize(localizer.CurrentLocale.ResultScreenButtonRetry);
            imageButtonTitle.Localize(localizer.CurrentLocale.ResultScreenButtonTitle);
            imageButtonRecords.Localize(localizer.CurrentLocale.ResultScreenButtonRecords);
            imageButtonQuit.Localize(localizer.CurrentLocale.ResultScreenButtonGameQuit);
        }

        public void Render()
        {
            var textLineCounts = "";
            if (_lineCounts.Count > 0)
            {
                for (var i = 0; i < _lineCounts.Count; i++)
                {
                    if (i >= 1)
                    {
                        textLineCounts += ",";
                    }

                    textLineCounts += _lineCounts[i];
                }
            }
            else
            {
                textLineCounts = localizer.CurrentLocale.ResultScreenNoLineCount;
            }

            textMeshProHeading.text = localizer.CurrentLocale.ResultScreenHeading;
            textMeshProScore.text = localizer.CurrentLocale.ResultScreenScore + ":" + _score;
            textMeshProLineCounts.text = localizer.CurrentLocale.ResultScreenLineCounts + ":" + textLineCounts;
            textMeshProAchievements.text = localizer.CurrentLocale.ResultScreenAchievements;
        }

        public void ShowUp()
        {
            _sequenceFade?.Kill();
            _sequenceFade= DOTween.Sequence()
                .Append(canvasGroupFrame.DOFade(1.0f, 1.0f)
                    .SetEase(Ease.InOutSine)
                ).OnStart(() =>
                {
                    canvasGroup.alpha = 1.0f;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;

                    canvasGroupFrame.alpha = 0.0f;
                    canvasGroupFrame.interactable = true;
                    canvasGroupFrame.blocksRaycasts = true;

                    canvasGroupButtons.alpha = 0.0f;
                    canvasGroupButtons.interactable = false;
                    canvasGroupButtons.blocksRaycasts = false;
                }).OnComplete(() =>
                {
                    canvasGroupButtons.alpha = 1.0f;
                    canvasGroupButtons.interactable = true;
                    canvasGroupButtons.blocksRaycasts = true;
                }).SetLink(gameObject);
        }

        public void Show()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, durationFadeIn)
                    .SetEase(easeFadeIn)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }).SetLink(gameObject);
        }
        public void Hide()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, durationFadeOut)
                    .SetEase(easeFadeOut)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }).SetLink(gameObject);
        }
    }
}
