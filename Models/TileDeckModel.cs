using System.Collections.Generic;
using Enums;
using UniRx;

namespace Models
{
    public class TileDeckModel
    {
        private readonly ReactiveCollection<TileType> _reactiveCollectionTileDeck = new();
        private readonly ReactiveProperty<int> _reactivePropertyCurrentTileDeckIndex = new(0);

        private readonly ReactiveDictionary<TileType, int> _reactiveDictionaryFixedTileAmounts = new();

        private readonly ReactiveProperty<int> _reactivePropertyExtraOneWayTilesCount = new(0);
        private readonly ReactiveProperty<int> _reactivePropertyExtraTwoWaysTilesCount = new(0);
        public void Initialize(FrameSize frameSize)
        {
            _reactiveCollectionTileDeck.Clear();
            _reactivePropertyCurrentTileDeckIndex.Value = 0;
            
            _reactiveDictionaryFixedTileAmounts.Clear();

            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Straight, 6);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Curve, 7);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.TwinCurves, 4);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Cross, 4);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.ThreeWayDistributor, 3);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.FourWayDistributor, 1);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Bulb, 2);

                    _reactivePropertyExtraOneWayTilesCount.Value = 2;
                    _reactivePropertyExtraTwoWaysTilesCount.Value = 1;
                    break;
                }
                case FrameSize.Medium:
                {
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Straight, 10);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Curve, 13);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.TwinCurves, 7);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Cross, 7);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.ThreeWayDistributor, 5);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.FourWayDistributor, 2);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Bulb, 6);

                    _reactivePropertyExtraOneWayTilesCount.Value = 7;
                    _reactivePropertyExtraTwoWaysTilesCount.Value = 3;
                    break;
                }
                case FrameSize.Large:
                {
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Straight, 18);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Curve, 23);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.TwinCurves, 10);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Cross, 11);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.ThreeWayDistributor, 8);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.FourWayDistributor, 3);
                    _reactiveDictionaryFixedTileAmounts.Add(TileType.Bulb, 10);

                    _reactivePropertyExtraOneWayTilesCount.Value = 11;
                    _reactivePropertyExtraTwoWaysTilesCount.Value = 6;
                    break;
                }
            }
        }

        public TileType TakeTile()
        {
            var tileType = _reactiveCollectionTileDeck[_reactivePropertyCurrentTileDeckIndex.Value];
            _reactivePropertyCurrentTileDeckIndex.Value++;
            
            return tileType;
        }

        public bool TryTakeValidTile(IEnumerable<TileType> tileTypes, out TileType validTileType)
        {
            var hashset = new HashSet<TileType>(tileTypes);

            for (var i = _reactivePropertyCurrentTileDeckIndex.Value; i < _reactiveCollectionTileDeck.Count; i++)
            {
                if (hashset.Contains(_reactiveCollectionTileDeck[i]))
                {
                    var tileType = _reactiveCollectionTileDeck[i];
                    var tmp = _reactiveCollectionTileDeck[i];
                    _reactiveCollectionTileDeck[i] =
                        _reactiveCollectionTileDeck[_reactivePropertyCurrentTileDeckIndex.Value];
                    _reactiveCollectionTileDeck[_reactivePropertyCurrentTileDeckIndex.Value] = tmp;
                    
                    _reactivePropertyCurrentTileDeckIndex.Value++;

                    for (var a = _reactiveCollectionTileDeck.Count - 1;
                         a > _reactivePropertyCurrentTileDeckIndex.Value + 1;
                         a--)
                    {
                        var b = UnityEngine.Random.Range(_reactivePropertyCurrentTileDeckIndex.Value, a);
                        var tmp2 = _reactiveCollectionTileDeck[a];
                        _reactiveCollectionTileDeck[a] = _reactiveCollectionTileDeck[b];
                        _reactiveCollectionTileDeck[b] = tmp2;
                    }

                    validTileType = tileType;
                    return true;
                }
            }

            validTileType = default;
            return false;
        }

        public void Reset()
        {
            _reactiveCollectionTileDeck.Clear();
            
            var oneWayTiles = new List<TileType>()
            {
                TileType.Straight,
                TileType.Curve
            };

            var twoWaysTiles = new List<TileType>()
            {
                TileType.TwinCurves,
                TileType.Cross
            };


            foreach (var (tileType, amount) in _reactiveDictionaryFixedTileAmounts)
            {
                for (var i = 0; i < amount; i++)
                {
                    _reactiveCollectionTileDeck.Add(tileType);
                }
            }

            for (var i = 0; i < _reactivePropertyExtraOneWayTilesCount.Value; i++)
            {
                _reactiveCollectionTileDeck.Add(oneWayTiles[UnityEngine.Random.Range(0, oneWayTiles.Count)]);
            }

            for (var i = 0; i < _reactivePropertyExtraTwoWaysTilesCount.Value; i++)
            {
                _reactiveCollectionTileDeck.Add(twoWaysTiles[UnityEngine.Random.Range(0, twoWaysTiles.Count)]);
            }

            for (var a = _reactiveCollectionTileDeck.Count - 1; a > 1; a--)
            {
                var b = UnityEngine.Random.Range(0, a);
                var tmp = _reactiveCollectionTileDeck[a];
                _reactiveCollectionTileDeck[a] = _reactiveCollectionTileDeck[b];
                _reactiveCollectionTileDeck[b] = tmp;
            }

            _reactivePropertyCurrentTileDeckIndex.Value = 0;
        }
    }
}