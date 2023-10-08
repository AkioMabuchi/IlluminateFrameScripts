using DG.Tweening;
using UnityEngine;
using Views.Instances;
using Views.Instances.ImageButtons;

namespace Views.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SelectFrameSizeScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private ImageButtonSelectFrameSize imageButtonSmall;
        public ImageButtonSelectFrameSize ImageButtonSmall => imageButtonSmall;
        [SerializeField] private ImageButtonSelectFrameSize imageButtonMedium;
        public ImageButtonSelectFrameSize ImageButtonMedium => imageButtonMedium;
        [SerializeField] private ImageButtonSelectFrameSize imageButtonLarge;
        public ImageButtonSelectFrameSize ImageButtonLarge => imageButtonLarge;

        [SerializeField] private Localizer localizer;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Localize()
        {
            imageButtonSmall.ChangeTexture(localizer.CurrentLocale.SelectFrameSizeScreenButtonSmall);
            imageButtonMedium.ChangeTexture(localizer.CurrentLocale.SelectFrameSizeScreenButtonMedium);
            imageButtonLarge.ChangeTexture(localizer.CurrentLocale.SelectFrameSizeScreenButtonLarge);
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1.0f, 1.0f).SetLink(gameObject);
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0.0f, 1.0f).SetLink(gameObject);
        }
    }
}
