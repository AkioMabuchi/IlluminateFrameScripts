using System;
using Enums;
using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Processes.Events
{
    public class OnBoardCellDetected: IInitializable, IDisposable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly CurrentTileModel _currentTileModel;
        private readonly SelectedBoardCellModel _selectedBoardCellModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;
        private readonly BoardCellDetector _boardCellDetector;

        private readonly CompositeDisposable _compositeDisposable = new();
        [Inject]
        public OnBoardCellDetected(GameStateModel gameStateModel, CurrentTileModel currentTileModel,
            SelectedBoardCellModel selectedBoardCellModel, ValidCellPositionsModel validCellPositionsModel,
            BoardCellDetector boardCellDetector)
        {
            _gameStateModel = gameStateModel;
            _currentTileModel = currentTileModel;
            _selectedBoardCellModel = selectedBoardCellModel;
            _validCellPositionsModel = validCellPositionsModel;
            _boardCellDetector = boardCellDetector;
        }

        public void Initialize()
        {
            _boardCellDetector.OnDetectPanelCell
                .Subscribe(cellPosition =>
                {
                    switch (_gameStateModel.GameStateName)
                    {
                        case GameStateName.Main:
                        {
                            if (_currentTileModel.TileModel == null)
                            {
                                _selectedBoardCellModel.Reset();
                                break;
                            }

                            if (_validCellPositionsModel.CanPutTile(_currentTileModel.TileModel, cellPosition))
                            {
                                _selectedBoardCellModel.SetSelectedPanelCell(cellPosition);
                            }
                            else
                            {
                                _selectedBoardCellModel.Reset();
                            }

                            break;
                        }
                        case GameStateName.Tutorial:
                        {
                            if (_currentTileModel.TileModel == null)
                            {
                                _selectedBoardCellModel.Reset();
                                break;
                            }

                            if (_validCellPositionsModel.CanPutTile(_currentTileModel.TileModel, cellPosition))
                            {
                                _selectedBoardCellModel.SetSelectedPanelCell(cellPosition);
                            }
                            else
                            {
                                _selectedBoardCellModel.Reset();
                            }

                            break;
                        }
                    }
                }).AddTo(_compositeDisposable);

            _boardCellDetector.OnDetectNone
                .Subscribe(_ =>
                {
                    _selectedBoardCellModel.Reset();
                }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}