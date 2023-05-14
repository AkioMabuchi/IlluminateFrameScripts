using DG.Tweening;
using UnityEngine;

namespace Views.Instances
{
    public class NextSign : MonoBehaviour
    {
        public void Landing(float duration)
        {
            transform.DOMoveY(0.0f, duration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
    }
}
