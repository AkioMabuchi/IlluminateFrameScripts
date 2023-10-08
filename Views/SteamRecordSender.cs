using System;
using Classes.Statics;
using Enums;
using Steamworks;
using UnityEngine;

namespace Views
{
    public class SteamRecordSender : MonoBehaviour
    {
        private FrameSize _frameSize = FrameSize.Small;

        public void SetFrameSize(FrameSize frameSize)
        {
            _frameSize = frameSize;
        }
        
        public void SendScore()
        {
            var score = -1;
            
            switch (_frameSize)
            {
                case FrameSize.Small:
                {
                    if (SteamUserStats.GetStat(SteamStats.HighScoreSmallFrame, out int scoreSmall))
                    {
                        score = scoreSmall;
                    }

                    break;
                }
                case FrameSize.Medium:
                {
                    if (SteamUserStats.GetStat(SteamStats.HighScoreMediumFrame, out int scoreMedium))
                    {
                        score = scoreMedium;
                    }

                    break;
                }
                case FrameSize.Large:
                {
                    if (SteamUserStats.GetStat(SteamStats.HighScoreLargeFrame, out int scoreLarge))
                    {
                        score = scoreLarge;
                    }

                    break;
                }
            }

            if (score < 0)
            {
                return;
            }
            
            CallResult<LeaderboardFindResult_t>.Create().Set(SteamUserStats.FindLeaderboard(_frameSize switch
                {
                    FrameSize.Small => SteamRankings.SmallFrame,
                    FrameSize.Medium => SteamRankings.MediumFrame,
                    FrameSize.Large => SteamRankings.LargeFrame,
                    _ => ""
                }),
                (findResult, findFailure) =>
                {
                    if (findFailure || findResult.m_bLeaderboardFound == 0)
                    {
                        return;
                    }

                    CallResult<LeaderboardScoreUploaded_t>.Create().Set(
                        SteamUserStats.UploadLeaderboardScore(findResult.m_hSteamLeaderboard,
                            ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score,
                            Array.Empty<int>(), 0), (uploadResult, uploadFailure) =>
                        {
                            Debug.Log("SCORE UPLOADED");
                        });
                });
        }
    }
}
