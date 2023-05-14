using Enums;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class ScoreBoardFactory : MonoBehaviour
    {
        [SerializeField] private NumberDisplayBoard prefabScoreBoard;
        
        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;
        
        private NumberDisplayBoard _scoreBoard;
        public NumberDisplayBoard ScoreBoard => _scoreBoard;

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

            _scoreBoard = Instantiate(prefabScoreBoard, position, rotation, transform);
        }
    }
}
