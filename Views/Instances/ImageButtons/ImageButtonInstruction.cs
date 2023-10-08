using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances.ImageButtons
{
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class ImageButtonInstruction : MonoBehaviour
    {
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        [SerializeField] private TextMeshProUGUI textMeshPro;
        
        public IObservable<Unit> OnPointerEnter =>
            observableEventTrigger.OnPointerEnterAsObservable().AsUnitObservable();

        public IObservable<Unit> OnPointerExit =>
            observableEventTrigger.OnPointerExitAsObservable().AsUnitObservable();

        private Color _currentColor;
        private Sequence _sequenceZoomUp;

        private void Reset()
        {
            observableEventTrigger = GetComponent<ObservableEventTrigger>();
        }
        public void ChangeText(string text)
        {
            textMeshPro.text = text;
        }
    }
}