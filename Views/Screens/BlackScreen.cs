using DG.Tweening;
using Enums;
using TMPro;
using UnityEngine;

namespace Views.Screens
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
            textMeshProMessage.text = messageType switch
            {
                BlackScreenMessageType.NoSteam =>
                    "PLEASE START THIS APPLICATION FROM STEAM\n" +
                    "<size=60>Click mouse to quit this application</size>",
                BlackScreenMessageType.NoLicense =>
                    "NO LICENSE TO PLAY THIS APPLICATION\n" +
                    "<size=60>Click mouse to quit this application</size>",
                _ => ""
            };
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
