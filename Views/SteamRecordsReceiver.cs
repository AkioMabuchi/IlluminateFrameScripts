using System;
using System.Collections.Generic;
using Classes.Statics;
using Enums;
using Steamworks;
using Structs;
using UniRx;
using UnityEngine;

namespace Views
{
    public class SteamRecordsReceiver : MonoBehaviour
    {
        private readonly Subject<Unit> _subjectOnFailed = new();
        public IObservable<Unit> OnFailed => _subjectOnFailed;
        private readonly Subject<IEnumerable<CellViewRecordParamsGroup>> _subjectOnDownloadedLeaderboardRecords = new();

        public IObservable<IEnumerable<CellViewRecordParamsGroup>> OnDownloadedLeaderboardRecords =>
            _subjectOnDownloadedLeaderboardRecords;

        private FrameSize _frameSize = FrameSize.Small;

        private ELeaderboardDataRequest _leaderboardDataRequest =
            ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;

        public void SetFrameSize(FrameSize frameSize)
        {
            _frameSize = frameSize;
        }

        public void SetLeaderBoardDataRequest(ELeaderboardDataRequest leaderboardDataRequest)
        {
            _leaderboardDataRequest = leaderboardDataRequest;
        }

        public void ReceiveRecords()
        {
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
                        _subjectOnFailed.OnNext(Unit.Default);
                        return;
                    }

                    var leaderboard = findResult.m_hSteamLeaderboard;
                    CallResult<LeaderboardScoresDownloaded_t>.Create().Set(
                        SteamUserStats.DownloadLeaderboardEntries(leaderboard,
                            _leaderboardDataRequest, 0, int.MaxValue),
                        (downloadResult, downloadFailure) =>
                        {
                            if (downloadFailure)
                            {
                                _subjectOnFailed.OnNext(Unit.Default);
                                return;
                            }

                            var myId = SteamUser.GetSteamID().m_SteamID;
                            var list = new List<CellViewRecordParamsGroup>();
                            for (var i = 0; i < downloadResult.m_cEntryCount; i++)
                            {

                                if (SteamUserStats.GetDownloadedLeaderboardEntry(
                                        downloadResult.m_hSteamLeaderboardEntries, i,
                                        out var leaderboardEntry, Array.Empty<int>(), 0))
                                {
                                    var id = leaderboardEntry.m_steamIDUser.m_SteamID;
                                    var rank = leaderboardEntry.m_nGlobalRank;
                                    var playerName = SteamFriends.GetFriendPersonaName(leaderboardEntry.m_steamIDUser);
                                    var score = leaderboardEntry.m_nScore;

                                    var status = id == myId ? CellViewRecordStatus.Mine : CellViewRecordStatus.Normal;

                                    list.Add(new CellViewRecordParamsGroup
                                    {
                                        status = status,
                                        rank = rank,
                                        playerName = playerName,
                                        score = score
                                    });
                                }
                            }


                            _subjectOnDownloadedLeaderboardRecords.OnNext(list);
                        });
                });
        }
    }
}
