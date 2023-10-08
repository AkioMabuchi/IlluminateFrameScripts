using System;
using Enums;

namespace Classes
{
    [Serializable]
    public class CellViewAchievementParamsGroupAchievement : CellViewAchievementParamsGroupBase
    {
        public bool achieved;
        public Achievement achievement;
        public string achievementName;
        public string achievementDetail;
    }
}