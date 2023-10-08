using System;
using Enums;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class Desk : MonoBehaviour
    {
        [Serializable]
        private struct Layout
        {
            public Vector3 positionValueDisplayScore;
            public Vector3 positionValueDisplayTileRestAmount;
            public Vector3 positionNextSign;
            public Vector3 positionTileInformationPlate;
        }

        [SerializeField] private DeskBody deskBody;
        
        [SerializeField] private ValueDisplay valueDisplayScore;
        [SerializeField] private ValueDisplay valueDisplayTileRestAmount;

        [SerializeField] private NextSign nextSign;
        [SerializeField] private TileInformationPlate tileInformationPlate;
        
        [SerializeField] private Layout layoutFrameSmall;
        [SerializeField] private Layout layoutFrameMedium;
        [SerializeField] private Layout layoutFrameLarge;
        public ValueDisplay ValueDisplayScore => valueDisplayScore;
        public ValueDisplay ValueDisplayTileRestAmount => valueDisplayTileRestAmount;

        public void ChangeDesk(FrameSize frameSize)
        {
            deskBody.ChangeMesh(frameSize);
            tileInformationPlate.ChangeMaterial(frameSize);
            
            Layout layout;

            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    layout = layoutFrameSmall;
                    break;
                }
                case FrameSize.Medium:
                {
                    layout = layoutFrameMedium;
                    break;
                }
                case FrameSize.Large:
                {
                    layout = layoutFrameLarge;
                    break;
                }
                default:
                {
                    return;
                }
            }

            valueDisplayScore.transform.position = layout.positionValueDisplayScore;
            valueDisplayTileRestAmount.transform.position = layout.positionValueDisplayTileRestAmount;
            nextSign.transform.position = layout.positionNextSign;
            tileInformationPlate.transform.position = layout.positionTileInformationPlate;
        }
    }
}
