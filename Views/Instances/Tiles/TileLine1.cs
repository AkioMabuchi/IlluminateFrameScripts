using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public class TileLine1 : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLine;

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
            
            meshRendererLine.material = electricMaterials.GetMaterialElectric(_electricStatusLine);
        }
    }
}
