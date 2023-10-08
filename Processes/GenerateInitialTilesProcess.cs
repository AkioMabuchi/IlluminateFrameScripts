using Enums;
using Interfaces.Tiles;
using Models;
using Models.Instances.Tiles;
using Models.Instances.Tiles.Powers;
using Models.Instances.Tiles.Terminals;
using Presenters.Controllers.Tiles.Powers;
using Presenters.Controllers.Tiles.Terminals;
using VContainer;
using Views.Controllers;
using Views.Instances.Tiles;
using Views.TileFactories.Powers;
using Views.TileFactories.Terminals;

namespace Processes
{
    public class GenerateInitialTilesProcess
    {
        private readonly MainFrameModel _mainFrameModel;
        private readonly TileIdModel _tileIdModel;

        private readonly MainBoardModel _mainBoardModel;
        private readonly ValidCellPositionsModel _validCellPositionsModel;

        private readonly NormalPowerTileFactory _normalPowerTileFactory;
        private readonly PlusPowerTileFactory _plusPowerTileFactory;
        private readonly MinusPowerTileFactory _minusPowerTileFactory;
        private readonly AlternatingPowerTileFactory _alternatingPowerTileFactory;

        private readonly NormalTerminalTileFactoryLeft _normalTerminalTileFactoryLeft;
        private readonly NormalTerminalTileFactoryRight _normalTerminalTileFactoryRight;
        private readonly PlusTerminalTileFactory _plusTerminalTileFactory;
        private readonly MinusTerminalTileFactory _minusTerminalTileFactory;
        private readonly AlternatingTerminalTileFactoryLeft _alternatingTerminalTileFactoryLeft;
        private readonly AlternatingTerminalTileFactoryRight _alternatingTerminalTileFactoryRight;
        
        private readonly NormalPowerTilePresenters _normalPowerTilePresenters;
        private readonly PlusPowerTilePresenters _plusPowerTilePresenters;
        private readonly MinusPowerTilePresenters _minusPowerTilePresenters;
        private readonly AlternatingPowerTilePresenters _alternatingPowerTilePresenters;

        private readonly NormalTerminalTilePresentersLeft _normalTerminalTilePresentersLeft;
        private readonly NormalTerminalTilePresentersRight _normalTerminalTilePresentersRight;
        private readonly PlusTerminalTilePresenters _plusTerminalTilePresenters;
        private readonly MinusTerminalTilePresenters _minusTerminalTilePresenters;
        private readonly AlternatingTerminalTilePresentersLeft _alternatingTerminalTilePresentersLeft;
        private readonly AlternatingTerminalTilePresentersRight _alternatingTerminalTilePresentersRight;

        private readonly BoardTilePositioner _boardTilePositioner;

        private readonly BulbTiles _bulbTiles;
        private readonly TerminalTiles _terminalTiles;
        private readonly TileRenderer _tileRenderer;

