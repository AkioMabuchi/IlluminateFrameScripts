using Models.Instances.Tiles.Terminals;
using UniRx;
using Views.Instances.Tiles.Terminals;

namespace Presenters.Instances.Tiles.Terminals
{
    public class AlternatingTerminalTilePresenterLeft
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public AlternatingTerminalTilePresenterLeft(AlternatingTerminalTileModelLeft alternatingTerminalTileModelLeft,
            AlternatingTerminalTileLeft alternatingTerminalTileLeft)
        {
            alternatingTerminalTileModelLeft.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                alternatingTerminalTileLeft.SetElectricStatusLine(electricStatus);
            }).AddTo(_compositeDisposable);

            alternatingTerminalTileModelLeft.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                alternatingTerminalTileLeft.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(_compositeDisposable);
        }

        public void CompositeDispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}