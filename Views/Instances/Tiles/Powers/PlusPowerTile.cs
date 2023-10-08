using System.Collections.Generic;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Powers
{
    public class PlusPowerTile : TileBase, IMovableTile, IRenderableTile
    {
        private static readonly int _idleControl = Shader.PropertyToID("_IdleControl");
        private static readonly int _idleHue = Shader.PropertyToID("_IdleHue");
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _radiantEmissionControl = Shader.PropertyToID("_RadiantEmissionControl");
        private static readonly int _radiantEmissionHue = Shader.PropertyToID("_RadiantEmissionHue");
        
        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererPowerSymbolFrame;
        [SerializeField] private MeshRenderer meshRendererPowerSymbolBase;
        [SerializeField] private MeshRenderer meshRendererPowerSymbol;
        
        [SerializeField] private Material materialPowerSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        [SerializeField] private ElectricParams electricParams;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;

        private void Start()
        {
            meshRendererPowerSymbol.material = new Material(materialPowerSymbol);
            meshRendererPowerSymbol.material.SetFloat(_idleHue, electricParams.GetHue(ElectricStatus.Plus));
            meshRendererPowerSymbol.material.SetFloat(_emissionHue, electricParams.GetHue(ElectricStatus.Plus));
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionHue, electricParams.GetHue(ElectricStatus.Plus));
        }
        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLine.enabled = true;
            meshRendererPowerSymbolFrame.enabled = true;
            meshRendererPowerSymbolBase.enabled = true;
            meshRendererPowerSymbol.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLine.enabled = false;
            meshRendererPowerSymbolFrame.enabled = false;
            meshRendererPowerSymbolBase.enabled = false;
            meshRendererPowerSymbol.enabled = false;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }
        
        public void Render()
        {
            meshRendererLine.material = electricMaterials.GetElectricMaterial(_electricStatusLine);
            
            meshRendererPowerSymbol.material.SetFloat(_idleControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 1.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }

        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            switch (lineDirectionOut)
            {
                case LineDirection.Right:
                {
                    meshRendererLine.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLine);
                    break;
                }
            }

            meshRendererPowerSymbol.material.SetFloat(_idleControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 1.0f);
        }

        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses)
        {
            var hashSetElectricStatuses = new HashSet<ElectricStatus>(electricStatuses);

            if (hashSetElectricStatuses.Contains(_electricStatusLine))
            {
                meshRendererLine.material = electricMaterials.None;
            }

            if (hashSetElectricStatuses.Contains(ElectricStatus.Plus))
            {
                meshRendererPowerSymbol.material.SetFloat(_idleControl, 1.0f);
                meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
                meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
            }
        }

        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            switch (lineDirectionOut)
            {
                case LineDirection.Right:
                {
                    meshRendererLine.material = electricMaterials.GetShortedMaterial(shortedStatus);
                    break;
                }
            }

            meshRendererPowerSymbol.material.SetFloat(_idleControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }

        public void RenderReset()
        {
            meshRendererLine.material = electricMaterials.None;

            meshRendererPowerSymbol.material.SetFloat(_idleControl, 1.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }
    }
}
