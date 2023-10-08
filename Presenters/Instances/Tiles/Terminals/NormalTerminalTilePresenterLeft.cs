using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class NormalTerminalTilePresenterLeft
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public NormalTerminalTilePresenterLeft(NormalTerminalTileModelLeft normalTerminalTileModelLeft,
            NormalTerminalTileLeft normalTerminalTileLeft)
        {
            normalTerminalTileModelLeft.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                normalTerminalTileLeft.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            normalTerminalTileModelLeft.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                normalTerminalTileLeft.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}