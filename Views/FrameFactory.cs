using UnityEngine;
using Views.Instances.Frames;

namespace Views
{
    public class FrameFactory : MonoBehaviour
    {
        [SerializeField] private FrameSmall prefabFrameSmall;
        [SerializeField] private FrameMedium prefabFrameMedium;
        [SerializeField] private FrameLarge prefabFrameLarge;

        [SerializeField] private Vector3 generatePosition;

        private FrameBase _frame;
        public FrameBase Frame => _frame;

        public void DestroyFrame()
        {
            if (_frame)
            {
                Destroy(_frame.gameObject);
            }
        }
        public FrameSmall GenerateFrameSmall()
        {
            var frameSmall = Instantiate(prefabFrameSmall, generatePosition, Quaternion.identity, transform);
            _frame = frameSmall;
            return frameSmall;
        }

        public FrameMedium GenerateFrameMedium()
        {
            var frameMedium = Instantiate(prefabFrameMedium, generatePosition, Quaternion.identity, transform);
            _frame = frameMedium;
            return frameMedium;
        }

        public FrameLarge GenerateFrameLarge()
        {
            var frameLarge = Instantiate(prefabFrameLarge, generatePosition, Quaternion.identity, transform);
            _frame = frameLarge;
            return frameLarge;
        }
    }
}
