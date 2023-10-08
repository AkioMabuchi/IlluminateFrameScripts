using System;
using Classes.Statics;
using Cysharp.Threading.Tasks;
using Interfaces.Processes;
using Models;
using Models.Instances.Frames;
using Steamworks;
using VContainer;
using Views;
using Views.Screens.Prior;

namespace Processes
{
    public class SteamEndMainGameProcess : IEndMainGameProcess
    {
        private readonly MainFrameModel _mainFrameModel;
        private readonly ScoreModel _scoreModel;
        private readonly Footer _footer;
        private readonly SteamRecordSender _steamRecordSender;

        private readonly EarnAchievementsProcess _earnAchievementsProcess;
        private readonly ShowUpResultScreenProcess _showUpResultScreenProcess;

        [Inject]
        public SteamEndMainGameProcess(MainFrameModel mainFrameModel, ScoreModel scoreModel, Footer footer,
            SteamRecordSender steamRecordSender, EarnAchievementsProcess earnAchievementsProcess,
            ShowUpResultScreenProcess showUpResultScreenProcess)
        {
            _mainFrameModel = mainFrameModel;
            _scoreModel = scoreModel;
            _footer = footer;
            _steamRecordSender = steamRecordSender;

            _earnAchievementsProcess = earnAchievementsProcess;
            _showUpResultScreenProcess = showUpResultScreenProcess;
        }

        public async UniTask EndMainGame()
        {
            _footer.Hide();

            switch (_mainFrameModel.FrameModel)
            {
                case FrameSmallModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountSmallFrame, out int count))
                    {
                        SteamUserStats.SetStat(SteamStats.FinishedCountSmallFrame, count + 1);
                    }

                    SteamUserStats.SetStat(SteamStats.HighScoreSmallFrame, _scoreModel.Score);
                    break;
                }
                case FrameMediumModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountMediumFrame, out int count))
                    {
                        SteamUserStats.SetStat(SteamStats.FinishedCountMediumFrame, count + 1);
                    }

                    SteamUserStats.SetStat(SteamStats.HighScoreMediumFrame, _scoreModel.Score);
                    break;
                }
                case FrameLargeModel:
                {
                    if (SteamUserStats.GetStat(SteamStats.FinishedCountLargeFrame, out int count))
                    {
                        SteamUserStats.SetStat(SteamStats.FinishedCountLargeFrame, count + 1);
                    }

                    SteamUserStats.SetStat(SteamStats.HighScoreLargeFrame, _scoreModel.Score);
                    break;
                }
            }

            SteamUserStats.StoreStats();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            _steamRecordSender.SendScore();
            await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            _earnAchievementsProcess.EarnAchievements();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1.0));
            await _showUpResultScreenProcess.ShowUpResultScreen();
        }
    }
}