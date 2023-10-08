using DG.Tweening;
using UnityEngine;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BackScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;
    
        private Sequence _sequenceFade;
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

        public void FadeIn()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, durationFadeIn)
                    .SetEase(easeFadeIn)
                ).SetLink(gameObject);
        }

        public void Hide()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, durationFadeOut)
                    .SetEase(easeFadeOut)
                ).SetLink(gameObject);
        }
    }
}
