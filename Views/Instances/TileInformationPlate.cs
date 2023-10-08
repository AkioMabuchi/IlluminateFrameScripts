using Enums;
using UnityEngine;

namespace Views.Instances
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TileInformationPlate: MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private Material materialTileInformationPlateSmall;
        [SerializeField] private Material materialTileInformationPlateMedium;
        [SerializeField] private Material materialTileInformationPlateLarge;

        private void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ChangeMaterial(FrameSize frameSize)
        {
            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    meshRenderer.material = materialTileInformationPlateSmall;
                    break;
                }
                case FrameSize.Medium:
                {
                    meshRenderer.material = materialTileInformationPlateMedium;
                    break;
                }
                case FrameSize.Large:
                {
                    meshRenderer.material = materialTileInformationPlateLarge;
                    break;
                }
            }
        }
    }
}