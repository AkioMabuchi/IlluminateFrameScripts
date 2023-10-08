using Models;
using Presenters.Controllers.Tiles.Dynamics;
using Presenters.Controllers.Tiles.Powers;
using Presenters.Controllers.Tiles.Terminals;
using VContainer;
using Views.Controllers;
using Views.TileFactories.Dynamics;
using Views.TileFactories.Powers;
using Views.TileFactories.Terminals;

namespace Processes
{
    public class ClearMainGameProcess
    {
        private readonly MainBoardModel _mainBoardModel;
        private readonly TileIdModel _tileIdModel;

        private readonly StraightTileFactory _straightTileFactory;
        private readonly CurveTileFactory _curveTileFactory;
        private readonly TwinCurvesTileFactory _twinCurvesTileFactory;
        private readonly CrossTileFactory _crossTileFactory;
        private readonly ThreeWayDistributorTileFactory _threeWayDistributorTileFactory;
        private readonly FourWayDistributorTileFactory _fourWayDistributorTileFactory;
        private readonly BulbTileFactory _bulbTileFactory;

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

        private readonly StraightTilePresenters _straightTilePresenters;
        private readonly CurveTilePresenters _curveTilePresenters;
        private readonly TwinCurvesTilePresenters _twinCurvesTilePresenters;
        private readonly CrossTilePresenters _crossTilePresenters;
        private readonly ThreeWayDistributorTilePresenters _threeWayDistributorTilePresenters;
        private readonly FourWayDistributorTilePresenters _fourWayDistributorTilePresenters;
        private readonly BulbTilePresenters _bulbTilePresenters;

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

        private readonly BulbTiles _bulbTiles;
        private readonly TerminalTiles _terminalTiles;
        private readonly TileMover _tileMover;
        private readonly TileRenderer _tileRenderer;
        private readonly TileRotator _tileRotator;
        private readonly TileThrower _tileThrower;

        [Inject]
        public ClearMainGameProcess(MainBoardModel mainBoardModel, TileIdModel tileIdModel,
            StraightTileFactory straightTileFactory, CurveTileFactory curveTileFactory,
            TwinCurvesTileFactory twinCurvesTileFactory, CrossTileFactory crossTileFactory,
            ThreeWayDistributorTileFactory threeWayDistributorTileFactory,
            FourWayDistributorTileFactory fourWayDistributorTileFactory, BulbTileFactory bulbTileFactory,
            NormalPowerTileFactory normalPowerTileFactory, PlusPowerTileFactory plusPowerTileFactory,
            MinusPowerTileFactory minusPowerTileFactory, AlternatingPowerTileFactory alternatingPowerTileFactory,
            NormalTerminalTileFactoryLeft normalTerminalTileFactoryLeft,
            NormalTerminalTileFactoryRight normalTerminalTileFactoryRight,
            PlusTerminalTileFactory plusTerminalTileFactory, MinusTerminalTileFactory minusTerminalTileFactory,
            AlternatingTerminalTileFactoryLeft alternatingTerminalTileFactoryLeft,
            AlternatingTerminalTileFactoryRight alternatingTerminalTileFactoryRight,
            StraightTilePresenters straightTilePresenters, CurveTilePresenters curveTilePresenters,
            TwinCurvesTilePresenters twinCurvesTilePresenters, CrossTilePresenters crossTilePresenters,
            ThreeWayDistributorTilePresenters threeWayDistributorTilePresenters,
            FourWayDistributorTilePresenters fourWayDistributorTilePresenters, BulbTilePresenters bulbTilePresenters,
            NormalPowerTilePresenters normalPowerTilePresenters, PlusPowerTilePresenters plusPowerTilePresenters,
            MinusPowerTilePresenters minusPowerTilePresenters,
            AlternatingPowerTilePresenters alternatingPowerTilePresenters,
            NormalTerminalTilePresentersLeft normalTerminalTilePresentersLeft,
            NormalTerminalTilePresentersRight normalTerminalTilePresentersRight,
            PlusTerminalTilePresenters plusTerminalTilePresenters,
            MinusTerminalTilePresenters minusTerminalTilePresenters,
            AlternatingTerminalTilePresentersLeft alternatingTerminalTilePresentersLeft,
            AlternatingTerminalTilePresentersRight alternatingTerminalTilePresentersRight,
            BulbTiles bulbTiles, TerminalTiles terminalTiles, TileMover tileMover, TileRenderer tileRenderer,
            TileRotator tileRotator, TileThrower tileThrower)
        {
            _mainBoardModel = mainBoardModel;
            _tileIdModel = tileIdModel;

            _straightTileFactory = straightTileFactory;
            _curveTileFactory = curveTileFactory;
            _twinCurvesTileFactory = twinCurvesTileFactory;
            _crossTileFactory = crossTileFactory;
            _threeWayDistributorTileFactory = threeWayDistributorTileFactory;
            _fourWayDistributorTileFactory = fourWayDistributorTileFactory;
            _bulbTileFactory = bulbTileFactory;
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

            _straightTilePresenters = straightTilePresenters;
            _curveTilePresenters = curveTilePresenters;
            _twinCurvesTilePresenters = twinCurvesTilePresenters;
            _crossTilePresenters = crossTilePresenters;
            _threeWayDistributorTilePresenters = threeWayDistributorTilePresenters;
            _fourWayDistributorTilePresenters = fourWayDistributorTilePresenters;
            _bulbTilePresenters = bulbTilePresenters;
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

            _bulbTiles = bulbTiles;
            _terminalTiles = terminalTiles;
            _tileMover = tileMover;
            _tileRenderer = tileRenderer;
            _tileRotator = tileRotator;
            _tileThrower = tileThrower;
        }

