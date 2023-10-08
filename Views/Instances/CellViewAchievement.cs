using Classes;
using EnhancedUI.EnhancedScroller;
using Enums;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Instances
{
    public class CellViewAchievement: EnhancedScrollerCellView
    {
        [SerializeField] private Image imageIcon;
        [SerializeField] private TextMeshProUGUI textMeshProAchievementName;
        [SerializeField] private TextMeshProUGUI textMeshProAchievementDetail;

        [SerializeField] private AchievementIcons achievementIconsAchieved;
        [SerializeField] private AchievementIcons achievementIconsUnachieved;

        [SerializeField] private Color colorAchieved;
        [SerializeField] private Color colorAchieving;
        public void SetParams(CellViewAchievementParamsGroupAchievement paramsGroupAchievement)
        {
            textMeshProAchievementName.text = paramsGroupAchievement.achievementName;
            textMeshProAchievementDetail.text = paramsGroupAchievement.achievementDetail;

            if (paramsGroupAchievement.achieved)
            {
                imageIcon.sprite = achievementIconsAchieved.GetAchievementIcon(paramsGroupAchievement.achievement);
                textMeshProAchievementName.color = colorAchieved;
                textMeshProAchievementDetail.color = colorAchieved;
            }
            else
            {
                imageIcon.sprite = achievementIconsUnachieved.GetAchievementIcon(paramsGroupAchievement.achievement);
                textMeshProAchievementName.color = colorAchieving;
                textMeshProAchievementDetail.color = colorAchieving;
            }
        }
    }
}