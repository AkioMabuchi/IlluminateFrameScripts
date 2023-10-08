using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Banners
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TutorialBanner : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageBackground;
        [SerializeField] private TextMeshProUGUI textMeshProMessage;

        [SerializeField] private Localizer localizer;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1.0f;
        }

        public void Render()
        {
            
        }

        public void ChangeMessageNone()
        {
            textMeshProMessage.text = "";
        }

        public void ChangeMessageIntroduction(int index)
        {
            textMeshProMessage.text = localizer.CurrentLocale.TutorialBannerIntroductions[index];
        }

        public void ChangeMessageWarningShorted()
        {
            textMeshProMessage.text = localizer.CurrentLocale.TutorialBannerWarningShorted;
        }

        public void ChangeMessageWarningClosed()
        {
            textMeshProMessage.text = localizer.CurrentLocale.TutorialBannerWarningClosed;
        }

        public void ChangeMessageBulbNotice()
        {
            textMeshProMessage.text = localizer.CurrentLocale.TutorialBannerBulbNotice;
        }
    }
}