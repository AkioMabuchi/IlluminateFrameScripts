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
    public class ImageButtonResult : MonoBehaviour
    {
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");
        private static readonly int _emissionMap = Shader.PropertyToID("_EmissionMap");
        
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        [SerializeField] private float scaleZoomUp;
        
        [SerializeField] private float durationZoomUp;
        [SerializeField] private Ease easeZoomUp;

        [SerializeField] private float durationZoomDown;
        [SerializeField] private Ease easeZoomDown;
        
        [SerializeField] private float durationEmissionControl;
        [SerializeField] private Ease easeEmissionControl;
        
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
            image.material.SetFloat(_emissionControl, 0.0f);
        }

        public void Localize(Texture2D texture2D)
        {
            image.material.SetTexture(_baseMap, texture2D);
            image.material.SetTexture(_emissionMap, texture2D);
        }
        
        public void ZoomUp()
        {
            _sequenceZoomUp?.Kill();
            _sequenceZoomUp = DOTween.Sequence()
                .Append(image.rectTransform.DOScale(scaleZoomUp, durationZoomUp)
                    .SetEase(easeZoomUp))
                .Join(image.material.DOFloat(1.0f, _emissionControl, durationEmissionControl)
                    .SetEase(easeEmissionControl))
                .SetLink(gameObject);
        }

        public void ZoomDown()
        {
            _sequenceZoomUp?.Kill();
            _sequenceZoomUp = DOTween.Sequence()
                .Append(image.rectTransform.DOScale(Vector3.one, durationZoomDown)
                    .SetEase(easeZoomDown))
                .Join(image.material.DOFloat(0.0f, _emissionControl, durationEmissionControl)
                    .SetEase(easeEmissionControl))
                .SetLink(gameObject);
        }
    }
}
