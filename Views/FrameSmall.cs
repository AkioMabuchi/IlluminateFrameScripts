using System;
using UnityEngine;

namespace Views
{
    public class FrameSmall : MonoBehaviour
    {
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        
        [SerializeField] private GameObject body;
        
        [SerializeField] private MeshRenderer meshRendererBulbTerminalA;
        [SerializeField] private MeshRenderer meshRendererBulbTerminalB;
        [SerializeField] private MeshRenderer meshRendererBulbTerminalC;
        [SerializeField] private MeshRenderer meshRendererBulbTerminalD;

        [SerializeField] private MeshRenderer[] meshRenderersBulbIllumination;

        [SerializeField] private Material materialBulb;
        [SerializeField] private Material materialIllumination;

        private void Start()
        {
            meshRendererBulbTerminalA.material = new Material(materialBulb);
            meshRendererBulbTerminalB.material = new Material(materialBulb);
            meshRendererBulbTerminalC.material = new Material(materialBulb);
            meshRendererBulbTerminalD.material = new Material(materialBulb);

            meshRendererBulbTerminalA.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbTerminalB.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbTerminalC.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbTerminalD.material.SetFloat(_emissionControl, 0.0f);
            
            foreach (var meshRendererIllumination in meshRenderersBulbIllumination)
            {
                meshRendererIllumination.material = new Material(materialIllumination);
                meshRendererIllumination.material.SetFloat(_emissionControl, 0.0f);
            }
        }

        public void Show()
        {
            body.SetActive(true);
        }

        public void Hide()
        {
            body.SetActive(false);
        }
        public void IlluminateBulbTerminalA(bool isIlluminated)
        {
            meshRendererBulbTerminalA.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }
        
        public void IlluminateBulbTerminalB(bool isIlluminated)
        {
            meshRendererBulbTerminalB.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }
        
        public void IlluminateBulbTerminalC(bool isIlluminated)
        {
            meshRendererBulbTerminalC.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }
        
        public void IlluminateBulbTerminalD(bool isIlluminated)
        {
            meshRendererBulbTerminalD.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }

        public void IlluminateBulbsIllumination(bool isIlluminated)
        {
            foreach (var meshRendererIllumination in meshRenderersBulbIllumination)
            {
                meshRendererIllumination.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
            }
        }
    }
}
