using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButtonSettingsResolution : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        [SerializeField] private TextMeshProUGUI textMeshPro;

        [SerializeField] private Color colorCurrent;
        [SerializeField] private Color colorSelected;
        [SerializeField] private Color colorNone;
        public IObservable<Unit> OnPointerEnter =>
            observableEventTrigger.OnPointerEnterAsObservable().AsUnitObservable();

        public IObservable<Unit> OnPointerExit =>
            observableEventTrigger.OnPointerExitAsObservable().AsUnitObservable();
        private void Reset()
        {
            image = GetComponent<Image>();
            observableEventTrigger = GetComponent<ObservableEventTrigger>();
        }

        public void ChangeColorCurrent()
        {
            image.color = colorCurrent;
        }

        public void ChangeColorSelected()
        {
            image.color = colorSelected;
        }

        public void ChangeColorNone()
        {
            image.color = colorNone;
        }
    }
}
