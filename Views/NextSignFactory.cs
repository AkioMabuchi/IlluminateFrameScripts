using Enums;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class NextSignFactory : MonoBehaviour
    {
        [SerializeField] private NextSign prefabNextSign;
        
        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;

        private NextSign _nextSign;
        public NextSign NextSign => _nextSign;

        public void GenerateNextSign(FrameSize frameSize)
        {
            if (_nextSign)
            {
                Destroy(_nextSign.gameObject);
            }
            
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

            _nextSign = Instantiate(prefabNextSign, position, rotation, transform);
        }
    }
}
