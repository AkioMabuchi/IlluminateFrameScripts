using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class MinusTerminalTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public MinusTerminalTilePresenter(MinusTerminalTileModel minusTerminalTileModel,
            MinusTerminalTile minusTerminalTile)
        {
            minusTerminalTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                minusTerminalTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            minusTerminalTileModel.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                minusTerminalTile.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}