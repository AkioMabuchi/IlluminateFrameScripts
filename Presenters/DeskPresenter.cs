using Models;
using UniRx;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class DeskPresenter : IInitializable
    {
        private readonly ScoreModel _scoreModel;
        private readonly TileRestAmountModel _tileRestAmountModel;
        private readonly DeskFactory _deskFactory;

        public DeskPresenter(ScoreModel scoreModel, TileRestAmountModel tileRestAmountModel, DeskFactory deskFactory)
        {
            _scoreModel = scoreModel;
            _tileRestAmountModel = tileRestAmountModel;
            _deskFactory = deskFactory;
        }


        public void Initialize()
        {
            _scoreModel.OnChangedScore.Subscribe(score =>
            {
                _deskFactory.SetScoreDisplayNumber(score);
            });

            _tileRestAmountModel.OnChangedTileRestAmount.Subscribe(tileRestAmount =>
            {
                _deskFactory.SetTileRestAmountDisplayNumber(tileRestAmount);
            });
        }
    }
}