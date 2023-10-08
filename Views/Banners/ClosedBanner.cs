using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Banners
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClosedBanner : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image[] imageLines;
        
        [SerializeField] private TextMeshProUGUI textMeshProMain;
        [SerializeField] private TextMeshProUGUI textMeshProSubL;
        [SerializeField] private TextMeshProUGUI textMeshProSubR;

        [SerializeField] private float alphaBackground;
        [SerializeField] private float durationShowUpBackground;
        [SerializeField] private Ease easeShowUpBackground;

        [SerializeField] private float durationShowUpLine;
        [SerializeField] private Ease easeShowUpLine;

        [SerializeField] private float durationShowUpTexts;
        [SerializeField] private Ease easeShowUpTexts;
        
        [SerializeField] private float durationFadeOut;
        [SerializeField] private Ease easeFadeOut;

        [SerializeField] private Localizer localizer;
        
        private float _lineScaleX;
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
                imageLine.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            }

            var colorTextMeshProMain = textMeshProMain.color;
            colorTextMeshProMain.a = 0.0f;
            textMeshProMain.color = colorTextMeshProMain;

            var colorTextMeshProSubL = textMeshProSubL.color;
            colorTextMeshProSubL.a = 0.0f;
            textMeshProSubL.color = colorTextMeshProSubL;

            var colorTextMeshProSubR = textMeshProSubR.color;
            colorTextMeshProSubR.a = 0.0f;
            textMeshProSubR.color = colorTextMeshProSubR;
        }

        public void Localize()
        {
            textMeshProMain.text = localizer.CurrentLocale.ClosedBannerMain;
            textMeshProSubL.text = localizer.CurrentLocale.ClosedBannerSubL;
            textMeshProSubR.text = localizer.CurrentLocale.ClosedBannerSubR;
        }

        public void ShowUp()
        {
            DOTween.Sequence()
                .Append(imageBackground.DOFade(alphaBackground, durationShowUpBackground)
                    .From(0.0f)
                    .SetEase(easeShowUpBackground)
                ).Append(DOTween
                    .To(() => _lineScaleX = 0.0f, value => { _lineScaleX = value; }, 1.0f, durationShowUpLine)
                    .SetEase(easeShowUpLine)
                    .OnUpdate(() =>
                    {
                        foreach (var imageLine in imageLines)
                        {
                            imageLine.transform.localScale = new Vector3(_lineScaleX, 1.0f, 1.0f);
                        }
                    })
                ).Append(textMeshProMain.DOFade(1.0f, durationShowUpTexts)
                    .From(0.0f)
                    .SetEase(easeShowUpTexts)
                ).Join(textMeshProSubL.DOFade(1.0f, durationShowUpTexts)
                    .From(0.0f)
                    .SetEase(easeShowUpTexts)
                ).Join(textMeshProSubR.DOFade(1.0f, durationShowUpTexts)
                    .From(0.0f)
                    .SetEase(easeShowUpTexts)
                ).OnStart(() =>
                {
                    canvasGroup.alpha = 1.0f;

                    var colorImageBackground = imageBackground.color;
                    colorImageBackground.a = 0.0f;
                    imageBackground.color = colorImageBackground;

                    foreach (var imageLine in imageLines)
                    {
                        imageLine.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
                    }

                    var colorTextMeshProMain = textMeshProMain.color;
                    colorTextMeshProMain.a = 0.0f;
                    textMeshProMain.color = colorTextMeshProMain;

                    var colorTextMeshProSubL = textMeshProSubL.color;
                    colorTextMeshProSubL.a = 0.0f;
                    textMeshProSubL.color = colorTextMeshProSubL;

                    var colorTextMeshProSubR = textMeshProSubR.color;
                    colorTextMeshProSubR.a = 0.0f;
                    textMeshProSubR.color = colorTextMeshProSubR;

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
