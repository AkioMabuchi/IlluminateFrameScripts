using System.Collections.Generic;
using Enums;

namespace Models
{
    public class AchievementsModel
    {
        private readonly HashSet<Achievement> _hashSetEarnedAchievements = new();

        public void EarnAchievement(Achievement achievement)
        {
            _hashSetEarnedAchievements.Add(achievement);
        }

        public bool IsAchieved(Achievement achievement)
        {
            return _hashSetEarnedAchievements.Contains(achievement);
        }
    }
}