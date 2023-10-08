using System;
using Models;
using Structs;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views.Screens;

namespace Presenters.Screens
{
    public class SteamResultScreenPresenter: IInitializable, IDisposable
    {
        private readonly ScoreModel _scoreModel;
        private readonly LineCountsModel _lineCountsModel;
        private readonly SteamResultScreen _steamResultScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public SteamResultScreenPresenter(ScoreModel scoreModel, LineCountsModel lineCountsModel,
            SteamResultScreen steamResultScreen)
        {
            _scoreModel = scoreModel;
            _lineCountsModel = lineCountsModel;
            _steamResultScreen = steamResultScreen;
        }
        
        public void Initialize()
        {
            _scoreModel.OnChangedScore.Subscribe(score =>
            {
                _steamResultScreen.ShareScore(score);
            }).AddTo(_compositeDisposable);

            _lineCountsModel.OnAddedLineCount.Subscribe(addEvent =>
            {
                _steamResultScreen.EnqueueLineCountDifference(new LineCountDifferenceAdd
                {
                    lineCount = addEvent.Value.count
                });
            }).AddTo(_compositeDisposable);

            _lineCountsModel.OnResetLineCounts.Subscribe(_ =>
            {
                _steamResultScreen.EnqueueLineCountDifference(new LineCountDifferenceReset());
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}