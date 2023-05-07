using System;
using Models.Instances.Tiles;
using Parameters.Enums;
using UniRx;

namespace Models
{
    public class TilesModel
    {
        private readonly ReactiveDictionary<int, TileBaseModel> _reactiveDictionaryTiles = new();
        public IObservable<DictionaryAddEvent<int, TileBaseModel>> OnAddedTile => 
            _reactiveDictionaryTiles.ObserveAdd();

        public IObservable<DictionaryRemoveEvent<int, TileBaseModel>> OnRemovedTile =>
            _reactiveDictionaryTiles.ObserveRemove();

        public IObservable<Unit> OnResetTiles => _reactiveDictionaryTiles.ObserveReset();

        private readonly ReactiveProperty<int> _reactivePropertyAllocatingTileId = new(0);
        
        public int AddTile(TileType tileType)
        {
            var tileId = _reactivePropertyAllocatingTileId.Value;

            switch (tileType)
            {
                case TileType.Straight:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileStraightModel());
                    break;
                }
                case TileType.Curve:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileCurveModel());
                    break;
                }
                case TileType.TwinCurves:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTwinCurvesModel());
                    break;
                }
                case TileType.Cross:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileCrossModel());
                    break;
                }
                case TileType.Distributor3:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileDistributorModel3());
                    break;
                }
                case TileType.Distributor4:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileDistributorModel4());
                    break;
                }
                case TileType.Bulb:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileBulbModel());
                    break;
                }
                case TileType.PowerNormal:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TilePowerNormalModel());
                    break;
                }
                case TileType.PowerPlus:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TilePowerPlusModel());
                    break;
                }
                case TileType.PowerMinus:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TilePowerMinusModel());
                    break;
                }
                case TileType.PowerAlternating:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TilePowerAlternatingModel());
                    break;
                }
                case TileType.TerminalNormalL:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalNormalLModel());
                    break;
                }
                case TileType.TerminalNormalR:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalNormalRModel());
                    break;
                }
                case TileType.TerminalPlus:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalPlusModel());
                    break;
                }
                case TileType.TerminalMinus:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalMinusModel());
                    break;
                }
                case TileType.TerminalAlternatingL:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalAlternatingLModel());
                    break;
                }
                case TileType.TerminalAlternatingR:
                {
                    _reactiveDictionaryTiles.Add(tileId, new TileTerminalAlternatingRModel());
                    break;
                }
            }

            _reactivePropertyAllocatingTileId.Value++;

            return tileId;
        }

        public void RotateTile(int tileId)
        {
            if (_reactiveDictionaryTiles.TryGetValue(tileId, out var tileModel))
            {
                tileModel.Rotate();
            }
        }

        public void RemoveTile(int tileId)
        {
            _reactiveDictionaryTiles.Remove(tileId);
        }

        public void ClearTiles()
        {
            _reactiveDictionaryTiles.Clear();
            _reactivePropertyAllocatingTileId.Value = 0;
        }

        public TileBaseModel GetTileModel(int tileId)
        {
            if(_reactiveDictionaryTiles.TryGetValue(tileId, out var tileModel))
            {
                return tileModel;
            }

            return null;
        }
    }
}