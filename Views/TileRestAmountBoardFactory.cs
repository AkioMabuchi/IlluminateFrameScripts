using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Instances;

namespace Views
{
    public class TileRestAmountBoardFactory : MonoBehaviour
    {
        [FormerlySerializedAs("prefabTileRestAmountBoard")] [SerializeField] private ValueDisplay prefabTileRestAmount;
        
        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;
        
        private ValueDisplay _tileRestAmount;
        public ValueDisplay TileRestAmount => _tileRestAmount;

        public void GenerateTileRestAmountBoard(FrameSize frameSize)
        {
            var position = frameSize switch
            {
                FrameSize.Small => generatePositionSmall,
                FrameSize.Medium => generatePositionMedium,
                FrameSize.Large => generatePositionLarge,
                _ => Vector3.zero
            };

            var rotation = frameSize switch
            {
                _ => Quaternion.identity
            };

            _tileRestAmount = Instantiate(prefabTileRestAmount, position, rotation, transform);
        }
    }
}
