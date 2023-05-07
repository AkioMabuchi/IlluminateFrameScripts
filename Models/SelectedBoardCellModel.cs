using System;
using UniRx;
using UnityEngine;

namespace Models
{
    public class SelectedBoardCellModel
    {
        private readonly ReactiveProperty<Vector2Int?> _reactivePropertySelectedPanelCell = new(null);
        public Vector2Int? SelectedPanelCell => _reactivePropertySelectedPanelCell.Value;

        public void SetSelectedPanelCell(Vector2Int cellPosition)
        {
            _reactivePropertySelectedPanelCell.Value = cellPosition;
        }

        public void SetSelectedPanelCellNull()
        {
            _reactivePropertySelectedPanelCell.Value = null;
        }
    }
}