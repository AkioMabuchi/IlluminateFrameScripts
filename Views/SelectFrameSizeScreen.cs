using System;
using DG.Tweening;
using Enums;
using UniRx;
using UnityEngine;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SelectFrameSizeScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private ImageButtonSelectFrameSize imageButtonSmall;
        [SerializeField] private ImageButtonSelectFrameSize imageButtonMedium;
        [SerializeField] private ImageButtonSelectFrameSize imageButtonLarge;

        public IObservable<Unit> OnPointerEnterImageButtonSmall => imageButtonSmall.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonMedium => imageButtonMedium.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonLarge => imageButtonLarge.OnPointerEnter;

        public IObservable<Unit> OnPointerExitImageButtonSmall => imageButtonSmall.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonMedium => imageButtonMedium.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonLarge => imageButtonLarge.OnPointerExit;
        
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            ZoomUpButtons(SelectFrameSizeScreenButtonName.None);
        }

        public void ChangeImageButtonTexts()
        {
            imageButtonSmall.ChangeTexts(LocaleKey.SelectFrameSizeSmall, LocaleKey.SelectFrameSizeSmallDetails);
            imageButtonMedium.ChangeTexts(LocaleKey.SelectFrameSizeMedium, LocaleKey.SelectFrameSizeMediumDetails);
            imageButtonLarge.ChangeTexts(LocaleKey.SelectFrameSizeLarge, LocaleKey.SelectFrameSizeLargeDetails);
        }

        public void FadeIn()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1.0f, 1.0f).SetLink(gameObject);
        }

        public void FadeOut()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0.0f, 1.0f).SetLink(gameObject);
        }

        public void ZoomUpButtons(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            imageButtonSmall.ZoomUp(selectFrameSizeScreenButtonName == SelectFrameSizeScreenButtonName.Small);
            imageButtonMedium.ZoomUp(selectFrameSizeScreenButtonName == SelectFrameSizeScreenButtonName.Medium);
            imageButtonLarge.ZoomUp(selectFrameSizeScreenButtonName == SelectFrameSizeScreenButtonName.Large);
        }
    }
}
