using Enums;
using ScriptableObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public class TileBulb : TileBase
    {
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererBulb;
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsLine;
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsBulb;
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusBulb = ElectricStatus.None;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    meshRendererLine.material = electricMaterialsLine.GetMaterialElectric(_electricStatusLine);
                    meshRendererBulb.material = electricMaterialsBulb.GetMaterialElectric(_electricStatusBulb);
                }).AddTo(gameObject);
        }

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void SetElectricStatusBulb(ElectricStatus electricStatus)
        {
            _electricStatusBulb = electricStatus;
        }
    }
}
