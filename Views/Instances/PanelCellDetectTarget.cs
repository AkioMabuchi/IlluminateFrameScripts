using UnityEngine;

namespace Views.Instances
{
    public class PanelCellDetectTarget : MonoBehaviour
    {
        [SerializeField] private Vector2Int cellPosition;
        public Vector2Int CellPosition => cellPosition;
    
        [ContextMenu("SetCellPositionFromObjectName")]
        private void SetCellPositionFromObjectName()
        {
            var numStrings = gameObject.name.Split(",");
            var x = int.Parse(numStrings[0]);
            var y = int.Parse(numStrings[1]);
            cellPosition = new Vector2Int(x, y);
        }
    }
}
