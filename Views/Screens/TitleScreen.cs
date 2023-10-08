using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens
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
        public ImageButtonTitle ImageButtonGameStart => imageButtonGameStart;
        [SerializeField] private ImageButtonTitle imageButtonTutorial;
        public ImageButtonTitle ImageButtonTutorial => imageButtonTutorial;
        [SerializeField] private ImageButtonTitle imageButtonInstruction;
        public ImageButtonTitle ImageButtonInstruction => imageButtonInstruction;
        [SerializeField] private ImageButtonTitle imageButtonSettings;
        public ImageButtonTitle ImageButtonSettings => imageButtonSettings;
        [SerializeField] private ImageButtonTitle imageButtonRecords;
        public ImageButtonTitle ImageButtonRecords => imageButtonRecords;
        [SerializeField] private ImageButtonTitle imageButtonAchievements;
        public ImageButtonTitle ImageButtonAchievements => imageButtonAchievements;
        [SerializeField] private ImageButtonTitle imageButtonStatistics;
        public ImageButtonTitle ImageButtonStatistics => imageButtonStatistics;
        [SerializeField] private ImageButtonTitle imageButtonQuit;
        public ImageButtonTitle ImageButtonQuit => imageButtonQuit;
        [SerializeField] private ImageButtonTitle imageButtonCredits;
        public ImageButtonTitle ImageButtonCredits => imageButtonCredits;
        [SerializeField] private ImageButtonTitle imageButtonLicenses;
        public ImageButtonTitle ImageButtonLicenses => imageButtonLicenses;

        [SerializeField] private Localizer localizer;
        
        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

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
        }
        
        public void Localize()
        {
            imageButtonGameStart.ChangeText(localizer.CurrentLocale.TitleScreenGameStart);
            imageButtonTutorial.ChangeText(localizer.CurrentLocale.TitleScreenTutorial);
            imageButtonInstruction.ChangeText(localizer.CurrentLocale.TitleScreenInstruction);
            imageButtonSettings.ChangeText(localizer.CurrentLocale.TitleScreenSettings);
            imageButtonRecords.ChangeText(localizer.CurrentLocale.TitleScreenRecords);
            imageButtonAchievements.ChangeText(localizer.CurrentLocale.TitleScreenAchievements);
            imageButtonStatistics.ChangeText(localizer.CurrentLocale.TitleScreenStatistics);
            imageButtonQuit.ChangeText(localizer.CurrentLocale.TitleScreenQuit);
            imageButtonCredits.ChangeText(localizer.CurrentLocale.TitleScreenCredits);
            imageButtonLicenses.ChangeText(localizer.CurrentLocale.TitleScreenLicences);
            
            imageButtonGameStart.Resize();
            imageButtonTutorial.Resize();
            imageButtonInstruction.Resize();
            imageButtonSettings.Resize();
            imageButtonRecords.Resize();
            imageButtonAchievements.Resize();
            imageButtonStatistics.Resize();
            imageButtonQuit.Resize();
            imageButtonCredits.Resize();
            imageButtonLicenses.Resize();
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

        public void Show()
        {
            _sequenceShowUp?.Kill();
            _sequenceShowUp = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, durationFadeIn)
                    .SetEase(easeFadeIn))
                .OnStart(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                })
                .SetLink(gameObject);
        }

        public void Hide()
        {
            _sequenceShowUp?.Kill();
            _sequenceShowUp = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, durationFadeOut)
                    .SetEase(easeFadeOut))
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                })
                .SetLink(gameObject);
        }
    }
}
