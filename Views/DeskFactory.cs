using Enums;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class DeskFactory : MonoBehaviour
    {
        [SerializeField] private Desk prefabDeskSmall;
        [SerializeField] private Desk prefabDeskMedium;
        [SerializeField] private Desk prefabDeskLarge;

        private Desk _desk;
        public Desk Desk => _desk;

        public void GenerateDesk(FrameSize frameSize)
        {
            if (_desk)
            {
                Destroy(_desk.gameObject);
            }

            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    _desk = Instantiate(prefabDeskSmall, transform);
                    break;
                }
                case FrameSize.Medium:
                {
                    _desk = Instantiate(prefabDeskMedium, transform);
                    break;
                }
                case FrameSize.Large:
                {
                    _desk = Instantiate(prefabDeskLarge, transform);
                    break;
                }
            }
        }

        public void SetScoreDisplayNumber(int scoreDisplayNumber)
        {
            if (_desk)
            {
                _desk.SetScoreDisplayNumber(scoreDisplayNumber);
            }
        }

        public void SetTileRestAmountDisplayNumber(int tileRestAmountDisplayNumber)
        {
            if (_desk)
            {
                _desk.SetTileRestAmountDisplayNumber(tileRestAmountDisplayNumber);
            }
        }
    }
}
