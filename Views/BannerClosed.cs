using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BannerClosed : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image[] imageLines;
        [SerializeField] private TextMeshProUGUI textMeshProMain;

        [SerializeField] private float durationFadeOut;
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
        }
        

        public void ShowUp()
        {
            DOTween.Sequence()
                .Append(imageBackground.DOFade(0.8f, 1.0f)
                    .From(0.0f)
                    .SetEase(Ease.InOutSine)
                ).Append(DOTween.To(() => _lineScaleX = 0.0f, value => { _lineScaleX = value; }, 1.0f, 1.0f)
                    .SetEase(Ease.InSine)
                    .OnUpdate(() =>
                    {
                        foreach (var imageLine in imageLines)
                        {
                            imageLine.transform.localScale = new Vector3(_lineScaleX, 1.0f, 1.0f);
                        }
                    })
                ).Append(textMeshProMain.DOFade(1.0f, 1.0f)
                    .From(0.0f)
                    .SetEase(Ease.InOutSine)
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
                }).SetLink(gameObject);
        }
        
        public void FadeOut()
        {
            canvasGroup.DOFade(0.0f, durationFadeOut)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject);
        }
    }
}
