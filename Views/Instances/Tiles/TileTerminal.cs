using Enums;
using ScriptableObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public class TileTerminal : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererTerminalSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsLine;
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsTerminalSymbol;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusTerminalSymbol = ElectricStatus.None;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    meshRendererLine.material = electricMaterialsLine.GetMaterialElectric(_electricStatusLine);
                    meshRendererTerminalSymbol.material =
                        electricMaterialsTerminalSymbol.GetMaterialElectric(_electricStatusTerminalSymbol);
                }).AddTo(gameObject);
        }

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void SetElectricStatusTerminalSymbol(ElectricStatus electricStatus)
        {
            _electricStatusTerminalSymbol = electricStatus;
        }
    }
}
