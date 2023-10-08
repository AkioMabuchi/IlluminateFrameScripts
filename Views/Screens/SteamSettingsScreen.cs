using System;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SteamSettingsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI textMeshProLabelMusic;
        [SerializeField] private TextMeshProUGUI textMeshProLabelSound;
        [SerializeField] private TextMeshProUGUI textMeshProLabelResolution;
        [SerializeField] private TextMeshProUGUI textMeshProLabelQuality;

        [SerializeField] private Slider sliderMusic;
        public Slider SliderMusic => sliderMusic;
        [SerializeField] private Slider sliderSound;
        public Slider SliderSound => sliderSound;

        [SerializeField] private ObservableEventTrigger observableEventTriggerSliderMusic;
        public ObservableEventTrigger ObservableEventTriggerSliderMusic => observableEventTriggerSliderMusic;
        [SerializeField] private ObservableEventTrigger observableEventTriggerSliderSound;
        public ObservableEventTrigger ObservableEventTriggerSliderSound => observableEventTriggerSliderSound;

        [SerializeField] private ImageButtonSettingsResolution imageButtonResolution960X540;
        public ImageButtonSettingsResolution ImageButtonResolution960X540 => imageButtonResolution960X540;
        [SerializeField] private ImageButtonSettingsResolution imageButtonResolution1280X720;
        public ImageButtonSettingsResolution ImageButtonResolution1280X720 => imageButtonResolution1280X720;
        [SerializeField] private ImageButtonSettingsResolution imageButtonResolution1920X1080;
        public ImageButtonSettingsResolution ImageButtonResolution1920X1080 => imageButtonResolution1920X1080;
        [SerializeField] private ImageButtonSettingsResolution imageButtonResolution2560X1440;
        public ImageButtonSettingsResolution ImageButtonResolution2560X1440 => imageButtonResolution2560X1440;
        [SerializeField] private ImageButtonSettingsResolution imageButtonResolution3840X2160;
        public ImageButtonSettingsResolution ImageButtonResolution3840X2160 => imageButtonResolution3840X2160;
        [SerializeField] private ImageButtonSettingsQuality imageButtonQualityLow;
        public ImageButtonSettingsQuality ImageButtonQualityLow => imageButtonQualityLow;
        [SerializeField] private ImageButtonSettingsQuality imageButtonQualityMedium;
        public ImageButtonSettingsQuality ImageButtonQualityMedium => imageButtonQualityMedium;
        [SerializeField] private ImageButtonSettingsQuality imageButtonQualityHigh;
        public ImageButtonSettingsQuality ImageButtonQualityHigh => imageButtonQualityHigh;
        [SerializeField] private ImageButtonSettingsQuality imageButtonQualityVeryHigh;
        public ImageButtonSettingsQuality ImageButtonQualityVeryHigh => imageButtonQualityVeryHigh;

        [SerializeField] private Localizer localizer;
        
        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;
        
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

        public void Localize()
        {
            textMeshProLabelMusic.text = localizer.CurrentLocale.SettingsScreenMusic;
            textMeshProLabelSound.text = localizer.CurrentLocale.SettingsScreenSound;
            textMeshProLabelResolution.text = localizer.CurrentLocale.SettingsScreenResolution;
            textMeshProLabelQuality.text = localizer.CurrentLocale.SettingsScreenQuality;

            imageButtonQualityLow.ChangeText(localizer.CurrentLocale.SettingsScreenButtonQualityLow);
            imageButtonQualityMedium.ChangeText(localizer.CurrentLocale.SettingsScreenButtonQualityMedium);
            imageButtonQualityHigh.ChangeText(localizer.CurrentLocale.SettingsScreenButtonQualityHigh);
            imageButtonQualityVeryHigh.ChangeText(localizer.CurrentLocale.SettingsScreenButtonQualityVeryHigh);
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

        public void SelectResolutionButtons(ResolutionSize resolutionSize)
        {
            imageButtonResolution960X540.ChangeColorNone();
            imageButtonResolution1280X720.ChangeColorNone();
            imageButtonResolution1920X1080.ChangeColorNone();
            imageButtonResolution2560X1440.ChangeColorNone();
            imageButtonResolution3840X2160.ChangeColorNone();

            switch (resolutionSize)
            {
                case ResolutionSize.Size960X540:
                {
                    imageButtonResolution960X540.ChangeColorCurrent();
                    break;
                }
                case ResolutionSize.Size1280X720:
                {
                    imageButtonResolution1280X720.ChangeColorCurrent();
                    break;
                }
                case ResolutionSize.Size1920X1080:
                {
                    imageButtonResolution1920X1080.ChangeColorCurrent();
                    break;
                }
                case ResolutionSize.Size2560X1440:
                {
                    imageButtonResolution2560X1440.ChangeColorCurrent();
                    break;
                }
                case ResolutionSize.Size3840X2160:
                {
                    imageButtonResolution3840X2160.ChangeColorCurrent();
                    break;
                }
            }
        }

        public void SelectRenderQualityButtons(RenderQuality renderQuality)
        {
            imageButtonQualityLow.ChangeColorNone();
            imageButtonQualityMedium.ChangeColorNone();
            imageButtonQualityHigh.ChangeColorNone();
            ImageButtonQualityVeryHigh.ChangeColorNone();

            switch (renderQuality)
            {
                case RenderQuality.Low:
                {
                    imageButtonQualityLow.ChangeColorCurrent();
                    break;
                }
                case RenderQuality.Medium:
                {
                    imageButtonQualityMedium.ChangeColorCurrent();
                    break;
                }
                case RenderQuality.High:
                {
                    imageButtonQualityHigh.ChangeColorCurrent();
                    break;
                }
                case RenderQuality.VeryHigh:
                {
                    imageButtonQualityVeryHigh.ChangeColorCurrent();
                    break;
                }
            }
        }
    }
}