        [Inject]
        public GenerateInitialTilesProcess(MainFrameModel mainFrameModel, TileIdModel tileIdModel,
            MainBoardModel mainBoardModel,
            ValidCellPositionsModel validCellPositionsModel,
            NormalPowerTileFactory normalPowerTileFactory, PlusPowerTileFactory plusPowerTileFactory,
            MinusPowerTileFactory minusPowerTileFactory, AlternatingPowerTileFactory alternatingPowerTileFactory,
            NormalTerminalTileFactoryLeft normalTerminalTileFactoryLeft,
            NormalTerminalTileFactoryRight normalTerminalTileFactoryRight,
            PlusTerminalTileFactory plusTerminalTileFactory, MinusTerminalTileFactory minusTerminalTileFactory,
            AlternatingTerminalTileFactoryLeft alternatingTerminalTileFactoryLeft,
            AlternatingTerminalTileFactoryRight alternatingTerminalTileFactoryRight,
            NormalPowerTilePresenters normalPowerTilePresenters, PlusPowerTilePresenters plusPowerTilePresenters,
            MinusPowerTilePresenters minusPowerTilePresenters,
            AlternatingPowerTilePresenters alternatingPowerTilePresenters,
            NormalTerminalTilePresentersLeft normalTerminalTilePresentersLeft,
            NormalTerminalTilePresentersRight normalTerminalTilePresentersRight,
            PlusTerminalTilePresenters plusTerminalTilePresenters,
            MinusTerminalTilePresenters minusTerminalTilePresenters,
            AlternatingTerminalTilePresentersLeft alternatingTerminalTilePresentersLeft,
            AlternatingTerminalTilePresentersRight alternatingTerminalTilePresentersRight,
            BoardTilePositioner boardTilePositioner, BulbTiles bulbTiles, TerminalTiles terminalTiles,
            TileRenderer tileRenderer)
        {
            _mainFrameModel = mainFrameModel;
            _tileIdModel = tileIdModel;

            _mainBoardModel = mainBoardModel;
            _validCellPositionsModel = validCellPositionsModel;

            _normalPowerTileFactory = normalPowerTileFactory;
            _plusPowerTileFactory = plusPowerTileFactory;
            _minusPowerTileFactory = minusPowerTileFactory;
            _alternatingPowerTileFactory = alternatingPowerTileFactory;

            _normalTerminalTileFactoryLeft = normalTerminalTileFactoryLeft;
            _normalTerminalTileFactoryRight = normalTerminalTileFactoryRight;
            _plusTerminalTileFactory = plusTerminalTileFactory;
            _minusTerminalTileFactory = minusTerminalTileFactory;
            _alternatingTerminalTileFactoryLeft = alternatingTerminalTileFactoryLeft;
            _alternatingTerminalTileFactoryRight = alternatingTerminalTileFactoryRight;

            _normalPowerTilePresenters = normalPowerTilePresenters;
            _plusPowerTilePresenters = plusPowerTilePresenters;
            _minusPowerTilePresenters = minusPowerTilePresenters;
            _alternatingPowerTilePresenters = alternatingPowerTilePresenters;
            _normalTerminalTilePresentersLeft = normalTerminalTilePresentersLeft;
            _normalTerminalTilePresentersRight = normalTerminalTilePresentersRight;
            _plusTerminalTilePresenters = plusTerminalTilePresenters;
            _minusTerminalTilePresenters = minusTerminalTilePresenters;
            _alternatingTerminalTilePresentersLeft = alternatingTerminalTilePresentersLeft;
            _alternatingTerminalTilePresentersRight = alternatingTerminalTilePresentersRight;

            _boardTilePositioner = boardTilePositioner;

            _bulbTiles = bulbTiles;
            _terminalTiles = terminalTiles;
            _tileRenderer = tileRenderer;
        }

