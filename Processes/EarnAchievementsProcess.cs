using System.Collections.Generic;
using Classes;
using Classes.Statics;
using Enums;
using Models;
using Models.Instances.Frames;
using Steamworks;
using VContainer;
using Views;
using Views.Screens;

namespace Processes
{
    public class EarnAchievementsProcess
    {
        private readonly LineCountsModel _lineCountsModel;
        private readonly MainBoardModel _mainBoardModel;
        private readonly MainFrameModel _mainFrameModel;
        private readonly ScoreModel _scoreModel;
        
        private readonly SteamResultScreen _steamResultScreen;

        [Inject]
        public EarnAchievementsProcess(LineCountsModel lineCountsModel, MainBoardModel mainBoardModel,
            MainFrameModel mainFrameModel, ScoreModel scoreModel, SteamResultScreen steamResultScreen)
        {
            _lineCountsModel = lineCountsModel;
            _mainBoardModel = mainBoardModel;
            _mainFrameModel = mainFrameModel;
            _scoreModel = scoreModel;

            _steamResultScreen = steamResultScreen;
        }

        public void EarnAchievements()
        {
            var achievements = new HashSet<Achievement>();

            void EarnAchievement(Achievement achievement)
            {
                if (SteamUserStats.GetAchievement(Achievements.Dictionary[achievement], out var achieved))
                {
                    if (achieved)
                    {
                        return;
                    }

                    achievements.Add(achievement);
                }
            }

            // ------------------------------ ここから実績 ------------------------------

            var conductBoardResult = _mainBoardModel.Conduct();

            switch (_mainFrameModel.FrameModel)
            {
                case FrameSmallModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountSmallFrame, out int count))
                    {
                        if (count >= 1)
                        {
                            EarnAchievement(Achievement.FirstSmall);
                        }
                    }

                    if (SteamUserStats.GetStat(SteamStats.IlluminatedCountSmallFrame, out int illuminatedCount))
                    {
                        if (illuminatedCount >= 100)
                        {
                            EarnAchievement(Achievement.Illuminated100Smalls);
                        }
                    }

                    if (conductBoardResult.isCircuitClosed)
                    {
                        EarnAchievement(Achievement.ClosedSmall);
                    }
                    else if (_mainBoardModel.IsRadiant)
                    {
                        EarnAchievement(Achievement.RadiantSmall);
                    }

                    if (_mainFrameModel.FrameModel.IsIlluminated)
                    {
                        EarnAchievement(Achievement.IlluminatedSmall);
                    }

                    if (_scoreModel.Score >= 6000)
                    {
                        EarnAchievement(Achievement.GreatSmall);
                    }

                    if (_scoreModel.Score >= 9000)
                    {
                        EarnAchievement(Achievement.ExcellentSmall);
                    }

                    if (_scoreModel.Score == 0)
                    {
                        EarnAchievement(Achievement.GloomySmall);
                    }

                    if (_lineCountsModel.LongestLineCount >= 20)
                    {
                        EarnAchievement(Achievement.LongLineSmall);
                    }

                    if (_lineCountsModel.LongestLineCount >= 30)
                    {
                        EarnAchievement(Achievement.OdysseySmall);
                    }

