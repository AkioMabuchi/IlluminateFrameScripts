using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AchievementsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
