using Interfaces;
using UniRx;
using Views.Instances.Tiles;

namespace Presenters.Instances.Tiles
{
    public class TileTerminalPresenter : TileBasePresenter
    {
        public TileTerminalPresenter(ITileTerminal tileTerminalModel, TileTerminal tileTerminal)
        {
            tileTerminalModel.OnChangedRotateStatus.Subscribe(rotateStatus =>
            {
                tileTerminal.SetRotateStatus(rotateStatus);
            }).AddTo(compositeDisposable);

            tileTerminalModel.OnChangedElectricStatusLine.Subscribe(electricStatus =>
            {
                tileTerminal.SetElectricStatusLine(electricStatus);
            }).AddTo(compositeDisposable);

            tileTerminalModel.OnChangedElectricStatusTerminalSymbol.Subscribe(electricStatus =>
            {
                tileTerminal.SetElectricStatusTerminalSymbol(electricStatus);
            }).AddTo(compositeDisposable);
        }
    }
}