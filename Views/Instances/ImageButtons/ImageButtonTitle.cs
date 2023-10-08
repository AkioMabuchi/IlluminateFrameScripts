using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances.ImageButtons
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButtonTitle : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        [SerializeField] private TextMeshProUGUI textMeshProImageButton;

        [SerializeField] private float fontSize;
        [SerializeField] private float fontSizeZoomUp;
        [SerializeField] private Ease fontSizeEase;
        
        [SerializeField] private Color fontColor;
        [SerializeField] private Color fontColorZoomUp;
        [SerializeField] private Ease fontColorEase;
        
        [SerializeField] private float durationZoomUp;
        [SerializeField] private float buttonPaddingWidth;
        
        public IObservable<Unit> OnPointerEnter =>
            observableEventTrigger.OnPointerEnterAsObservable().AsUnitObservable();

        public IObservable<Unit> OnPointerExit =>
            observableEventTrigger.OnPointerExitAsObservable().AsUnitObservable();

        private Sequence _sequenceZoom;

        private void Reset()
        {
            image = GetComponent<Image>();
            observableEventTrigger = GetComponent<ObservableEventTrigger>();
        }

        private void Start()
        {
            textMeshProImageButton.fontSize = fontSize;
            textMeshProImageButton.color = fontColor;
            
            var textWidth = textMeshProImageButton.preferredWidth;
            var sizeDelta = image.rectTransform.sizeDelta;
            sizeDelta.x = textWidth + buttonPaddingWidth;
            image.rectTransform.sizeDelta = sizeDelta;   
        }

        public void ChangeText(string text)
        {
            textMeshProImageButton.text = text;
        }

        public void Resize()
        {
            var textWidth = textMeshProImageButton.preferredWidth;
            var sizeDelta = image.rectTransform.sizeDelta;
            sizeDelta.x = textWidth + buttonPaddingWidth;
            image.rectTransform.sizeDelta = sizeDelta;
        }

        public void ZoomUp()
        {
            _sequenceZoom?.Kill();
            _sequenceZoom = DOTween.Sequence()
                .Append(textMeshProImageButton.DOFontSize(fontSizeZoomUp, durationZoomUp)
                    .SetEase(fontSizeEase)
                    .OnUpdate(() =>
                    {
                        var textWidth = textMeshProImageButton.preferredWidth;
                        var sizeDelta = image.rectTransform.sizeDelta;
                        sizeDelta.x = textWidth + buttonPaddingWidth;
                        image.rectTransform.sizeDelta = sizeDelta;
                    }))
                .Join(textMeshProImageButton.DOColor(fontColorZoomUp, durationZoomUp)
                    .SetEase(fontColorEase))
                .SetLink(gameObject);
        }

        public void ZoomDown()
        {
            _sequenceZoom?.Kill();
            _sequenceZoom = DOTween.Sequence()
                .Append(textMeshProImageButton.DOFontSize(fontSize, durationZoomUp)
                    .SetEase(fontSizeEase)
                    .OnUpdate(() =>
                    {
                        var textWidth = textMeshProImageButton.preferredWidth;
                        var sizeDelta = image.rectTransform.sizeDelta;
                        sizeDelta.x = textWidth + buttonPaddingWidth;
                        image.rectTransform.sizeDelta = sizeDelta;
                    }))
                .Join(textMeshProImageButton.DOColor(fontColor, durationZoomUp)
                    .SetEase(fontColorEase))
                .SetLink(gameObject);
        }
    }
}
