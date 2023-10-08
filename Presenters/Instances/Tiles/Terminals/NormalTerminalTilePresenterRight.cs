using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class NormalTerminalTilePresenterRight
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public NormalTerminalTilePresenterRight(NormalTerminalTileModelRight normalTerminalTileModelRight,
            NormalTerminalTileRight normalTerminalTileRight)
        {
            normalTerminalTileModelRight.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                normalTerminalTileRight.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            normalTerminalTileModelRight.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                normalTerminalTileRight.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}