        public void GenerateInitialTiles()
        {
            foreach (var (cellPosition, tileType) in _mainFrameModel.FrameModel.InitialTiles)
            {
                TileModelBase tileModel = tileType switch
                {
                    TileType.NormalPower => new NormalPowerTileModel(),
                    TileType.PlusPower => new PlusPowerTileModel(),
                    TileType.MinusPower => new MinusPowerTileModel(),
                    TileType.AlternatingPower => new AlternatingPowerTileModel(),
                    TileType.NormalTerminalLeft => new NormalTerminalTileModelLeft(),
                    TileType.NormalTerminalRight => new NormalTerminalTileModelRight(),
                    TileType.PlusTerminal => new PlusTerminalTileModel(),
                    TileType.MinusTerminal => new MinusTerminalTileModel(),
                    TileType.AlternatingTerminalLeft => new AlternatingTerminalTileModelLeft(),
                    TileType.AlternatingTerminalRight => new AlternatingTerminalTileModelRight(),
                    _ => null
                };

                if (tileModel == null)
                {
                    continue;
                }

                var tileId = _tileIdModel.GetTileId();

                TileBase tile = null;

                switch (tileModel)
                {
                    case NormalPowerTileModel normalPowerTileModel:
                    {
                        var normalPowerTile = _normalPowerTileFactory.Generate(tileId);
                        _normalPowerTilePresenters.Add(tileId, normalPowerTileModel, normalPowerTile);
                        tile = normalPowerTile;
                        break;
                    }
                    case PlusPowerTileModel plusPowerTileModel:
                    {
                        var plusPowerTile = _plusPowerTileFactory.Generate(tileId);
                        _plusPowerTilePresenters.Add(tileId, plusPowerTileModel, plusPowerTile);
                        tile = plusPowerTile;
                        break;
                    }
                    case MinusPowerTileModel minusPowerTileModel:
                    {
                        var minusPowerTile = _minusPowerTileFactory.Generate(tileId);
                        _minusPowerTilePresenters.Add(tileId, minusPowerTileModel, minusPowerTile);
                        tile = minusPowerTile;
                        break;
                    }
                    case AlternatingPowerTileModel alternatingPowerTileModel:
                    {
                        var alternatingPowerTile = _alternatingPowerTileFactory.Generate(tileId);
                        _alternatingPowerTilePresenters.Add(tileId, alternatingPowerTileModel, alternatingPowerTile);
                        tile = alternatingPowerTile;
                        break;
                    }
                    case NormalTerminalTileModelLeft normalTerminalTileModelLeft:
                    {
                        var normalTerminalTileLeft = _normalTerminalTileFactoryLeft.Generate(tileId);
                        _normalTerminalTilePresentersLeft.Add(tileId, normalTerminalTileModelLeft,
                            normalTerminalTileLeft);
                        tile = normalTerminalTileLeft;
                        break;
                    }
                    case NormalTerminalTileModelRight normalTerminalTileModelRight:
                    {
                        var normalTerminalTileRight = _normalTerminalTileFactoryRight.Generate(tileId);
                        _normalTerminalTilePresentersRight.Add(tileId, normalTerminalTileModelRight,
                            normalTerminalTileRight);
                        tile = normalTerminalTileRight;
                        break;
                    }
                    case PlusTerminalTileModel plusTerminalTileModel:
                    {
                        var plusTerminalTile = _plusTerminalTileFactory.Generate(tileId);
                        _plusTerminalTilePresenters.Add(tileId, plusTerminalTileModel, plusTerminalTile);
                        tile = plusTerminalTile;
                        break;
                    }
                    case MinusTerminalTileModel minusTerminalTileModel:
                    {
                        var minusTerminalTile = _minusTerminalTileFactory.Generate(tileId);
                        _minusTerminalTilePresenters.Add(tileId, minusTerminalTileModel, minusTerminalTile);
                        tile = minusTerminalTile;
                        break;
                    }
                    case AlternatingTerminalTileModelLeft alternatingTerminalTileModelLeft:
                    {
                        var alternatingTerminalTileLeft = _alternatingTerminalTileFactoryLeft.Generate(tileId);
                        _alternatingTerminalTilePresentersLeft.Add(tileId, alternatingTerminalTileModelLeft,
                            alternatingTerminalTileLeft);
                        tile = alternatingTerminalTileLeft;
                        break;
                    }
                    case AlternatingTerminalTileModelRight alternatingTerminalTileModelRight:
                    {
                        var alternatingTerminalTileRight = _alternatingTerminalTileFactoryRight.Generate(tileId);
                        _alternatingTerminalTilePresentersRight.Add(tileId, alternatingTerminalTileModelRight,
                            alternatingTerminalTileRight);
                        tile = alternatingTerminalTileRight;
                        break;
                    }
                }

                if (tile == null)
                {
                    continue;
                }

                if (tile is IBulbTile bulbTile)
                {
                    _bulbTiles.AddTile(tileId, bulbTile);
                }

                if (tile is ITerminalTile terminalTile)
                {
                    _terminalTiles.AddTile(tileId, terminalTile);
                }
                    
                if (tile is IMovableTile movableTile)
                {
                    movableTile.Move(_boardTilePositioner.GetPosition(cellPosition));
                }

                if (tile is IRenderableTile renderableTile)
                {
                    _tileRenderer.AddTile(tileId, renderableTile);
                }

                _mainBoardModel.PutTile(cellPosition, tileId, tileModel);
            }
        }
    }
}