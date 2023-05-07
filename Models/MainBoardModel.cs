using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public class MainBoardModel
    {
        private readonly ReactiveDictionary<Vector2Int, int> _reactiveDictionaryMainBoard = new();
        public IEnumerable<Vector2Int> CellPositions => _reactiveDictionaryMainBoard.Keys;
        public IEnumerable<KeyValuePair<Vector2Int, int>> CellPositionsAndTileIds => _reactiveDictionaryMainBoard;

        public void ClearBoard()
        {
            _reactiveDictionaryMainBoard.Clear();
        }
        public void PutTile(Vector2Int cellPosition, int panelId)
        {
            _reactiveDictionaryMainBoard[cellPosition] = panelId;
        }
        public int? GetPutTileId(Vector2Int cellPosition)
        {
            if (_reactiveDictionaryMainBoard.TryGetValue(cellPosition, out var cellId))
            {
                return cellId;
            }

            return null;
        }
    }
}