using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnBoardCellDetected: IInitializable
    {
        private readonly SelectBoardCellProcess _selectBoardCellProcess;
        private readonly BoardCellDetector _boardCellDetector;

        [Inject]
        public OnBoardCellDetected(SelectBoardCellProcess selectBoardCellProcess, BoardCellDetector boardCellDetector)
        {
            _selectBoardCellProcess = selectBoardCellProcess;
            _boardCellDetector = boardCellDetector;
        }
        
        public void Initialize()
        {
            _boardCellDetector.OnDetectPanelCell.Subscribe(cellPosition =>
            {
                _selectBoardCellProcess.SelectPanelCell(cellPosition);
            });

            _boardCellDetector.OnDetectNone.Subscribe(_ =>
            {
                _selectBoardCellProcess.DeselectPanelCell();
            });
        }
    }
}