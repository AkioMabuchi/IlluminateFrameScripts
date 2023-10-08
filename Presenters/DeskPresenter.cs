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
        
        private readonly Desk _desk;

        public DeskPresenter(ScoreModel scoreModel, TileRestAmountModel tileRestAmountModel, Desk desk)
        {
            _scoreModel = scoreModel;
            _tileRestAmountModel = tileRestAmountModel;

            _desk = desk;
        }


        public void Initialize()
        {
            _scoreModel.OnChangedScore.Subscribe(score =>
            {
                _desk.ValueDisplayScore.SetDisplayNumber(score);
            });

            _tileRestAmountModel.OnChangedTileRestAmount.Subscribe(tileRestAmount =>
            {
                _desk.ValueDisplayTileRestAmount.SetDisplayNumber(tileRestAmount);
            });
        }
    }
}