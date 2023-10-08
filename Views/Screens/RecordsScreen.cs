using System;
using DG.Tweening;
using Enums;
using Enums.ScreenButtonNames;
using Presenters.Instances;
using Steamworks;
using TMPro;
using UniRx;
using UnityEngine;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class RecordsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private ImageButtonRecords imageButtonSmall;
        public ImageButtonRecords ImageButtonSmall => imageButtonSmall;
        [SerializeField] private ImageButtonRecords imageButtonMedium;
        public ImageButtonRecords ImageButtonMedium => imageButtonMedium;
        [SerializeField] private ImageButtonRecords imageButtonLarge;
        public ImageButtonRecords ImageButtonLarge => imageButtonLarge;
        [SerializeField] private ImageButtonRecords imageButtonGlobal;
        public ImageButtonRecords ImageButtonGlobal => imageButtonGlobal;
        [SerializeField] private ImageButtonRecords imageButtonFriends;
        public ImageButtonRecords ImageButtonFriends => imageButtonFriends;

        [SerializeField] private TextMeshProUGUI textMeshProHeader;
        [SerializeField] private TextMeshProUGUI textMeshProRank;
        [SerializeField] private TextMeshProUGUI textMeshProPlayerName;
        [SerializeField] private TextMeshProUGUI textMeshProScore;

        [SerializeField] private CanvasGroup canvasGroupRecordsBoardContents;
        
        [SerializeField] private EnhancedScrollerRecords enhancedScrollerRecords;
        public EnhancedScrollerRecords EnhancedScrollerRecords => enhancedScrollerRecords;
        [SerializeField] private RecordsBoardMessage recordsBoardMessage;
        public RecordsBoardMessage RecordsBoardMessage => recordsBoardMessage;
        
        [SerializeField] private float durationFadeIn;
        [SerializeField] private float durationFadeOut;
        
        [SerializeField] private Ease easeFadeIn;
        [SerializeField] private Ease easeFadeOut;

        private FrameSize _frameSizeHeader;
        private ELeaderboardDataRequest _leaderboardDataRequestHeader;
        
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

        public void Show()
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
        

        public void SetHeaderTextFrameSize(FrameSize frameSize)
        {
            _frameSizeHeader = frameSize;
        }

        public void SetHeaderTextLeaderboardDataRequest(ELeaderboardDataRequest leaderboardDataRequest)
        {
            _leaderboardDataRequestHeader = leaderboardDataRequest;
        }

        public void RenderHeaderText()
        {
            textMeshProHeader.text = "DASHBOARD (" + _frameSizeHeader switch
            {
                FrameSize.Small => "SMALL",
                FrameSize.Medium => "MEDIUM",
                FrameSize.Large => "LARGE",
                _ => ""
            } + "," + _leaderboardDataRequestHeader switch
            {
                ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal => "GLOBAL",
                ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends => "FRIENDS",
                _ => ""
            } + ")";
        }
    }
}
