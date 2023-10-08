using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StatisticsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameFinishedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameHighScoreLabel;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameIlluminatedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameLongestPathLabel;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameFinishedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameHighScoreLabel;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameIlluminatedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameLongestPathLabel;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameFinishedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameHighScoreLabel;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameIlluminatedCountLabel;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameLongestPathLabel;
        
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameFinishedCount;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameHighScore;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameIlluminatedCount;
        [SerializeField] private TextMeshProUGUI textMeshProSmallFrameLongestPath;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameFinishedCount;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameHighScore;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameIlluminatedCount;
        [SerializeField] private TextMeshProUGUI textMeshProMediumFrameLongestPath;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameFinishedCount;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameHighScore;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameIlluminatedCount;
        [SerializeField] private TextMeshProUGUI textMeshProLargeFrameLongestPath;
        
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
            textMeshProSmallFrameFinishedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenSmallFrameFinishedCount;
            textMeshProSmallFrameHighScoreLabel.text =
                localizer.CurrentLocale.StatisticsScreenSmallFrameHighScore;
            textMeshProSmallFrameIlluminatedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenSmallFrameIlluminatedCount;
            textMeshProSmallFrameLongestPathLabel.text =
                localizer.CurrentLocale.StatisticsScreenSmallFrameLongestPath;
            textMeshProMediumFrameFinishedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenMediumFrameFinishedCount;
            textMeshProMediumFrameHighScoreLabel.text =
                localizer.CurrentLocale.StatisticsScreenMediumFrameHighScore;
            textMeshProMediumFrameIlluminatedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenMediumFrameIlluminatedCount;
            textMeshProMediumFrameLongestPathLabel.text =
                localizer.CurrentLocale.StatisticsScreenMediumFrameLongestPath;
            textMeshProLargeFrameFinishedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenLargeFrameFinishedCount;
            textMeshProLargeFrameHighScoreLabel.text =
                localizer.CurrentLocale.StatisticsScreenLargeFrameHighScore;
            textMeshProLargeFrameIlluminatedCountLabel.text =
                localizer.CurrentLocale.StatisticsScreenLargeFrameIlluminatedCount;
            textMeshProLargeFrameLongestPathLabel.text =
                localizer.CurrentLocale.StatisticsScreenLargeFrameLongestPath;
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

        public void ChangeSmallFrameFinishedCount(int smallFrameFinishedCount)
        {
            textMeshProSmallFrameFinishedCount.text = smallFrameFinishedCount.ToString();
        }

        public void ChangeSmallFrameHighScore(int smallFrameHighScore)
        {
            if (smallFrameHighScore >= 0)
            {
                textMeshProSmallFrameHighScore.text = smallFrameHighScore.ToString();
            }
            else
            {
                textMeshProSmallFrameHighScore.text = "-";
            }
        }

        public void ChangeSmallFrameIlluminatedCount(int smallFrameIlluminatedCount)
        {
            textMeshProSmallFrameIlluminatedCount.text = smallFrameIlluminatedCount.ToString();
        }

        public void ChangeSmallFrameLongestPath(int smallFrameLongestPath)
        {
            textMeshProSmallFrameLongestPath.text = smallFrameLongestPath.ToString();
        }

        public void ChangeMediumFrameFinishedCount(int mediumFrameFinishedCount)
        {
            textMeshProMediumFrameFinishedCount.text = mediumFrameFinishedCount.ToString();
        }

        public void ChangeMediumFrameHighScore(int mediumFrameHighScore)
        {
            if (mediumFrameHighScore >= 0)
            {
                textMeshProMediumFrameHighScore.text = mediumFrameHighScore.ToString();
            }
            else
            {
                textMeshProMediumFrameHighScore.text = "-";
            }
        }

        public void ChangeMediumFrameIlluminatedCount(int mediumFrameIlluminatedCount)
        {
            textMeshProMediumFrameIlluminatedCount.text = mediumFrameIlluminatedCount.ToString();
        }

        public void ChangeMediumFrameLongestPath(int mediumFrameLongestPath)
        {
            textMeshProMediumFrameLongestPath.text = mediumFrameLongestPath.ToString();
        }

        public void ChangeLargeFrameFinishedCount(int largeFrameLongestCount)
        {
            textMeshProLargeFrameFinishedCount.text = largeFrameLongestCount.ToString();
        }

        public void ChangeLargeFrameHighScore(int largeFrameHighScore)
        {
            if (largeFrameHighScore >= 0)
            {
                textMeshProLargeFrameHighScore.text = largeFrameHighScore.ToString();
            }
            else
            {
                textMeshProLargeFrameHighScore.text = "-";
            }
        }

        public void ChangeLargeFrameIlluminatedCount(int largeFrameIlluminatedCount)
        {
            textMeshProLargeFrameIlluminatedCount.text = largeFrameIlluminatedCount.ToString();
        }

        public void ChangeLargeFrameLongestPath(int largeFrameLongestPath)
        {
            textMeshProLargeFrameLongestPath.text = largeFrameLongestPath.ToString();
        }
    }
}