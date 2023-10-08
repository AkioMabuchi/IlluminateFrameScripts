using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Banners
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FinishedBanner : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image[] imageLines;
        [SerializeField] private TextMeshProUGUI textMeshProMain;

        [SerializeField] private float alphaBackground;
        [SerializeField] private float durationFadeInBackground;
        [SerializeField] private Ease easeFadeInBackground;

        [SerializeField] private float durationFadeInLines;
        [SerializeField] private Ease easeFadeInLines;

        [SerializeField] private float durationFadeInText;
        [SerializeField] private Ease easeFadeInText;

        [SerializeField] private float durationFadeOut;
        [SerializeField] private Ease easeFadeOut;

        [SerializeField] private Localizer localizer;
        
        private float _currentAlphaLines;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            
            var colorImageBackground = imageBackground.color;
            colorImageBackground.a = 0.0f;
            imageBackground.color = colorImageBackground;
            
            foreach (var imageLine in imageLines)
            {
                var colorImageLine = imageLine.color;
                colorImageLine.a = 0.0f;
                imageLine.color = colorImageLine;
            }
            
            var colorTextMeshProMain = textMeshProMain.color;
            colorTextMeshProMain.a = 0.0f;
            textMeshProMain.color = colorTextMeshProMain;
        }

        public void Localize()
        {
            textMeshProMain.text = localizer.CurrentLocale.FinishedBannerMain;
        }
        public void ShowUp()
        {
            DOTween.Sequence()
                .Append(imageBackground.DOFade(alphaBackground, durationFadeInBackground)
                    .From(0.0f)
                    .SetEase(easeFadeInBackground)
                ).Append(DOTween.To(() => _currentAlphaLines = 0.0f, value =>
                    {
                        _currentAlphaLines = value;
                    }, 1.0f, durationFadeInLines)
                    .SetEase(easeFadeInLines)
                    .OnUpdate(() =>
                    {
                        foreach (var imageLine in imageLines)
                        {
                            var color = imageLine.color;
                            color.a = _currentAlphaLines;
                            imageLine.color = color;
                        }
                    })
                ).Append(textMeshProMain.DOFade(1.0f, durationFadeInText)
                    .From(0.0f)
                    .SetEase(easeFadeInText)
                ).OnStart(() =>
                {
                    canvasGroup.alpha = 1.0f;
            
                    var colorImageBackground = imageBackground.color;
                    colorImageBackground.a = 0.0f;
                    imageBackground.color = colorImageBackground;
            
                    foreach (var imageLine in imageLines)
                    {
                        var colorImageLine = imageLine.color;
                        colorImageLine.a = 0.0f;
                        imageLine.color = colorImageLine;
                    }
            
                    var colorTextMeshProMain = textMeshProMain.color;
                    colorTextMeshProMain.a = 0.0f;
                    textMeshProMain.color = colorTextMeshProMain;
                }).SetLink(gameObject);
        }

        public void FadeOut()
        {
            canvasGroup.DOFade(0.0f, durationFadeOut)
                .SetEase(easeFadeOut)
                .SetLink(gameObject);
        }
    }
}