using System;
using DG.Tweening;
using TMPro;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances.ImageButtons
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButtonSettingsQuality : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;
        public ObservableEventTrigger ObservableEventTrigger => observableEventTrigger;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        [SerializeField] private Color colorCurrent;
        
        [SerializeField] private Color colorSelected;
        [SerializeField] private float durationColorSelected;
        [SerializeField] private Ease easeColorSelected;
        
        [SerializeField] private Color colorNone;
        [SerializeField] private float durationColorNone;
        [SerializeField] private Ease easeColorNone;
        
        private Sequence _sequence;
        private void Reset()
        {
            image = GetComponent<Image>();
            observableEventTrigger = GetComponent<ObservableEventTrigger>();
        }

        private void Start()
        {
            image.color = colorNone;
        }

        public void ChangeText(string text)
        {
            textMeshPro.text = text;
        }
        public void ChangeColorCurrent()
        {
            _sequence?.Kill();
            image.color = colorCurrent;
        }

        public void ChangeColorSelected()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(image.DOColor(colorSelected, durationColorSelected)
                    .SetEase(easeColorSelected))
                .SetLink(gameObject);
        }

        public void ChangeColorNone()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(image.DOColor(colorNone, durationColorNone)
                    .SetEase(easeColorNone))
                .SetLink(gameObject);
        }
    }
}