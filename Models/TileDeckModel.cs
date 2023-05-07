using System.Collections.Generic;
using Parameters.Enums;
using UniRx;
using UnityEngine;

namespace Models
{
    public class TileDeckModel
    {
        private readonly ReactiveCollection<TileType> _reactiveCollectionTileDeck = new();
        private readonly ReactiveProperty<int> _reactivePropertyCurrentTileDeckIndex = new(0);

        public void ResetTileDeck(PanelSize panelSize)
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

            switch (panelSize)
            {
                case PanelSize.Large:
                {
                    foreach (var (tileType, amount) in new Dictionary<TileType, int>()
                             {
                                 {TileType.Straight, 20},
                                 {TileType.Curve, 25},
                                 {TileType.TwinCurves, 8},
                                 {TileType.Cross, 9},
                                 {TileType.Distributor3, 8},
                                 {TileType.Distributor4, 3},
                                 {TileType.Bulb, 10},
                             })
                    {
                        for (var i = 0; i < amount; i++)
                        {
                            _reactiveCollectionTileDeck.Add(tileType);
                        }
                    }

                    for (var i = 0; i < 11; i++)
                    {
                        _reactiveCollectionTileDeck.Add(oneWayTiles[UnityEngine.Random.Range(0, oneWayTiles.Count)]);
                    }

                    for (var i = 0; i < 6; i++)
                    {
                        _reactiveCollectionTileDeck.Add(twoWaysTiles[UnityEngine.Random.Range(0, twoWaysTiles.Count)]);
                    }

                    break;
                }
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