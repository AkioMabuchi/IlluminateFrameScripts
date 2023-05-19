using System;
using DG.Tweening;
using Enums;
using UniRx;
using UnityEngine;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class RecordsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private ImageButtonRecords imageButtonSmall;
        [SerializeField] private ImageButtonRecords imageButtonMedium;
        [SerializeField] private ImageButtonRecords imageButtonLarge;
        [SerializeField] private ImageButtonRecords imageButtonGlobal;
        [SerializeField] private ImageButtonRecords imageButtonFriends;

        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

        public IObservable<Unit> OnPointerEnterSmall => imageButtonSmall.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterMedium => imageButtonMedium.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterLarge => imageButtonLarge.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterGlobal => imageButtonGlobal.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterFriends => imageButtonFriends.OnPointerEnter;

        public IObservable<Unit> OnPointerExitSmall => imageButtonSmall.OnPointerExit;
        public IObservable<Unit> OnPointerExitMedium => imageButtonMedium.OnPointerExit;
        public IObservable<Unit> OnPointerExitLarge => imageButtonLarge.OnPointerExit;
        public IObservable<Unit> OnPointerExitGlobal => imageButtonGlobal.OnPointerExit;
        public IObservable<Unit> OnPointerExitFriends => imageButtonFriends.OnPointerExit;
        
        
        private Sequence _sequenceFade;

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

        public void FadeIn()
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

        public void FadeOut()
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

        public void ZoomUpButtons(RecordsScreenButtonName recordsScreenButtonName)
        {
            imageButtonSmall.ZoomUp(recordsScreenButtonName == RecordsScreenButtonName.Small);
            imageButtonMedium.ZoomUp(recordsScreenButtonName == RecordsScreenButtonName.Medium);
            imageButtonLarge.ZoomUp(recordsScreenButtonName == RecordsScreenButtonName.Large);
            imageButtonGlobal.ZoomUp(recordsScreenButtonName == RecordsScreenButtonName.Global);
            imageButtonFriends.ZoomUp(recordsScreenButtonName == RecordsScreenButtonName.Friends);
        }
    }
}
