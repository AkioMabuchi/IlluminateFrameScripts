using Classes.Statics;
using DG.Tweening;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Footer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransformBackground;

        [SerializeField] private TextMeshProUGUI textMeshProFooting;
        
        [SerializeField] private float durationPull;

        private Tweener _tweenerPull;
        
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 1.0f;
            rectTransformBackground.anchoredPosition = new Vector2(0.0f, -rectTransformBackground.sizeDelta.y);
            textMeshProFooting.text = "";
        }

        public void ChangeFootingText(FooterFootingText footingText)
        {
            textMeshProFooting.text = LocalizationSettings.StringDatabase.GetLocalizedString(
                Const.LocalizationStrings, footingText switch
                {
                    FooterFootingText.ReturnToTitle => "FooterReturnToTitle",
                    FooterFootingText.SelectFrameSizeSmall => "FooterSelectFrameSizeSmall",
                    FooterFootingText.SelectFrameSizeMedium => "FooterSelectFrameSizeMedium",
                    FooterFootingText.SelectFrameSizeLarge => "FooterSelectFrameSizeLarge",
                    FooterFootingText.MainGame => "FooterMainGame",
                    _ => "None"
                });
        }
        public void PullDown()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(-rectTransformBackground.sizeDelta.y, durationPull)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
        }

        public void PullUp()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            
            _tweenerPull?.Kill();
            _tweenerPull = rectTransformBackground.DOAnchorPosY(0.0f, durationPull)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
        }
    }
}
