using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;

namespace Views.Instances
{
    public class CellViewNoAchievement : EnhancedScrollerCellView
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public void Localize(string text)
        {
            textMeshPro.text = text;
        }
    }
}