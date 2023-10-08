using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Instances;

namespace Views
{
    public class ScoreBoardFactory : MonoBehaviour
    {
        [FormerlySerializedAs("prefabScoreBoard")] [SerializeField] private ValueDisplay prefabScore;
        
        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;
        
        private ValueDisplay _score;
        public ValueDisplay Score => _score;

        public void GenerateScoreBoard(FrameSize frameSize)
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

            _score = Instantiate(prefabScore, position, rotation, transform);
        }
    }
}
