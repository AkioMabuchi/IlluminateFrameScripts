using DG.Tweening;
using UnityEngine;
using Views.Instances;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SteamAchievementsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private EnhancedScrollerAchievements enhancedScrollerAchievements;
        public EnhancedScrollerAchievements EnhancedScrollerAchievements => enhancedScrollerAchievements;
        
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

        public void Localize()
        {
            
        }

        public void FadeIn()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, durationFadeIn)
                    .SetEase(easeFadeIn)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }).SetLink(gameObject);
        }

        public void Hide()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, durationFadeOut)
                    .SetEase(easeFadeOut)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }).SetLink(gameObject);
        }
    }
}
