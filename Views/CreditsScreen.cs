using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CreditsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
