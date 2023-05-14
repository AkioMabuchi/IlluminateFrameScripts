using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public class TilePower2 : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLineA;
        [SerializeField] private MeshRenderer meshRendererLineB;
        [SerializeField] private MeshRenderer meshRendererPowerSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLineA = ElectricStatus.None;
        private ElectricStatus _electricStatusLineB = ElectricStatus.None;
        private ElectricStatus _electricStatusPowerSymbol = ElectricStatus.None;
        
        public void SetElectricStatusLineA(ElectricStatus electricStatus)
        {
            _electricStatusLineA = electricStatus;

            meshRendererLineA.material = electricMaterials.GetMaterialElectric(_electricStatusLineA);
        }

        public void SetElectricStatusLineB(ElectricStatus electricStatus)
        {
            _electricStatusLineB = electricStatus;

            meshRendererLineB.material = electricMaterials.GetMaterialElectric(_electricStatusLineB);
        }
        
        public void SetElectricStatusPowerSymbol(ElectricStatus electricStatus)
        {
            _electricStatusPowerSymbol = electricStatus;
        }
    }
}
