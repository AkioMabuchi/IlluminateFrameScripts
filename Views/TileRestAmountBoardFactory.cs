using System;
using Parameters.Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class TileRestAmountBoardFactory : MonoBehaviour
    {
        [SerializeField] private NumberDisplayBoard prefabTileRestAmountBoard;

        private NumberDisplayBoard _tileRestAmountBoard;

        public void SetTileRestAmountBoardDisplayMode(NumberDisplayMode displayMode)
        {
            if (_tileRestAmountBoard)
            {
                _tileRestAmountBoard.SetDisplayMode(displayMode);
            }
        }

        public void SetTileRestAmountBoardTileRestAmount(int tileRestAmount)
        {
            if (_tileRestAmountBoard)
            {
                _tileRestAmountBoard.SetDisplayNumber(tileRestAmount);
            }
        }

        public void GenerateTileRestAmountBoard(PanelSize panelSize)
        {
            var position = panelSize switch
            {
                PanelSize.Large => new Vector3(-1.05f, 0.0f, 0.5f),
                _ => Vector3.zero
            };

            var rotation = panelSize switch
            {
                _ => Quaternion.identity
            };

            _tileRestAmountBoard = Instantiate(prefabTileRestAmountBoard, position, rotation, transform);
        }
    }
}
