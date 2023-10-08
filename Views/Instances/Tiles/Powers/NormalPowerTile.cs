using System.Collections.Generic;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Powers
{
    public class NormalPowerTile : TileBase, IMovableTile, IRenderableTile
    {
        private static readonly int _idleControl = Shader.PropertyToID("_IdleControl");
        private static readonly int _idleHue = Shader.PropertyToID("_IdleHue");
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _radiantEmissionControl = Shader.PropertyToID("_RadiantEmissionControl");
        private static readonly int _radiantEmissionHue = Shader.PropertyToID("_RadiantEmissionHue");
        
        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLineA;
        [SerializeField] private MeshRenderer meshRendererLineB;
        [SerializeField] private MeshRenderer meshRendererPowerSymbolFrame;
        [SerializeField] private MeshRenderer meshRendererPowerSymbolBase;
        [SerializeField] private MeshRenderer meshRendererPowerSymbol;
        
        [SerializeField] private Material materialPowerSymbol;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;
        [SerializeField] private ElectricParams electricParams;
        
        private ElectricStatus _electricStatusLineA = ElectricStatus.None;
        private ElectricStatus _electricStatusLineB = ElectricStatus.None;

        private void Start()
        {
            meshRendererPowerSymbol.material = new Material(materialPowerSymbol);
            meshRendererPowerSymbol.material.SetFloat(_idleHue, electricParams.GetHue(ElectricStatus.Normal));
            meshRendererPowerSymbol.material.SetFloat(_emissionHue, electricParams.GetHue(ElectricStatus.Normal));
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionHue,
                electricParams.GetHue(ElectricStatus.Normal));
        }
        public void SetElectricStatusLineA(ElectricStatus electricStatus)
        {
            _electricStatusLineA = electricStatus;
        }

        public void SetElectricStatusLineB(ElectricStatus electricStatus)
        {
            _electricStatusLineB = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLineA.enabled = true;
            meshRendererLineB.enabled = true;
            meshRendererPowerSymbolFrame.enabled = true;
            meshRendererPowerSymbolBase.enabled = true;
            meshRendererPowerSymbol.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLineA.enabled = false;
            meshRendererLineB.enabled = false;
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
            meshRendererLineA.material = electricMaterials.GetElectricMaterial(_electricStatusLineA);
            meshRendererLineB.material = electricMaterials.GetElectricMaterial(_electricStatusLineB);

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
                    meshRendererLineA.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineA);
                    break;
                }
                case LineDirection.Left:
                {
                    meshRendererLineB.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineB);
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

            if (hashSetElectricStatuses.Contains(_electricStatusLineA))
            {
                meshRendererLineA.material = electricMaterials.None;
            }

            if (hashSetElectricStatuses.Contains(_electricStatusLineB))
            {
                meshRendererLineB.material = electricMaterials.None;
            }

            if (hashSetElectricStatuses.Contains(ElectricStatus.Alternating))
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
                    meshRendererLineA.material = electricMaterials.GetShortedMaterial(shortedStatus);
                    break;
                }
                case LineDirection.Left:
                {
                    meshRendererLineB.material = electricMaterials.GetShortedMaterial(shortedStatus);
                    break;
                }
            }

            meshRendererPowerSymbol.material.SetFloat(_idleControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }

        public void RenderReset()
        {
            meshRendererLineA.material = electricMaterials.None;
            meshRendererLineB.material = electricMaterials.None;

            meshRendererPowerSymbol.material.SetFloat(_idleControl, 1.0f);
            meshRendererPowerSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererPowerSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }
    }
}
