using UnityEngine;

namespace Views
{
    public class FrameLarge : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        
        [SerializeField] private MeshRenderer meshRendererBulbPlusTop;
        [SerializeField] private MeshRenderer meshRendererBulbPlusBottom;
        [SerializeField] private MeshRenderer meshRendererBulbMinusTop;
        [SerializeField] private MeshRenderer meshRendererBulbMinusBottom;
        [SerializeField] private MeshRenderer meshRendererBulbAlternatingLeft;
        [SerializeField] private MeshRenderer meshRendererBulbAlternatingRight;

        [SerializeField] private MeshRenderer[] meshRenderersBulbIlluminationTop;
        [SerializeField] private MeshRenderer[] meshRenderersBulbIlluminationSide;
        [SerializeField] private MeshRenderer[] meshRenderersBulbIlluminationBottom;

        [SerializeField] private Material materialBulbTerminal;
        [SerializeField] private Material materialBulbTerminalIlluminatedPlus;
        [SerializeField] private Material materialBulbTerminalIlluminatedMinus;
        [SerializeField] private Material materialBulbTerminalIlluminatedAlternating;

        [SerializeField] private Material materialBulbIllumination;
        [SerializeField] private Material materialBulbIlluminationIlluminatedTop;
        [SerializeField] private Material materialBulbIlluminationIlluminatedSide;
        [SerializeField] private Material materialBulbIlluminationIlluminatedBottom;

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
            meshRendererBulbPlusTop.material =
                isIlluminated ? materialBulbTerminalIlluminatedPlus : materialBulbTerminal;
        }

        public void IlluminateBulbPlusBottom(bool isIlluminated)
        {
            meshRendererBulbPlusBottom.material =
                isIlluminated ? materialBulbTerminalIlluminatedPlus : materialBulbTerminal;
        }

        public void IlluminateBulbMinusTop(bool isIlluminated)
        {
            meshRendererBulbMinusTop.material =
                isIlluminated ? materialBulbTerminalIlluminatedMinus : materialBulbTerminal;
        }

        public void IlluminateBulbMinusBottom(bool isIlluminated)
        {
            meshRendererBulbMinusBottom.material =
                isIlluminated ? materialBulbTerminalIlluminatedMinus : materialBulbTerminal;
        }

        public void IlluminateBulbAlternatingLeft(bool isIlluminated)
        {
            meshRendererBulbAlternatingLeft.material =
                isIlluminated ? materialBulbTerminalIlluminatedAlternating : materialBulbTerminal;
        }

        public void IlluminateBulbAlternatingRight(bool isIlluminated)
        {
            meshRendererBulbAlternatingRight.material =
                isIlluminated ? materialBulbTerminalIlluminatedAlternating : materialBulbTerminal;
        }

        public void IlluminateBulbIllumination(bool isIlluminated)
        {
            foreach (var meshRenderer in meshRenderersBulbIlluminationTop)
            {
                meshRenderer.material =
                    isIlluminated ? materialBulbIlluminationIlluminatedTop : materialBulbIllumination;
            }

            foreach (var meshRenderer in meshRenderersBulbIlluminationSide)
            {
                meshRenderer.material =
                    isIlluminated ? materialBulbIlluminationIlluminatedSide : materialBulbIllumination;
            }

            foreach (var meshRenderer in meshRenderersBulbIlluminationBottom)
            {
                meshRenderer.material =
                    isIlluminated ? materialBulbIlluminationIlluminatedBottom : materialBulbIllumination;
            }
        }
    }
}
