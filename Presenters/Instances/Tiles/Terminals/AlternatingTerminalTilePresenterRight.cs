using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class AlternatingTerminalTilePresenterRight
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public AlternatingTerminalTilePresenterRight(
            AlternatingTerminalTileModelRight alternatingTerminalTileModelRight,
            AlternatingTerminalTileRight alternatingTerminalTileRight)
        {
            alternatingTerminalTileModelRight.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                alternatingTerminalTileRight.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            alternatingTerminalTileModelRight.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                alternatingTerminalTileRight.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}