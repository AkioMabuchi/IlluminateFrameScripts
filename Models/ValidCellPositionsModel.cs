using System.Collections.Generic;
using Parameters.Enums;
using UniRx;
using UnityEngine;

namespace Models
{
    public class ValidCellPositionsModel
    {
        private readonly ReactiveDictionary<TileType, IReadOnlyDictionary<Vector2Int, HashSet<RotateStatus>>>
            _reactiveDictionaryValidCellPositions = new();

        public void ClearValidCellPositions()
        {
            _reactiveDictionaryValidCellPositions.Clear();
        }

        public void SetValidCellPositions(TileType tileType,
            IReadOnlyDictionary<Vector2Int, HashSet<RotateStatus>> validCellPositions)
        {
            _reactiveDictionaryValidCellPositions.Add(tileType, validCellPositions);
        }

        public bool CanPutTileType(TileType tileType)
        {
            return _reactiveDictionaryValidCellPositions.ContainsKey(tileType);
        }

        public bool CanPutTile(TileType tileType, RotateStatus rotateStatus, Vector2Int cellPosition)
        {
            if(_reactiveDictionaryValidCellPositions.TryGetValue(tileType, out var dictionary))
            {
                if( dictionary.TryGetValue(cellPosition, out var cellPositions))
                {
                    if (cellPositions.Contains(rotateStatus))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}