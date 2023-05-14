using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public class TilePower1 : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererPowerSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusPowerSymbol = ElectricStatus.None;
        
        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;

            meshRendererLine.material = electricMaterials.GetMaterialElectric(_electricStatusLine);
        }

        public void SetElectricStatusPowerSymbol(ElectricStatus electricStatus)
        {
            _electricStatusPowerSymbol = electricStatus;
        }
    }
}
