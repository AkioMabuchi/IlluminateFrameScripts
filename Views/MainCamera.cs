using DG.Tweening;
using Enums;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        [SerializeField] private Vector3 positionSmall;
        [SerializeField] private Vector3 positionMedium;
        [SerializeField] private Vector3 positionLarge;
        
        [SerializeField] private float menuRotationX;
        [SerializeField] private float boardRotationX;
        [SerializeField] private float durationRotateX;

        private void Reset()
        {
            mainCamera = GetComponent<Camera>();
        }

        private void Start()
        {
            mainCamera.transform.eulerAngles = Vector3.zero;
        }

        public void Move(FrameSize frameSize)
        {
            transform.position = frameSize switch
            {
                FrameSize.Small => positionSmall,
                FrameSize.Medium => positionMedium,
                FrameSize.Large => positionLarge,
                _ => transform.position
            };
        }

        public void LookMainBoard()
        {
            mainCamera.transform.DORotate(new Vector3(boardRotationX, 0.0f, 0.0f), durationRotateX)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject);
        }

        public void LookMenu()
        {
            mainCamera.transform.DORotate(new Vector3(menuRotationX, 0.0f, 0.0f), durationRotateX)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject);
        }
    }
}
