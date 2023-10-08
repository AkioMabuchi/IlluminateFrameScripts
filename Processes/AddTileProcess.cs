using Enums;
using Interfaces.Tiles;
using Models;
using Models.Instances.Tiles;
using Models.Instances.Tiles.Dynamics;
using Presenters.Controllers.Tiles.Dynamics;
using VContainer;
using Views.Controllers;
using Views.Instances.Tiles;
using Views.TileFactories.Dynamics;

namespace Processes
{
    public class AddTileProcess
    {
        private readonly TileIdModel _tileIdModel;
        

        private readonly StraightTileFactory _straightTileFactory;
        private readonly CurveTileFactory _curveTileFactory;
        private readonly TwinCurvesTileFactory _twinCurvesTileFactory;
        private readonly CrossTileFactory _crossTileFactory;
        private readonly ThreeWayDistributorTileFactory _threeWayDistributorTileFactory;
        private readonly FourWayDistributorTileFactory _fourWayDistributorTileFactory;
        private readonly BulbTileFactory _bulbTileFactory;

        private readonly StraightTilePresenters _straightTilePresenters;
        private readonly CurveTilePresenters _curveTilePresenters;
        private readonly TwinCurvesTilePresenters _twinCurvesTilePresenters;
        private readonly CrossTilePresenters _crossTilePresenters;
        private readonly ThreeWayDistributorTilePresenters _threeWayDistributorTilePresenters;
        private readonly FourWayDistributorTilePresenters _fourWayDistributorTilePresenters;
        private readonly BulbTilePresenters _bulbTilePresenters;

        private readonly BulbTiles _bulbTiles;
        private readonly TerminalTiles _terminalTiles;
        private readonly TileMover _tileMover;
        private readonly TileRenderer _tileRenderer;
        private readonly TileRotator _tileRotator;
        private readonly TileThrower _tileThrower;

        [Inject]
        public AddTileProcess(TileIdModel tileIdModel, StraightTileFactory straightTileFactory,
            CurveTileFactory curveTileFactory, TwinCurvesTileFactory twinCurvesTileFactory,
            CrossTileFactory crossTileFactory, ThreeWayDistributorTileFactory threeWayDistributorTileFactory,
            FourWayDistributorTileFactory fourWayDistributorTileFactory, BulbTileFactory bulbTileFactory,
            StraightTilePresenters straightTilePresenters, CurveTilePresenters curveTilePresenters,
            TwinCurvesTilePresenters twinCurvesTilePresenters, CrossTilePresenters crossTilePresenters,
            ThreeWayDistributorTilePresenters threeWayDistributorTilePresenters,
            FourWayDistributorTilePresenters fourWayDistributorTilePresenters, BulbTilePresenters bulbTilePresenters,
            BulbTiles bulbTiles, TerminalTiles terminalTiles, TileMover tileMover, TileRenderer tileRenderer,
            TileRotator tileRotator, TileThrower tileThrower)
        {
            _tileIdModel = tileIdModel;

            _straightTileFactory = straightTileFactory;
            _curveTileFactory = curveTileFactory;
            _twinCurvesTileFactory = twinCurvesTileFactory;
            _crossTileFactory = crossTileFactory;
            _threeWayDistributorTileFactory = threeWayDistributorTileFactory;
            _fourWayDistributorTileFactory = fourWayDistributorTileFactory;
            _bulbTileFactory = bulbTileFactory;

            _straightTilePresenters = straightTilePresenters;
            _curveTilePresenters = curveTilePresenters;
            _twinCurvesTilePresenters = twinCurvesTilePresenters;
            _crossTilePresenters = crossTilePresenters;
            _threeWayDistributorTilePresenters = threeWayDistributorTilePresenters;
            _fourWayDistributorTilePresenters = fourWayDistributorTilePresenters;
            _bulbTilePresenters = bulbTilePresenters;

            _bulbTiles = bulbTiles;
            _terminalTiles = terminalTiles;
            _tileMover = tileMover;
            _tileRenderer = tileRenderer;
            _tileRotator = tileRotator;
            _tileThrower = tileThrower;
        }

