using System;
using Classes.Statics;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances
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

        [SerializeField] private Color fontColor;
        [SerializeField] private Color fontColorZoomUp;

        [SerializeField] private float durationZoomUp;
        [SerializeField] private float buttonPaddingWidth;

        public IObservable<Unit> OnPointerEnter =>
            observableEventTrigger.OnPointerEnterAsObservable().AsUnitObservable();

        public IObservable<Unit> OnPointerExit =>
            observableEventTrigger.OnPointerExitAsObservable().AsUnitObservable();

        private bool _isSelected;
        
        private Sequence _sequenceZoomUp;

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

        public void ChangeText(LocaleKey localeKey)
        {
            textMeshProImageButton.text = Localize.LocaleString(localeKey);
        }

        public void Resize()
        {
            var textWidth = textMeshProImageButton.preferredWidth;
            var sizeDelta = image.rectTransform.sizeDelta;
            sizeDelta.x = textWidth + buttonPaddingWidth;
            image.rectTransform.sizeDelta = sizeDelta;
        }

        public void ZoomUp(bool isSelected)
        {
            if (_isSelected == isSelected)
            {
                return;
            }

            _isSelected = isSelected;

            var size = isSelected ? fontSizeZoomUp : fontSize;
            var color = isSelected ? fontColorZoomUp : fontColor;

            _sequenceZoomUp?.Kill();
            _sequenceZoomUp = DOTween.Sequence()
                .Append(textMeshProImageButton.DOFontSize(size, durationZoomUp)
                    .SetEase(Ease.InOutCirc)
                    .OnUpdate(() =>
                    {
                        Resize();
                    }))
                .Join(textMeshProImageButton.DOColor(color, durationZoomUp)
                    .SetEase(Ease.InOutCirc))
                .SetLink(gameObject);

        }
    }
}
