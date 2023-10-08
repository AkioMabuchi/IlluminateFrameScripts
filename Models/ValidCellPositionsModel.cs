using System.Collections.Generic;
using Enums;
using Interfaces.TileModels;
using Models.Instances.Tiles;
using UniRx;
using UnityEngine;

namespace Models
{
    public class ValidCellPositionsModel
    {
        private readonly ReactiveDictionary<TileType, IReadOnlyDictionary<Vector2Int, HashSet<RotateStatus>>>
            _reactiveDictionaryValidCellPositions = new();

        public IEnumerable<TileType> ValidTiles => _reactiveDictionaryValidCellPositions.Keys;

        public bool CanPutTileType(TileType tileType)
        {
            return _reactiveDictionaryValidCellPositions.ContainsKey(tileType);
        }

        public bool CanPutTile(TileModelBase tileModel, Vector2Int cellPosition)
        {
            if (tileModel is ICheckableValidTileModel checkableValidTileModel)
            {
                var tileType = checkableValidTileModel.TileType;
                var rotateStatus = checkableValidTileModel.RotateStatus;

                if (_reactiveDictionaryValidCellPositions.TryGetValue(tileType, out var dictionary))
                {
                    if (dictionary.TryGetValue(cellPosition, out var cellPositions))
                    {
                        if (cellPositions.Contains(rotateStatus))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void SetValidCellPositions(
            IReadOnlyDictionary<TileType, IReadOnlyDictionary<Vector2Int, IEnumerable<RotateStatus>>>
                validCellPositions)
        {
            _reactiveDictionaryValidCellPositions.Clear();

            foreach (var (tileType, value)in validCellPositions)
            {
                var dictionary = new Dictionary<Vector2Int, HashSet<RotateStatus>>();
                foreach (var (cellPosition, rotateStatuses)in value)
                {
                    var hashSetRotateStatuses = new HashSet<RotateStatus>(rotateStatuses);
                    dictionary.Add(cellPosition, hashSetRotateStatuses);
                }

                _reactiveDictionaryValidCellPositions.Add(tileType, dictionary);
            }
        }
    }
}