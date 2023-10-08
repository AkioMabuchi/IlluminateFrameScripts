using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Terminals
{
    public class MinusTerminalTile : TileBase, IMovableTile, IRenderableTile, ITerminalTile
    {
        private static readonly int _idleControl = Shader.PropertyToID("_IdleControl");
        private static readonly int _idleHue = Shader.PropertyToID("_IdleHue");
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _additionalEmissionControl = Shader.PropertyToID("_AdditionalEmissionControl");
        private static readonly int _additionalEmissionHue = Shader.PropertyToID("_AdditionalEmissionHue");
        private static readonly int _radiantEmissionControl = Shader.PropertyToID("_RadiantEmissionControl");
        private static readonly int _radiantEmissionHue = Shader.PropertyToID("_RadiantEmissionHue");

        private static readonly int _radiantAdditionalEmissionControl =
            Shader.PropertyToID("_RadiantAdditionalEmissionControl");

        private static readonly int _radiantAdditionalEmissionHue =
            Shader.PropertyToID("_RadiantAdditionalEmissionHue");
        
        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererTerminalSymbolFrame;
        [SerializeField] private MeshRenderer meshRendererTerminalSymbolBase;
        [SerializeField] private MeshRenderer meshRendererTerminalSymbol;
        
        [SerializeField] private Material materialTerminalSymbol;

        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsLine;
        [SerializeField] private ElectricParams electricParams;
        
        [SerializeField] private float durationIlluminate;
        [SerializeField] private Ease easeIlluminate;
        [SerializeField] private float durationRadiantIlluminate;
        [SerializeField] private Ease easeRadiantIlluminate;
        
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusTerminalSymbol = ElectricStatus.None;
        
        private Sequence _sequenceIlluminate;

        private void Start()
        {
            meshRendererTerminalSymbol.material = new Material(materialTerminalSymbol);
            meshRendererTerminalSymbol.material.SetFloat(_idleHue, electricParams.GetHue(ElectricStatus.Minus));
        }

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void SetElectricStatusTerminalSymbol(ElectricStatus electricStatus)
        {
            _electricStatusTerminalSymbol = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLine.enabled = true;
            meshRendererTerminalSymbolFrame.enabled = true;
            meshRendererTerminalSymbolBase.enabled = true;
            meshRendererTerminalSymbol.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLine.enabled = false;
            meshRendererTerminalSymbolFrame.enabled = false;
            meshRendererTerminalSymbolBase.enabled = false;
            meshRendererTerminalSymbol.enabled = false;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void Render()
        {
            meshRendererLine.material = electricMaterialsLine.GetElectricMaterial(_electricStatusLine);
            meshRendererTerminalSymbol.material.SetFloat(_idleControl, _electricStatusTerminalSymbol switch
            {
                ElectricStatus.Normal => 0.0f,
                ElectricStatus.Plus => 0.0f,
                ElectricStatus.Minus => 0.0f,
                ElectricStatus.Alternating => 0.0f,
                _ => 1.0f
            });
            meshRendererTerminalSymbol.material.SetFloat(_emissionControl, _electricStatusTerminalSymbol switch
            {
                ElectricStatus.Normal => 1.0f,
                ElectricStatus.Plus => 1.0f,
                ElectricStatus.Minus => 0.0f,
                ElectricStatus.Alternating => 1.0f,
                _ => 0.0f
            });
            meshRendererTerminalSymbol.material.SetFloat(_radiantEmissionControl, _electricStatusTerminalSymbol switch
            {
                ElectricStatus.Normal => 0.0f,
                ElectricStatus.Plus => 0.0f,
                ElectricStatus.Minus => 1.0f,
                ElectricStatus.Alternating => 0.0f,
                _ => 0.0f
            });

            meshRendererTerminalSymbol.material.SetFloat(_emissionHue,
                electricParams.GetHue(_electricStatusTerminalSymbol));
            meshRendererTerminalSymbol.material.SetFloat(_additionalEmissionHue,
                electricParams.GetHue(_electricStatusTerminalSymbol));
            meshRendererTerminalSymbol.material.SetFloat(_radiantEmissionHue,
                electricParams.GetHue(_electricStatusTerminalSymbol));
            meshRendererTerminalSymbol.material.SetFloat(_radiantAdditionalEmissionHue,
                electricParams.GetHue(_electricStatusTerminalSymbol));
        }

        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            switch (lineDirectionIn)
            {
                case LineDirection.Right:
                {
                    meshRendererLine.material = electricMaterialsLine.GetIlluminatedMaterial(_electricStatusLine);
                    break;
                }
            }
        }

        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses)
        {
            var hashSetElectricStatuses = new HashSet<ElectricStatus>(electricStatuses);

            if (hashSetElectricStatuses.Contains(_electricStatusLine))
            {
                meshRendererLine.material = electricMaterialsLine.None;
            }
            
            if (hashSetElectricStatuses.Contains(_electricStatusTerminalSymbol))
            {
                meshRendererTerminalSymbol.material.SetFloat(_idleControl, 0.0f);
                meshRendererTerminalSymbol.material.SetFloat(_emissionControl, 0.0f);
                meshRendererTerminalSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
            }
        }

        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            switch (lineDirectionIn)
            {
                case LineDirection.Right:
                {
                    meshRendererLine.material = electricMaterialsLine.GetShortedMaterial(shortedStatus);
                    break;
                }
            }

            meshRendererTerminalSymbol.material.SetFloat(_idleControl, 1.0f);
            meshRendererTerminalSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererTerminalSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }
        
        public void RenderReset()
        {
            meshRendererLine.material = electricMaterialsLine.None;
            
            meshRendererTerminalSymbol.material.SetFloat(_idleControl, 1.0f);
            meshRendererTerminalSymbol.material.SetFloat(_emissionControl, 0.0f);
            meshRendererTerminalSymbol.material.SetFloat(_radiantEmissionControl, 0.0f);
        }
        
        public void IlluminateRadiantly()
        {
            _sequenceIlluminate?.Kill();
            _sequenceIlluminate = DOTween.Sequence()
                .Append(meshRendererTerminalSymbol.material
                    .DOFloat(0.0f, _radiantAdditionalEmissionControl, durationRadiantIlluminate)
                    .From(1.0f)
                    .SetEase(easeRadiantIlluminate)
                ).SetLink(gameObject);
        }

        public void Illuminate()
        {
            _sequenceIlluminate?.Kill();
            _sequenceIlluminate = DOTween.Sequence()
                .Append(meshRendererTerminalSymbol.material
                    .DOFloat(0.0f, _additionalEmissionControl, durationIlluminate)
                    .From(1.0f)
                    .SetEase(easeIlluminate)
                ).SetLink(gameObject);
        }
    }
}