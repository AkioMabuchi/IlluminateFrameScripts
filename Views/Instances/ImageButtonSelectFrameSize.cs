using System;
using Classes.Statics;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Views.Instances
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButtonSelectFrameSize : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        [SerializeField] private TextMeshProUGUI textMeshProMain;
        [SerializeField] private TextMeshProUGUI textMeshProDetails;
        
        [SerializeField] private float scaleZoomUp;
        
        [SerializeField] private Color imageColor;
        [SerializeField] private Color imageColorZoomUp;
        
        [SerializeField] private float durationZoomUp;
        
        public IObservable<Unit> OnPointerEnter =>
            observableEventTrigger.OnPointerEnterAsObservable().AsUnitObservable();

        public IObservable<Unit> OnPointerExit =>
            observableEventTrigger.OnPointerExitAsObservable().AsUnitObservable();
        
        private bool _isSelected;

        private Color _currentColor;
        private Sequence _sequenceZoomUp;
        
        private void Reset()
        {
            image = GetComponent<Image>();
            observableEventTrigger = GetComponent<ObservableEventTrigger>();
        }

        private void Start()
        {
            _currentColor = imageColor;
            image.color =  imageColor;
            image.rectTransform.localScale = Vector3.one;
            textMeshProMain.color =  imageColor;
            textMeshProDetails.color =  imageColor;
        }

        public void ChangeTexts(string stringLocale, string stringLocaleDetails)
        {
            textMeshProMain.text =
                LocalizationSettings.StringDatabase.GetLocalizedString(Const.LocalizationStrings, stringLocale);
            textMeshProDetails.text =
                LocalizationSettings.StringDatabase.GetLocalizedString(Const.LocalizationStrings, stringLocaleDetails);
        }

        public void ZoomUp(bool isSelected)
        {
            if (_isSelected == isSelected)
            {
                return;
            }

            _isSelected = isSelected;

            var scale = isSelected ? new Vector3(scaleZoomUp, scaleZoomUp, 1.0f) : Vector3.one;
            var color = isSelected ? imageColorZoomUp : imageColor;
            
            _sequenceZoomUp?.Kill();
            _sequenceZoomUp = DOTween.Sequence()
                .Append(image.rectTransform.DOScale(scale, durationZoomUp)
                    .SetEase(Ease.InOutCirc))
                .Join(DOTween.To(() => _currentColor, value => { _currentColor = value; }, color, durationZoomUp)
                    .OnUpdate(() =>
                    {
                        image.color = _currentColor;
                        textMeshProMain.color = _currentColor;
                        textMeshProDetails.color = _currentColor;
                    })
                    .OnComplete(() =>
                    {
                        _currentColor = color;
                    })
                    .SetEase(Ease.InOutCirc))
                .SetLink(gameObject);
        }
    }
}
