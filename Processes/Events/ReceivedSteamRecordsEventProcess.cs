using System;
using Enums;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;

namespace Processes.Events
{
    public class ReceivedSteamRecordsEventProcess: IInitializable, IDisposable
    {
        private readonly SteamRecordsReceiver _steamRecordsReceiver;
        private readonly RecordsScreen _recordsScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public ReceivedSteamRecordsEventProcess(SteamRecordsReceiver steamRecordsReceiver, RecordsScreen recordsScreen)
        {
            _steamRecordsReceiver = steamRecordsReceiver;
            _recordsScreen = recordsScreen;
        }


        public void Initialize()
        {
            _steamRecordsReceiver.OnDownloadedLeaderboardRecords.Subscribe(records =>
            {
                _recordsScreen.RecordsBoardMessage.RenderClear();
                _recordsScreen.EnhancedScrollerRecords.SetRecords(records);
                _recordsScreen.EnhancedScrollerRecords.Render();
            }).AddTo(_compositeDisposable);
            
            _steamRecordsReceiver.OnFailed.Subscribe(_ =>
            {
                _recordsScreen.RecordsBoardMessage.RenderFailed();
                _recordsScreen.EnhancedScrollerRecords.Render();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}