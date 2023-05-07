using Parameters.Enums;
using Parameters.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Instances.Tiles
{
    public class TileDistributor4 : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLineA;
        [SerializeField] private MeshRenderer meshRendererLineB;
        [SerializeField] private MeshRenderer meshRendererLineC;
        [SerializeField] private MeshRenderer meshRendererLineD;
        [SerializeField] private MeshRenderer meshRendererLineCore;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLineA = ElectricStatus.None;
        private ElectricStatus _electricStatusLineB = ElectricStatus.None;
        private ElectricStatus _electricStatusLineC = ElectricStatus.None;
        private ElectricStatus _electricStatusLineD = ElectricStatus.None;
        private ElectricStatus _electricStatusCore = ElectricStatus.None;
        
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

        public void SetElectricStatusLineC(ElectricStatus electricStatus)
        {
            _electricStatusLineC = electricStatus;
            
            meshRendererLineC.material = electricMaterials.GetMaterialElectric(_electricStatusLineC);
        }

        public void SetElectricStatusLineD(ElectricStatus electricStatus)
        {
            _electricStatusLineD = electricStatus;
            
            meshRendererLineD.material = electricMaterials.GetMaterialElectric(_electricStatusLineD);
        }

        public void SetElectricStatusCore(ElectricStatus electricStatus)
        {
            _electricStatusCore = electricStatus;

            meshRendererLineCore.material = electricMaterials.GetMaterialElectric(_electricStatusCore);
        }
    }
}
