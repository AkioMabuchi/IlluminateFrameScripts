using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InstructionScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