                    break;
                }
                case FrameMediumModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountMediumFrame, out int count))
                    {
                        if (count >= 1)
                        {
                            EarnAchievement(Achievement.FirstMedium);
                        }
                    }

                    if (SteamUserStats.GetStat(SteamStats.IlluminatedCountMediumFrame, out int illuminatedCount))
                    {
                        if (illuminatedCount >= 100)
                        {
                            EarnAchievement(Achievement.Illuminated100Mediums);
                        }
                    }

                    if (conductBoardResult.isCircuitShorted)
                    {
                        EarnAchievement(Achievement.ShortedMedium);
                    }
                    else if (conductBoardResult.isCircuitClosed)
                    {
                        EarnAchievement(Achievement.ClosedMedium);
                    }
                    else if (_mainBoardModel.IsRadiant)
                    {
                        EarnAchievement(Achievement.RadiantMedium);
                    }

                    if (_mainFrameModel.FrameModel.IsIlluminated)
                    {
                        EarnAchievement(Achievement.IlluminatedMedium);
                    }

                    if (_scoreModel.Score >= 13000)
                    {
                        EarnAchievement(Achievement.GreatMedium);
                    }

                    if (_scoreModel.Score >= 19500)
                    {
                        EarnAchievement(Achievement.ExcellentMedium);
                    }

                    if (_scoreModel.Score == 0)
                    {
                        EarnAchievement(Achievement.GloomyMedium);
                    }

                    if (_lineCountsModel.LongestLineCount >= 40)
                    {
                        EarnAchievement(Achievement.LongLineMedium);
                    }

                    if (_lineCountsModel.LongestLineCount >= 60)
                    {
                        EarnAchievement(Achievement.OdysseyMedium);
                    }

                    break;
                }
                case FrameLargeModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountLargeFrame, out int count))
                    {
                        if (count >= 1)
                        {
                            EarnAchievement(Achievement.FirstLarge);
                        }
                    }

                    if (SteamUserStats.GetStat(SteamStats.IlluminatedCountLargeFrame, out int illuminatedCount))
                    {
                        if (illuminatedCount >= 100)
                        {
                            EarnAchievement(Achievement.Illuminated100Larges);
                        }
                    }

                    if (conductBoardResult.isCircuitShorted)
                    {
                        var hashSetShortedElectricStatus = new HashSet<ElectricStatus>();
                        foreach (var shortedCircuitPath in conductBoardResult.shortedCircuitPaths)
                        {
                            hashSetShortedElectricStatus.Add(shortedCircuitPath.electricStatus);
                        }

                        if (hashSetShortedElectricStatus.Count >= 3)
                        {
                            EarnAchievement(Achievement.FatalCircuit);
                        }

                        EarnAchievement(Achievement.ShortedLarge);
                    }
                    else if (conductBoardResult.isCircuitClosed)
                    {
                        EarnAchievement(Achievement.ClosedLarge);
                    }
                    else if (_mainBoardModel.IsRadiant)
                    {
                        EarnAchievement(Achievement.RadiantLarge);
                    }

                    if (_mainFrameModel.FrameModel.IsIlluminated)
                    {
                        EarnAchievement(Achievement.IlluminatedLarge);
                    }

                    if (_scoreModel.Score >= 28000)
                    {
                        EarnAchievement(Achievement.GreatLarge);
                    }

                    if (_scoreModel.Score >= 42000)
                    {
                        EarnAchievement(Achievement.ExcellentLarge);
                    }

                    if (_scoreModel.Score == 0)
                    {
                        EarnAchievement(Achievement.GloomyLarge);
                    }

                    if (_lineCountsModel.LongestLineCount >= 65)
                    {
                        EarnAchievement(Achievement.LongLineLarge);
                    }

                    if (_lineCountsModel.LongestLineCount >= 100)
                    {
                        EarnAchievement(Achievement.OdysseyLarge);
                    }

                    break;
                }
            }

            if (SteamUserStats.GetStat(SteamStats.FinishedCountSmallFrame, out int finishedSmallCount) &&
                SteamUserStats.GetStat(SteamStats.FinishedCountMediumFrame, out int finishedMediumCount) &&
                SteamUserStats.GetStat(SteamStats.FinishedCountLargeFrame, out int finishedLargeCount))
            {
                var finishedCountSum = finishedSmallCount + finishedMediumCount + finishedLargeCount;
                if (finishedCountSum >= 100)
                {
                    EarnAchievement(Achievement.Played100Times);
                }

                if (finishedCountSum >= 1000)
                {
                    EarnAchievement(Achievement.Played1000Times);
                }
            }

            // ------------------------------ ここまで　------------------------------

            foreach (var achievement in achievements)
            {
                SteamUserStats.SetAchievement(achievement.ToString());
            }

            SteamUserStats.StoreStats();

            _steamResultScreen.EnhancedScrollerResultAchievements.ClearAchievements();

            if (achievements.Count > 0)
            {
                var earnedAchievements = new List<CellViewAchievementParamsGroupAchievement>();
                foreach (var achievement in achievements)
                {
                    var achievementString = achievement.ToString();
                    earnedAchievements.Add(new CellViewAchievementParamsGroupAchievement
                    {
                        achieved = true,
                        achievement = achievement,
                        achievementName = SteamUserStats.GetAchievementDisplayAttribute(achievementString, "name"),
                        achievementDetail = SteamUserStats.GetAchievementDisplayAttribute(achievementString, "desc")
                    });
                }

                _steamResultScreen.EnhancedScrollerResultAchievements.SetAchievements(earnedAchievements);
            }
            else
            {
                _steamResultScreen.EnhancedScrollerResultAchievements.SetNoAchievement();
            }
            
            _steamResultScreen.EnhancedScrollerResultAchievements.Reload();
        }
    }
}