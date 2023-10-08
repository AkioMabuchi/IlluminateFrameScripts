using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class PlusTerminalTilePresenter
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public PlusTerminalTilePresenter(PlusTerminalTileModel plusTerminalTileModel, PlusTerminalTile plusTerminalTile)
        {
            plusTerminalTileModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                plusTerminalTile.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            plusTerminalTileModel.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                plusTerminalTile.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}