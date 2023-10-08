using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances.ImageButtons;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SteamInstructionScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Image imageForm;
        [SerializeField] private ImageButtonInstruction[] imageButtonsPage;
        [SerializeField] private Image imageArticle;

        [SerializeField] private Sprite[] spritesForm;
        [SerializeField] private Localizer localizer;

        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

        private readonly Subject<int> _subjectOnPointerEnterImageButtonPage = new();
        public IObservable<int> OnPointerEnterImageButtonPage => _subjectOnPointerEnterImageButtonPage;
        private readonly Subject<Unit> _subjectOnPointerExitImageButtonPage = new();
        public IObservable<Unit> OnPointerExitImageButtonPage => _subjectOnPointerExitImageButtonPage;

        private Sequence _sequenceFade;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            for (var i = 0; i < imageButtonsPage.Length; i++)
            {
                var ii = i;
                imageButtonsPage[i].OnPointerEnter.Subscribe(_ =>
                {
                    _subjectOnPointerEnterImageButtonPage.OnNext(ii);
                }).AddTo(gameObject);

                imageButtonsPage[i].OnPointerExit.Subscribe(_ =>
                {
                    _subjectOnPointerExitImageButtonPage.OnNext(Unit.Default);
                }).AddTo(gameObject);
            }
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        public void Localize()
        {
            for (var i = 0;
                 i < imageButtonsPage.Length && i < localizer.CurrentLocale.InstructionScreenTabNames.Length;
                 i++)
            {
                imageButtonsPage[i].ChangeText(localizer.CurrentLocale.InstructionScreenTabNames[i]);
            }
        }

        public void ChangePage(int page)
        {
            imageForm.sprite = spritesForm[page];
            imageArticle.sprite = localizer.CurrentLocale.InstructionScreenArticles[page];
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
