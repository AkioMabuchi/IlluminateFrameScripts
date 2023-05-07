using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Instances;

namespace ForDevelopments
{
    public class PanelCellDetectTargetsGenerator : MonoBehaviour
    {
        [SerializeField] private PanelCellDetectTarget prefabPanelCellDetectTarget;
        [SerializeField] private Transform transformPanelCellDetectTargets;
        [SerializeField] private List<PanelCellDetectTarget> panelCellDetectTargets;

        [SerializeField] private RectInt layout;
        [SerializeField] private float tileSize = 0.1f;
        [SerializeField] private float panelBaseHeight = 0.01f;
        [SerializeField] private Vector2 offset = Vector2.zero;
    
        [ContextMenu("GeneratePanelCellDetectTargets")]
        private void GeneratePanelCellDetectTargets()
        {
            foreach (var panelCellDetectTarget in panelCellDetectTargets)
            {
                DestroyImmediate(panelCellDetectTarget.gameObject);
            }
        
            panelCellDetectTargets.Clear();

            for (var x = 0; x < layout.width; x++)
            {
                for (var y = 0; y < layout.height; y++)
                {
                    Debug.Log("sss");
                    var cellPositionX = x + layout.x;
                    var cellPositionY = y + layout.y;
                    var panelCellDetectTarget =
                        Instantiate(prefabPanelCellDetectTarget, transformPanelCellDetectTargets);
                    var positionX = cellPositionX * tileSize + offset.x;
                    var positionY = panelBaseHeight;
                    var positionZ = cellPositionY * tileSize + offset.y;

                    panelCellDetectTarget.name = cellPositionX + "," + cellPositionY;
                    panelCellDetectTarget.transform.localPosition = new Vector3(positionX, positionY, positionZ);
                
                    panelCellDetectTargets.Add(panelCellDetectTarget);
                }
            }
        }

        [ContextMenu("RenamePanelCellDetectTargets")]
        private void RenamePanelCellDetectTargets()
        {
            for (var i = 0; i < panelCellDetectTargets.Count; i++)
            {
                panelCellDetectTargets[i].name = "PanelCellDetectTarget (" + i + ")";
            }
        }
    }
}
