using System;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Image imageFrame;
        [SerializeField] private Image imageBanner;
        [SerializeField] private CanvasGroup canvasGroupTexts;

        [SerializeField] private TextMeshProUGUI textMeshProVersion;
        
        [SerializeField] private ImageButtonTitle imageButtonGameStart;
        [SerializeField] private ImageButtonTitle imageButtonTutorial;
        [SerializeField] private ImageButtonTitle imageButtonInstruction;
        [SerializeField] private ImageButtonTitle imageButtonSettings;
        [SerializeField] private ImageButtonTitle imageButtonRecords;
        [SerializeField] private ImageButtonTitle imageButtonAchievements;
        [SerializeField] private ImageButtonTitle imageButtonCredits;
        [SerializeField] private ImageButtonTitle imageButtonQuit;

        public IObservable<Unit> OnPointerEnterImageButtonStart => imageButtonGameStart.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonTutorial => imageButtonTutorial.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonInstruction => imageButtonInstruction.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonSettings => imageButtonSettings.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonRecords => imageButtonRecords.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonAchievements => imageButtonAchievements.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonCredits => imageButtonCredits.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterImageButtonQuit => imageButtonQuit.OnPointerEnter;

        public IObservable<Unit> OnPointerExitImageButtonStart => imageButtonGameStart.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonTutorial => imageButtonTutorial.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonInstruction => imageButtonInstruction.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonSettings => imageButtonSettings.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonRecords => imageButtonRecords.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonAchievements => imageButtonAchievements.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonCredits => imageButtonCredits.OnPointerExit;
        public IObservable<Unit> OnPointerExitImageButtonQuit => imageButtonQuit.OnPointerExit;

        private Sequence _sequenceShowUp;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            textMeshProVersion.text = "VERSION " + Application.version;

            ZoomUpButtons(TitleScreenButtonName.None);
        }

        public void ShowUp()
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            canvasGroupTexts.alpha = 0.0f;
            canvasGroupTexts.interactable = false;
            canvasGroupTexts.blocksRaycasts = false;
            
            var colorFrame = imageFrame.color;
            colorFrame.a = 0.0f;
            imageFrame.color = colorFrame;

            var colorBanner = imageBanner.color;
            colorBanner.a = 0.0f;
            imageBanner.color = colorBanner;
            
            _sequenceShowUp = DOTween.Sequence()
                .Append(imageFrame.DOFade(1.0f, 1.0f).From(0.0f).SetEase(Ease.InSine))
                .Append(imageBanner.DOFade(1.0f, 1.0f).From(0.0f).SetEase(Ease.InSine))
                .AppendInterval(0.3f)
                .OnComplete(() =>
                {
                    canvasGroupTexts.alpha = 1.0f;
                    canvasGroupTexts.interactable = true;
                    canvasGroupTexts.blocksRaycasts = true;
                }).SetLink(gameObject);
        }

        public void FadeIn()
        {
            _sequenceShowUp?.Kill();

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            canvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetLink(gameObject);
        }
        public void FadeOut()
        {
            _sequenceShowUp?.Kill();
            
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(0.0f, 1.0f).SetEase(Ease.InOutSine).SetLink(gameObject);
        }

        public void ChangeTexts()
        {
            imageButtonGameStart.ChangeText(LocaleKey.GameStart);
            imageButtonTutorial.ChangeText(LocaleKey.Tutorial);
            imageButtonInstruction.ChangeText(LocaleKey.Instruction);
            imageButtonSettings.ChangeText(LocaleKey.Settings);
            imageButtonRecords.ChangeText(LocaleKey.Records);
            imageButtonAchievements.ChangeText(LocaleKey.Achievements);
            imageButtonCredits.ChangeText(LocaleKey.Credits);
            imageButtonQuit.ChangeText(LocaleKey.Quit);
        }

        public void ResizeButtons()
        {
            imageButtonGameStart.Resize();
            imageButtonTutorial.Resize();
            imageButtonInstruction.Resize();
            imageButtonSettings.Resize();
            imageButtonRecords.Resize();
            imageButtonAchievements.Resize();
            imageButtonCredits.Resize();
            imageButtonQuit.Resize();
        }
        public void ZoomUpButtons(TitleScreenButtonName titleScreenButtonName)
        {
            imageButtonGameStart.ZoomUp(titleScreenButtonName == TitleScreenButtonName.GameStart);
            imageButtonTutorial.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Tutorial);
            imageButtonInstruction.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Instruction);
            imageButtonSettings.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Settings);
            imageButtonRecords.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Records);
            imageButtonAchievements.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Achievements);
            imageButtonCredits.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Credits);
            imageButtonQuit.ZoomUp(titleScreenButtonName == TitleScreenButtonName.Quit);
        }
    }
}
