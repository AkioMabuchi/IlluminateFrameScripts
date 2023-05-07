using Parameters.Enums;
using Parameters.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Instances.Tiles
{
    public class TileTerminal : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererTerminalSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusTerminalSymbol = ElectricStatus.None;
        
        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;

            meshRendererLine.material = electricMaterials.GetMaterialElectric(_electricStatusLine);
        }

        public void SetElectricStatusTerminalSymbol(ElectricStatus electricStatus)
        {
            _electricStatusTerminalSymbol = electricStatus;

            meshRendererTerminalSymbol.material = electricMaterials.GetMaterialElectric(_electricStatusTerminalSymbol);
        }
    }
}
