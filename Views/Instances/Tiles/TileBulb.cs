using Parameters.Enums;
using Parameters.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Instances.Tiles
{
    public class TileBulb : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererBulb;
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusBulb = ElectricStatus.None;
        
        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;

            meshRendererLine.material = electricMaterials.GetMaterialElectric(_electricStatusLine);
        }

        public void SetElectricStatusBulb(ElectricStatus electricStatus)
        {
            _electricStatusBulb = electricStatus;
        }
    }
}
