using DG.Tweening;
using UnityEngine;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LicensesScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        [SerializeField] private Localizer localizer;
        
        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

        private Sequence _sequence;

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

        public void Localize()
        {
            
        }
        
        public void Show()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, durationFadeIn)
                    .SetEase(easeFadeIn))
                .OnStart(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                })
                .SetLink(gameObject);
        }

        public void Hide()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, durationFadeOut)
                    .SetEase(easeFadeOut))
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                })
                .SetLink(gameObject);
        }
    }
}