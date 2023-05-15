using System;
using Classes.Statics;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Header : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransformBackground;

        [SerializeField] private ImageButton imageButtonReturn;
        [SerializeField] private TextMeshProUGUI textMeshProHeading;
        
        [SerializeField] private float durationPull;
        public IObservable<Unit> OnPointerEnterImageButtonReturn => imageButtonReturn.OnPointerEnter;
        public IObservable<Unit> OnPointerExitImageButtonReturn => imageButtonReturn.OnPointerExit;

        private Tweener _tweenerPull;
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

        public void ChangeHeadingText(HeaderHeadingText headingText)
        {
            textMeshProHeading.text = Localize.LocaleString(headingText switch
            {
                HeaderHeadingText.SelectFrameSize => LocaleKey.HeaderSelectFrameSize,
                _ => LocaleKey.None
            });
        }

        public void PullDown()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            
            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(0.0f, durationPull)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
        }

        public void PullUp()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(rectTransformBackground.sizeDelta.y, durationPull)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
        }

        public void ZoomUpButtons(HeaderButtonName headerButtonName)
        {
            imageButtonReturn.ZoomUp(headerButtonName == HeaderButtonName.Return);
        }
    }
}
