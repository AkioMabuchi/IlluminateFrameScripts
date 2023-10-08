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
    public class ImageButtonSelectFrameSize : MonoBehaviour
    {
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");
        
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;
        
        [SerializeField] private Vector3 scaleZoomUp;

        [SerializeField] private float durationZoomUp;
        [SerializeField] private Ease easeZoomUp;
        
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
            image.material.SetFloat(_emissionControl, 0.0f);
            image.rectTransform.localScale = Vector3.one;
        }

        public void ChangeTexture(Texture2D texture2D)
        {
            image.material.SetTexture(_baseMap, texture2D);
        }

        public void ZoomUp()
        {
            _sequenceZoom?.Kill();
            _sequenceZoom = DOTween.Sequence()
                .Append(image.rectTransform.DOScale(scaleZoomUp, durationZoomUp)
                    .SetEase(easeZoomUp))
                .Join(image.material.DOFloat(1.0f,_emissionControl, durationZoomUp)
                    .SetEase(easeZoomUp))
                .SetLink(gameObject);
        }

        public void ZoomDown()
        {
            _sequenceZoom?.Kill();
            _sequenceZoom = DOTween.Sequence()
                .Append(image.rectTransform.DOScale(Vector3.one, durationZoomUp)
                    .SetEase(easeZoomUp))
                .Join(image.material.DOFloat(0.0f, _emissionControl, durationZoomUp)
                    .SetEase(easeZoomUp))
                .SetLink(gameObject);
        }
    }
}
