using System.Collections.Generic;
using Classes;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Views.Instances
{
    [RequireComponent(typeof(EnhancedScroller))]
    public class EnhancedScrollerResultAchievements : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhancedScroller;

        [SerializeField] private CellViewAchievement prefabCellViewAchievement;
        [SerializeField] private CellViewNoAchievement prefabCellViewNoAchievement;

        [SerializeField] private Localizer localizer;
        
        [SerializeField] private float cellViewAchievementHeight;
        [SerializeField] private float cellViewNoAchievementHeight;
        
        private readonly List<CellViewAchievementParamsGroupBase> _achievementParamsGroups = new();
        
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

        public void SetNoAchievement()
        {
            _achievementParamsGroups.Add(new CellViewAchievementParamsGroupNoAchievement());
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
            return _achievementParamsGroups[dataIndex] switch
            {
                CellViewAchievementParamsGroupAchievement => cellViewAchievementHeight,
                CellViewAchievementParamsGroupNoAchievement => cellViewNoAchievementHeight,
                _ => 0.0f
            };
        }
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            EnhancedScrollerCellView cellView = null;

            switch (_achievementParamsGroups[dataIndex])
            {
                case CellViewAchievementParamsGroupAchievement paramsGroupAchievement:
                {
                    cellView = scroller.GetCellView(prefabCellViewAchievement);
                    if (cellView is CellViewAchievement cellViewAchievement)
                    {
                        cellViewAchievement.SetParams(paramsGroupAchievement);
                    }
                    break;
                }
                case CellViewAchievementParamsGroupNoAchievement:
                {
                    cellView = scroller.GetCellView(prefabCellViewNoAchievement);
                    if (cellView is CellViewNoAchievement cellViewNoAchievement)
                    {
                        cellViewNoAchievement.Localize(localizer.CurrentLocale.ResultScreenNoAchievement);
                    }
                    break;
                }
            }

            return cellView;
        }
    }
}