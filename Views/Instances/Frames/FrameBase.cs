using DG.Tweening;
using UnityEngine;

namespace Views.Instances.Frames
{
    public abstract class FrameBase : MonoBehaviour
    {
        [SerializeField] private Transform transformBoardOrigin;

        public Transform TransformBoardOrigin => transformBoardOrigin;

        public void Landing(float duration)
        {
            transform.DOMoveY(0.0f, duration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
    }
}
