using System.Collections.Generic;
using Models;
using Models.Instances.Tiles;
using Presenters.Instances;
using Presenters.Instances.Tiles;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class TilesPresenter : IInitializable
    {
        private readonly TilesModel _tilesModel;
        private readonly TileFactory _tileFactory;

        private readonly Dictionary<int, TileBasePresenter> _tilePresenters = new();
        
        [Inject]
        public TilesPresenter(TilesModel tilesModel, TileFactory tileFactory)
        {
            _tilesModel = tilesModel;
            _tileFactory = tileFactory;
        }
        
        public void Initialize()
        {
            _tilesModel.OnAddedTile.Subscribe(addedEvent =>
            {
                var tileId = addedEvent.Key;
                var tileModel = addedEvent.Value;

                switch (tileModel)
                {
                    case TileStraightModel tileStraightModel:
                    {
                        var tileStraight = _tileFactory.GenerateTileStraight(tileId);
                        _tilePresenters.Add(tileId, new TileLine1Presenter(tileStraightModel, tileStraight));
                        break;
                    }
                    case TileCurveModel tileCurveModel:
                    {
                        var tileCurve = _tileFactory.GenerateTileCurve(tileId);
                        _tilePresenters.Add(tileId, new TileLine1Presenter(tileCurveModel, tileCurve));
                        break;
                    }
                    case TileTwinCurvesModel tileTwinCurvesModel:
                    {
                        var tileTwinCurves = _tileFactory.GenerateTileTwinCurves(tileId);
                        _tilePresenters.Add(tileId, new TileLine2Presenter(tileTwinCurvesModel, tileTwinCurves));
                        break;
                    }
                    case TileCrossModel tileCrossModel:
                    {
                        var tileCross = _tileFactory.GenerateTileCross(tileId);
                        _tilePresenters.Add(tileId, new TileLine2Presenter(tileCrossModel, tileCross));
                        break;
                    }
                    case TileDistributorModel3 tileDistributorModel3:
                    {
                        var tileDistributor3 = _tileFactory.GenerateTileDistributor3(tileId);
                        _tilePresenters.Add(tileId,
                            new TileDistributor3Presenter(tileDistributorModel3, tileDistributor3));
                        break;
                    }
                    case TileDistributorModel4 tileDistributorModel4:
                    {
                        var tileDistributor4 = _tileFactory.GenerateTileDistributor4(tileId);
                        _tilePresenters.Add(tileId,
                            new TileDistributor4Presenter(tileDistributorModel4, tileDistributor4));
                        break;
                    }
                    case TileBulbModel tileBulbModel:
                    {
                        var tileBulb = _tileFactory.GenerateTileBulb(tileId);
                        _tilePresenters.Add(tileId, new TileBulbPresenter(tileBulbModel, tileBulb));
                        break;
                    }
                    case TilePowerNormalModel tilePowerNormalModel:
                    {
                        var tilePowerNormal = _tileFactory.GenerateTilePowerNormal(tileId);
                        _tilePresenters.Add(tileId,
                            new TilePower2Presenter(tilePowerNormalModel, tilePowerNormal));
                        break;
                    }
                    case TilePowerPlusModel tilePowerPlusModel:
                    {
                        var tilePowerPlus = _tileFactory.GenerateTilePowerPlus(tileId);
                        _tilePresenters.Add(tileId, new TilePower1Presenter(tilePowerPlusModel, tilePowerPlus));
                        break;
                    }
                    case TilePowerMinusModel tilePowerMinusModel:
                    {
                        var tilePowerMinus = _tileFactory.GenerateTilePowerMinus(tileId);
                        _tilePresenters.Add(tileId, new TilePower1Presenter(tilePowerMinusModel, tilePowerMinus));
                        break;
                    }
                    case TilePowerAlternatingModel tilePowerAlternatingModel:
                    {
                        var tilePowerAlternating = _tileFactory.GenerateTilePowerAlternating(tileId);
                        _tilePresenters.Add(tileId,
                            new TilePower2Presenter(tilePowerAlternatingModel, tilePowerAlternating));
                        break;
                    }
                    case TileTerminalNormalLModel tileTerminalNormalLModel:
                    {
                        var tileTerminalNormalL = _tileFactory.GenerateTileTerminalNormalL(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalNormalLModel, tileTerminalNormalL));
                        break;
                    }
                    case TileTerminalNormalRModel tileTerminalNormalRModel:
                    {
                        var tileTerminalNormalR = _tileFactory.GenerateTileTerminalNormalR(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalNormalRModel, tileTerminalNormalR));
                        break;
                    }
                    case TileTerminalPlusModel tileTerminalPlusModel:
                    {
                        var tileTerminalPlus = _tileFactory.GenerateTileTerminalPlus(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalPlusModel, tileTerminalPlus));
                        break;
                    }
                    case TileTerminalMinusModel tileTerminalMinusModel:
                    {
                        var tileTerminalMinus = _tileFactory.GenerateTileTerminalMinus(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalMinusModel, tileTerminalMinus));
                        break;
                    }
                    case TileTerminalAlternatingLModel tileTerminalAlternatingLModel:
                    {
                        var terminalAlternatingL = _tileFactory.GenerateTileTerminalAlternatingL(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalAlternatingLModel, terminalAlternatingL));
                        break;
                    }
                    case TileTerminalAlternatingRModel tileTerminalAlternatingRModel:
                    {
                        var tileTerminalAlternatingR = _tileFactory.GenerateTileTerminalAlternatingR(tileId);
                        _tilePresenters.Add(tileId,
                            new TileTerminalPresenter(tileTerminalAlternatingRModel, tileTerminalAlternatingR));
                        break;
                    }
                }
            });

            _tilesModel.OnRemovedTile.Subscribe(removedEvent =>
            {
                var tileId = removedEvent.Key;
                if (_tilePresenters.TryGetValue(tileId, out var tilePresenter))
                {
                    tilePresenter.CompositeDispose();
                }

                _tilePresenters.Remove(tileId);
                _tileFactory.ClearTile(tileId);
            });

            _tilesModel.OnResetTiles.Subscribe(_ =>
            {
                foreach (var tilePresenter in _tilePresenters.Values)
                {
                    tilePresenter.CompositeDispose();
                }
                
                _tilePresenters.Clear();
                _tileFactory.ClearAllTiles();
            });
        }
    }
}