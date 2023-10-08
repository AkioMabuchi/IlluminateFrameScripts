using System.Collections.Generic;
using Classes;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Views.Instances
{
    [RequireComponent(typeof(EnhancedScroller))]
    public class EnhancedScrollerAchievements: MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhancedScroller;

        [SerializeField] private CellViewAchievement prefabCellViewAchievement;

        [SerializeField] private float cellViewAchievementHeight;
        
        private readonly List<CellViewAchievementParamsGroupAchievement> _achievementParamsGroups = new();

        private void Reset()
        {
            enhancedScroller = GetComponent<EnhancedScroller>();
        }

        private void Awake()
        {
            enhancedScroller.Delegate = this;
        }

        public void ClearAchievements()
        {
            _achievementParamsGroups.Clear();
        }

        public void SetAchievements(IEnumerable<CellViewAchievementParamsGroupAchievement> achievements)
        {
            foreach (var achievement in achievements)
            {
                _achievementParamsGroups.Add(achievement);
            }
        }

        public void Reload()
        {
            enhancedScroller.ReloadData();
        }
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _achievementParamsGroups.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return cellViewAchievementHeight;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = scroller.GetCellView(prefabCellViewAchievement);
            switch (cellView)
            {
                case CellViewAchievement cellViewAchievement:
                {
                    cellViewAchievement.SetParams(_achievementParamsGroups[dataIndex]);
                    break;
                }
            }

            return cellView;
        }
    }
}