using System;
using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    public class FrameMedium : MonoBehaviour
    {
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        
        [SerializeField] private GameObject body;

        [SerializeField] private MeshRenderer meshRendererBulbPlusTop;
        [SerializeField] private MeshRenderer meshRendererBulbPlusBottom;
        [SerializeField] private MeshRenderer meshRendererBulbMinusTop;
        [SerializeField] private MeshRenderer meshRendererBulbMinusBottom;

        [SerializeField] private MeshRenderer[] meshRenderersBulbIllumination;

        [SerializeField] private Material materialBulbPlus;
        [SerializeField] private Material materialBulbMinus;
        [SerializeField] private Material materialIllumination;


        private void Start()
        {
            meshRendererBulbPlusTop.material = new Material(materialBulbPlus);
            meshRendererBulbPlusBottom.material = new Material(materialBulbPlus);
            meshRendererBulbMinusTop.material = new Material(materialBulbMinus);
            meshRendererBulbMinusBottom.material = new Material(materialBulbMinus);

            meshRendererBulbPlusTop.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbPlusBottom.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbMinusTop.material.SetFloat(_emissionControl, 0.0f);
            meshRendererBulbMinusBottom.material.SetFloat(_emissionControl, 0.0f);

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

        public void IlluminateBulbPlusTop(bool isIlluminated)
        {
            meshRendererBulbPlusTop.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }

        public void IlluminateBulbPlusBottom(bool isIlluminated)
        {
            meshRendererBulbPlusBottom.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }

        public void IlluminateBulbMinusTop(bool isIlluminated)
        {
            meshRendererBulbMinusTop.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }

        public void IlluminateBulbMinusBottom(bool isIlluminated)
        {
            meshRendererBulbMinusBottom.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
        }

        public void IlluminateBulbIllumination(bool isIlluminated)
        {
            foreach (var meshRendererIllumination in meshRenderersBulbIllumination)
            {
                meshRendererIllumination.material.SetFloat(_emissionControl, isIlluminated ? 1.0f : 0.0f);
            }
        }
    }
}