        public void ClearMainGame()
        {
            _mainBoardModel.Clear();
            _tileIdModel.Initialize();
            
            _straightTilePresenters.Clear();
            _curveTilePresenters.Clear();
            _twinCurvesTilePresenters.Clear();
            _crossTilePresenters.Clear();
            _threeWayDistributorTilePresenters.Clear();
            _fourWayDistributorTilePresenters.Clear();
            _bulbTilePresenters.Clear();
            _normalPowerTilePresenters.Clear();
            _plusPowerTilePresenters.Clear();
            _minusPowerTilePresenters.Clear();
            _alternatingPowerTilePresenters.Clear();
            _normalTerminalTilePresentersLeft.Clear();
            _normalTerminalTilePresentersRight.Clear();
            _plusTerminalTilePresenters.Clear();
            _minusTerminalTilePresenters.Clear();
            _alternatingTerminalTilePresentersLeft.Clear();
            _alternatingTerminalTilePresentersRight.Clear();
            
            _straightTileFactory.ClearAll();
            _curveTileFactory.ClearAll();
            _twinCurvesTileFactory.ClearAll();
            _crossTileFactory.ClearAll();
            _threeWayDistributorTileFactory.ClearAll();
            _fourWayDistributorTileFactory.ClearAll();
            _bulbTileFactory.ClearAll();
            _normalPowerTileFactory.ClearAll();
            _plusPowerTileFactory.ClearAll();
            _minusPowerTileFactory.ClearAll();
            _alternatingPowerTileFactory.ClearAll();
            _normalTerminalTileFactoryLeft.ClearAll();
            _normalTerminalTileFactoryRight.ClearAll();
            _plusTerminalTileFactory.ClearAll();
            _minusTerminalTileFactory.ClearAll();
            _alternatingTerminalTileFactoryLeft.ClearAll();
            _alternatingTerminalTileFactoryRight.ClearAll();
            
            _bulbTiles.ClearAllTiles();
            _terminalTiles.ClearAllTiles();
            _tileMover.ClearAllTiles();
            _tileRenderer.ClearAllTiles();
            _tileRotator.ClearAllTiles();
            _tileThrower.ClearAllTiles();
        }
    }
}