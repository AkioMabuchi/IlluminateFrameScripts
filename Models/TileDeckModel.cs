using System.Collections.Generic;
using Enums;
using UniRx;

namespace Models
{
    public class TileDeckModel
    {
        private readonly ReactiveCollection<TileType> _reactiveCollectionTileDeck = new();
        private readonly ReactiveProperty<int> _reactivePropertyCurrentTileDeckIndex = new(0);

        public void ResetTileDeck(FrameSize frameSize)
        {
            _reactiveCollectionTileDeck.Clear();
            _reactivePropertyCurrentTileDeckIndex.Value = 0;

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

            var fixedTilesAmounts = frameSize switch
            {
                FrameSize.Small => new Dictionary<TileType, int>
                {
                    {TileType.Straight, 6},
                    {TileType.Curve, 7},
                    {TileType.TwinCurves, 4},
                    {TileType.Cross, 4},
                    {TileType.Distributor3, 3},
                    {TileType.Distributor4, 1},
                    {TileType.Bulb, 2}
                },
                FrameSize.Medium => new Dictionary<TileType, int>
                {
                    {TileType.Straight, 10},
                    {TileType.Curve, 13},
                    {TileType.TwinCurves, 7},
                    {TileType.Cross, 7},
                    {TileType.Distributor3, 5},
                    {TileType.Distributor4, 2},
                    {TileType.Bulb, 6}
                },
                FrameSize.Large => new Dictionary<TileType, int>()
                {
                    {TileType.Straight, 18},
                    {TileType.Curve, 23},
                    {TileType.TwinCurves, 10},
                    {TileType.Cross, 11},
                    {TileType.Distributor3, 8},
                    {TileType.Distributor4, 3},
                    {TileType.Bulb, 10}
                },
                _ => new Dictionary<TileType, int>()
            };

            var extraOneWayTilesAmount = frameSize switch
            {
                FrameSize.Small => 2,
                FrameSize.Medium => 7,
                FrameSize.Large => 11,
                _ => 0
            };

            var extraTwoWaysTilesAmount = frameSize switch
            {
                FrameSize.Small => 1,
                FrameSize.Medium => 3,
                FrameSize.Large => 6,
                _ => 0
            };

            foreach (var (tileType, amount) in fixedTilesAmounts)
            {
                for (var i = 0; i < amount; i++)
                {
                    _reactiveCollectionTileDeck.Add(tileType);
                }
            }

            for (var i = 0; i < extraOneWayTilesAmount; i++)
            {
                _reactiveCollectionTileDeck.Add(oneWayTiles[UnityEngine.Random.Range(0, oneWayTiles.Count)]);
            }

            for (var i = 0; i < extraTwoWaysTilesAmount; i++)
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
        }

        public TileType TakeTile()
        {
            var tileType = _reactiveCollectionTileDeck[_reactivePropertyCurrentTileDeckIndex.Value];
            _reactivePropertyCurrentTileDeckIndex.Value++;
            
            return tileType;
        }

        public TileType? TakeValidTile(IEnumerable<TileType> tileTypes)
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

                    return tileType;
                }
            }

            return null;
        }
    }
}