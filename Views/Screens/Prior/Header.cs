using System;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UnityEngine;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens.Prior
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Header : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransformBackground;

        [SerializeField] private ImageButtonHeader imageButtonReturn;
        public ImageButtonHeader ImageButtonReturn => imageButtonReturn;
        
        [SerializeField] private TextMeshProUGUI textMeshProHeading;

        [SerializeField] private Localizer localizer;

        [SerializeField] private float durationShow;
        [SerializeField] private Ease easeShow;

        [SerializeField] private float durationHide;
        [SerializeField] private Ease easeHide;
        
        private Tweener _tweenerPull;

        private GameStateName _gameStateName;
        
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 1.0f;
            rectTransformBackground.anchoredPosition = new Vector2(0.0f, rectTransformBackground.sizeDelta.y);
            textMeshProHeading.text = "";
        }

        public void SetGameStateName(GameStateName gameStateName)
        {
            _gameStateName = gameStateName;
        }

        public void RenderText()
        {
            switch (_gameStateName)
            {
                case GameStateName.SelectFrameSize:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderSelectFrameSize;
                    break;
                }
                case GameStateName.Instruction:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderInstruction;
                    break;
                }
                case GameStateName.Settings:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderSettings;
                    break;
                }
                case GameStateName.Records:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderRecords;
                    break;
                }
                case GameStateName.Achievements:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderAchievements;
                    break;
                }
                case GameStateName.Statistics:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderStatistics;
                    break;
                }
                case GameStateName.Credits:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderCredits;
                    break;
                }
                case GameStateName.Licenses:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderLicenses;
                    break;
                }
                case GameStateName.ResultRecords:
                {
                    textMeshProHeading.text = localizer.CurrentLocale.HeaderRecords;
                    break;
                }
                default:
                {
                    textMeshProHeading.text = "";
                    break;
                }
            }
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            
            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(0.0f, durationShow)
                .SetEase(easeShow)
                .SetLink(gameObject);
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(rectTransformBackground.sizeDelta.y, durationHide)
                .SetEase(easeHide)
                .SetLink(gameObject);
        }
    }
}
