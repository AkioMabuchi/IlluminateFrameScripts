using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPanelCellDetected: IInitializable
    {
        private readonly SelectBoardCellProcess _selectBoardCellProcess;
        private readonly PanelCellDetector _panelCellDetector;

        [Inject]
        public OnPanelCellDetected(SelectBoardCellProcess selectBoardCellProcess, PanelCellDetector panelCellDetector)
        {
            _selectBoardCellProcess = selectBoardCellProcess;
            _panelCellDetector = panelCellDetector;
        }
        
        public void Initialize()
        {
            _panelCellDetector.OnDetectPanelCell.Subscribe(cellPosition =>
            {
                _selectBoardCellProcess.SelectPanelCell(cellPosition);
            });

            _panelCellDetector.OnDetectNone.Subscribe(_ =>
            {
                _selectBoardCellProcess.DeselectPanelCell();
            });
        }
    }
}