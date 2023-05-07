using Parameters.Enums;
using Parameters.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Instances.Tiles
{
    public class TileLine2 : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLineA;
        [SerializeField] private MeshRenderer meshRendererLineB;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;

        private ElectricStatus _electricStatusLineA = ElectricStatus.None;
        private ElectricStatus _electricStatusLineB = ElectricStatus.None;

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
    }
}