        public (int tileId, TileModelBase tileModel) AddTile(TileType tileType)
        {
            var tileId = _tileIdModel.GetTileId();

            TileModelBase tileModel = tileType switch
            {
                TileType.Straight => new StraightTileModel(),
                TileType.Curve => new CurveTileModel(),
                TileType.TwinCurves => new TwinCurvesTileModel(),
                TileType.Cross => new CrossTileModel(),
                TileType.ThreeWayDistributor => new ThreeWayDistributorTileModel(),
                TileType.FourWayDistributor => new FourWayDistributorTileModel(),
                TileType.Bulb => new BulbTileModel(),
                _ => null
            };

            TileBase tile = null;

            switch (tileModel)
            {
                case StraightTileModel straightTileModel:
                {
                    var straightTile = _straightTileFactory.Generate(tileId);
                    _straightTilePresenters.Add(tileId, straightTileModel, straightTile);
                    tile = straightTile;
                    break;
                }
                case CurveTileModel curveTileModel:
                {
                    var curveTile = _curveTileFactory.Generate(tileId);
                    _curveTilePresenters.Add(tileId, curveTileModel, curveTile);
                    tile = curveTile;
                    break;
                }
                case TwinCurvesTileModel twinCurvesTileModel:
                {
                    var twinCurvesTile = _twinCurvesTileFactory.Generate(tileId);
                    _twinCurvesTilePresenters.Add(tileId, twinCurvesTileModel, twinCurvesTile);
                    tile = twinCurvesTile;
                    break;
                }
                case CrossTileModel crossTileModel:
                {
                    var crossTile = _crossTileFactory.Generate(tileId);
                    _crossTilePresenters.Add(tileId, crossTileModel, crossTile);
                    tile = crossTile;
                    break;
                }
                case ThreeWayDistributorTileModel threeWayDistributorTileModel:
                {
                    var threeWayDistributorTile = _threeWayDistributorTileFactory.Generate(tileId);
                    _threeWayDistributorTilePresenters.Add(tileId, threeWayDistributorTileModel,
                        threeWayDistributorTile);
                    tile = threeWayDistributorTile;
                    break;
                }
                case FourWayDistributorTileModel fourWayDistributorTileModel:
                {
                    var fourWayDistributorTile = _fourWayDistributorTileFactory.Generate(tileId);
                    _fourWayDistributorTilePresenters.Add(tileId, fourWayDistributorTileModel, fourWayDistributorTile);
                    tile = fourWayDistributorTile;
                    break;
                }
                case BulbTileModel bulbTileModel:
                {
                    var bulbTile = _bulbTileFactory.Generate(tileId);
                    _bulbTilePresenters.Add(tileId, bulbTileModel, bulbTile);
                    tile = bulbTile;
                    break;
                }
            }

            if (tile is IBulbTile bulbTileSecond)
            {
                _bulbTiles.AddTile(tileId, bulbTileSecond);
            }

            if (tile is ITerminalTile terminalTile)
            {
                _terminalTiles.AddTile(tileId, terminalTile);
            }

            if (tile is IMovableTile movableTile)
            {
                _tileMover.AddTile(tileId, movableTile);
            }

            if (tile is IRenderableTile renderableTile)
            {
                renderableTile.RenderReset();
                _tileRenderer.AddTile(tileId, renderableTile);
            }

            if (tile is IRotatableTile rotatableTile)
            {
                rotatableTile.RotateReset();
                _tileRotator.AddTile(tileId, rotatableTile);
            }

            if (tile is IThrowableTile throwableTile)
            {
                throwableTile.ThrowReset();
                _tileThrower.AddTile(tileId, throwableTile);
            }

            return (tileId, tileModel);
        }
    }
}