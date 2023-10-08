using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Locale")]
    public class Locale : ScriptableObject
    {
        [Header("Header")] [SerializeField] private string headerSelectFrameSize;
        public string HeaderSelectFrameSize => headerSelectFrameSize;
        [SerializeField] private string headerInstruction;
        public string HeaderInstruction => headerInstruction;
        [SerializeField] private string headerSettings;
        public string HeaderSettings => headerSettings;
        [SerializeField] private string headerRecords;
        public string HeaderRecords => headerRecords;
        [SerializeField] private string headerAchievements;
        public string HeaderAchievements => headerAchievements;
        [SerializeField] private string headerStatistics;
        public string HeaderStatistics => headerStatistics;
        [SerializeField] private string headerCredits;
        public string HeaderCredits => headerCredits;
        [SerializeField] private string headerLicenses;
        public string HeaderLicenses => headerLicenses;
        
        [Header("Footer")] [SerializeField] private string footerMainGame;
        public string FooterMainGame => footerMainGame;
        [SerializeField] private string footerSelectFrameSizeSmall;
        public string FooterSelectFrameSizeSmall => footerSelectFrameSizeSmall;
        [SerializeField] private string footerSelectFrameSizeMedium;
        public string FooterSelectFrameSizeMedium => footerSelectFrameSizeMedium;
        [SerializeField] private string footerSelectFrameSizeLarge;
        public string FooterSelectFrameSizeLarge => footerSelectFrameSizeLarge;
        [SerializeField] private string footerReturnToTitle;
        public string FooterReturnToTitle => footerReturnToTitle;
        [SerializeField] private string footerReturnToResult;
        public string FooterReturnToResult => footerReturnToResult;

        [Header("TitleScreen")] [SerializeField]
        private string titleScreenGameStart;

        public string TitleScreenGameStart => titleScreenGameStart;
        [SerializeField] private string titleScreenTutorial;
        public string TitleScreenTutorial => titleScreenTutorial;
        [SerializeField] private string titleScreenInstruction;
        public string TitleScreenInstruction => titleScreenInstruction;
        [SerializeField] private string titleScreenSettings;
        public string TitleScreenSettings => titleScreenSettings;
        [SerializeField] private string titleScreenRecords;
        public string TitleScreenRecords => titleScreenRecords;
        [SerializeField] private string titleScreenAchievements;
        public string TitleScreenAchievements => titleScreenAchievements;
        [SerializeField] private string titleScreenStatistics;
        public string TitleScreenStatistics => titleScreenStatistics;
        [SerializeField] private string titleScreenQuit;
        public string TitleScreenQuit => titleScreenQuit;
        [SerializeField] private string titleScreenCredits;
        public string TitleScreenCredits => titleScreenCredits;
        [SerializeField] private string titleScreenLicences;
        public string TitleScreenLicences => titleScreenLicences;

        [Header("SelectFrameSizeScreen")] [SerializeField]
        private Texture2D selectFrameSizeScreenButtonSmall;
        public Texture2D SelectFrameSizeScreenButtonSmall => selectFrameSizeScreenButtonSmall;
        [SerializeField] private Texture2D selectFrameSizeScreenButtonMedium;
        public Texture2D SelectFrameSizeScreenButtonMedium => selectFrameSizeScreenButtonMedium;
        [SerializeField] private Texture2D selectFrameSizeScreenButtonLarge;
        public Texture2D SelectFrameSizeScreenButtonLarge => selectFrameSizeScreenButtonLarge;

        [Header("InstructionScreen")] [SerializeField]
        private string[] instructionScreenTabNames;

        public string[] InstructionScreenTabNames => instructionScreenTabNames;
        [SerializeField]private Sprite[] instructionScreenArticles;
        public Sprite[] InstructionScreenArticles => instructionScreenArticles;
        
        [Header("SettingsScreen")] [SerializeField]
        private string settingsScreenMusic;

        public string SettingsScreenMusic => settingsScreenMusic;
        [SerializeField] private string settingsScreenSound;
        public string SettingsScreenSound => settingsScreenSound;
        [SerializeField] private string settingsScreenResolution;
        public string SettingsScreenResolution => settingsScreenResolution;
        [SerializeField] private string settingsScreenQuality;
        public string SettingsScreenQuality => settingsScreenQuality;
        [SerializeField] private string settingsScreenButtonQualityLow;
        public string SettingsScreenButtonQualityLow => settingsScreenButtonQualityLow;
        [SerializeField] private string settingsScreenButtonQualityMedium;
        public string SettingsScreenButtonQualityMedium => settingsScreenButtonQualityMedium;
        [SerializeField] private string settingsScreenButtonQualityHigh;
        public string SettingsScreenButtonQualityHigh => settingsScreenButtonQualityHigh;
        [SerializeField] private string settingsScreenButtonQualityVeryHigh;
        public string SettingsScreenButtonQualityVeryHigh => settingsScreenButtonQualityVeryHigh;

        [Header("ResultScreen")] [SerializeField]
        private string resultScreenHeading;

        public string ResultScreenHeading => resultScreenHeading;
        [SerializeField] private string resultScreenScore;
        public string ResultScreenScore => resultScreenScore;
        [SerializeField] private string resultScreenLineCounts;
        public string ResultScreenLineCounts => resultScreenLineCounts;
        [SerializeField] private string resultScreenAchievements;

        public string ResultScreenAchievements => resultScreenAchievements;
        [SerializeField] private string resultScreenNoLineCount;
        public string ResultScreenNoLineCount => resultScreenNoLineCount;
        [SerializeField] private string resultScreenNoAchievement;
        public string ResultScreenNoAchievement => resultScreenNoAchievement;
        [SerializeField] private Texture2D resultScreenButtonRetry;
        public Texture2D ResultScreenButtonRetry => resultScreenButtonRetry;
        [SerializeField] private Texture2D resultScreenButtonTitle;
        public Texture2D ResultScreenButtonTitle => resultScreenButtonTitle;
        [SerializeField] private Texture2D resultScreenButtonRecords;
        public Texture2D ResultScreenButtonRecords => resultScreenButtonRecords;
        [SerializeField] private Texture2D resultScreenButtonGameQuit;
        public Texture2D ResultScreenButtonGameQuit => resultScreenButtonGameQuit;

        [Header("StatisticsScreen")] [SerializeField]
        private string statisticsScreenSmallFrameFinishedCount;

        public string StatisticsScreenSmallFrameFinishedCount => statisticsScreenSmallFrameFinishedCount;

        [SerializeField] private string statisticsScreenSmallFrameHighScore;
        public string StatisticsScreenSmallFrameHighScore => statisticsScreenSmallFrameHighScore;
        [SerializeField] private string statisticsScreenSmallFrameIlluminatedCount;
        public string StatisticsScreenSmallFrameIlluminatedCount => statisticsScreenSmallFrameIlluminatedCount;
        [SerializeField] private string statisticsScreenSmallFrameLongestPath;
        public string StatisticsScreenSmallFrameLongestPath => statisticsScreenSmallFrameLongestPath;
        [SerializeField] private string statisticsScreenMediumFrameFinishedCount;
        public string StatisticsScreenMediumFrameFinishedCount => statisticsScreenMediumFrameFinishedCount;
        [SerializeField] private string statisticsScreenMediumFrameHighScore;
        public string StatisticsScreenMediumFrameHighScore => statisticsScreenMediumFrameHighScore;
        [SerializeField] private string statisticsScreenMediumFrameIlluminatedCount;
        public string StatisticsScreenMediumFrameIlluminatedCount => statisticsScreenMediumFrameIlluminatedCount;
        [SerializeField] private string statisticsScreenMediumFrameLongestPath;
        public string StatisticsScreenMediumFrameLongestPath => statisticsScreenMediumFrameLongestPath;
        [SerializeField] private string statisticsScreenLargeFrameFinishedCount;
        public string StatisticsScreenLargeFrameFinishedCount => statisticsScreenLargeFrameFinishedCount;
        [SerializeField] private string statisticsScreenLargeFrameHighScore;
        public string StatisticsScreenLargeFrameHighScore => statisticsScreenLargeFrameHighScore;
        [SerializeField] private string statisticsScreenLargeFrameIlluminatedCount;
        public string StatisticsScreenLargeFrameIlluminatedCount => statisticsScreenLargeFrameIlluminatedCount;
        [SerializeField] private string statisticsScreenLargeFrameLongestPath;
        public string StatisticsScreenLargeFrameLongestPath => statisticsScreenLargeFrameLongestPath;

        [Header("ClosedBanner")] [SerializeField]
        private string closedBannerMain;

        public string ClosedBannerMain => closedBannerMain;
        [SerializeField] private string closedBannerSubL;
        public string ClosedBannerSubL => closedBannerSubL;
        [SerializeField] private string closedBannerSubR;
        public string ClosedBannerSubR => closedBannerSubR;

        [Header("ExterminatedBanner")] [SerializeField]
        private string exterminatedBannerMain;

        public string ExterminatedBannerMain => exterminatedBannerMain;

        [Header("FinishedBanner")] [SerializeField]
        private string finishedBannerMain;
        public string FinishedBannerMain => finishedBannerMain;
        
        [Header("ShortedBanner")] [SerializeField]
        private string shortedBannerMainShorted;

        public string ShortedBannerMainShorted => shortedBannerMainShorted;
        [SerializeField] private string shortedBannerSubLShorted;
        public string ShortedBannerSubLShorted => shortedBannerSubLShorted;
        [SerializeField] private string shortedBannerSubRShorted;
        public string ShortedBannerSubRShorted => shortedBannerSubRShorted;
        [SerializeField] private string shortedBannerMainFatal;
        public string ShortedBannerMainFatal => shortedBannerMainFatal;
        [SerializeField] private string shortedBannerSubLFatal;
        public string ShortedBannerSubLFatal => shortedBannerSubLFatal;
        [SerializeField] private string shortedBannerSubRFatal;
        public string ShortedBannerSubRFatal => shortedBannerSubRFatal;

        [Header("TutorialBanner")] [SerializeField]
        private string[] tutorialBannerIntroductions;

        public string[] TutorialBannerIntroductions => tutorialBannerIntroductions;
        [SerializeField] private string tutorialBannerWarningShorted;
        public string TutorialBannerWarningShorted => tutorialBannerWarningShorted;
        [SerializeField] private string tutorialBannerWarningClosed;
        public string TutorialBannerWarningClosed => tutorialBannerWarningClosed;
        [SerializeField] private string tutorialBannerBulbNotice;
        public string TutorialBannerBulbNotice => tutorialBannerBulbNotice;
        
        [Header("TextEffects")] [SerializeField]
        private string textEffectNoPlace;

        public string TextEffectNoPlace => textEffectNoPlace;

    }
}