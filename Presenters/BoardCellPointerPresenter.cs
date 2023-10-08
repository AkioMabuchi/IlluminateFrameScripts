using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;


namespace Presenters
{
    public class BoardCellPointerPresenter : IInitializable
    {
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly BoardCellPointer _boardCellPointer;

        [Inject]
        public BoardCellPointerPresenter(SelectedBoardCellModel selectedBoardCellModel,
            BoardCellPointer boardCellPointer)
        {
            _selectedBoardCellModel = selectedBoardCellModel;
            _boardCellPointer = boardCellPointer;
        }

        public void Initialize()
        {
            _selectedBoardCellModel.OnChangedSelectedBoardCell.Subscribe(cellPosition =>
            {
                _boardCellPointer.SetNullableCellPosition(cellPosition);
            });
        }
    }
}