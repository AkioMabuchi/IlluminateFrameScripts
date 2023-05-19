using Classes.Statics;
using DG.Tweening;
using Enums;
using TMPro;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BlackScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI textMeshProMessage;

        [SerializeField] private float durationFadeOut;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void ChangeMessage(BlackScreenMessageType messageType)
        {
            textMeshProMessage.text = Localize.LocaleString(messageType switch
            {
                BlackScreenMessageType.NoSteam => LocaleKey.BlackScreenNoSteam,
                BlackScreenMessageType.NoLicense => LocaleKey.BlackScreenNoLicense,
                _ => LocaleKey.None
            });
        }
    
        public void FadeOut()
        {
            canvasGroup.DOFade(0.0f, durationFadeOut)
                .SetEase(Ease.Linear)
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                })
                .SetLink(gameObject);
        }
    }
}
