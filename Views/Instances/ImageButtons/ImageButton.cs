using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances.ImageButtons
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;
        
        [SerializeField] private float scaleZoomUp;
        
        [SerializeField] private Color imageColor;
        [SerializeField] private Color imageColorZoomUp;
        
        [SerializeField] private float durationZoomUp;
        
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
                .Join(image.DOColor(color, durationZoomUp)
                    .SetEase(Ease.InOutCirc))
                .SetLink(gameObject);
        }
    }
}
