using System;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private ImageButtonResult imageButtonRetry;
        [SerializeField] private ImageButtonResult imageButtonTitle;
        [SerializeField] private ImageButtonResult imageButtonRecords;
        [SerializeField] private ImageButtonResult imageButtonQuit;

        public IObservable<Unit> OnPointerEnterRetry => imageButtonRetry.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterTitle => imageButtonTitle.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterRecords => imageButtonRecords.OnPointerEnter;
        public IObservable<Unit> OnPointerEnterQuit => imageButtonQuit.OnPointerEnter;

        public IObservable<Unit> OnPointerExitRetry => imageButtonRetry.OnPointerExit;
        public IObservable<Unit> OnPointerExitTitle => imageButtonTitle.OnPointerExit;
        public IObservable<Unit> OnPointerExitRecords => imageButtonRecords.OnPointerExit;
        public IObservable<Unit> OnPointerExitQuit => imageButtonQuit.OnPointerExit;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            ZoomUpButtons(ResultScreenButtonName.None);
        }

        public void ZoomUpButtons(ResultScreenButtonName resultScreenButtonName)
        {
            imageButtonRetry.ZoomUp(resultScreenButtonName == ResultScreenButtonName.Retry);
            imageButtonTitle.ZoomUp(resultScreenButtonName == ResultScreenButtonName.Title);
            imageButtonRecords.ZoomUp(resultScreenButtonName == ResultScreenButtonName.Records);
            imageButtonQuit.ZoomUp(resultScreenButtonName == ResultScreenButtonName.Quit);
        }
    }
}
