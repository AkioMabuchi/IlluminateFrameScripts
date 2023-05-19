using EnhancedUI.EnhancedScroller;
using Enums;
using Structs;
using TMPro;
using UnityEngine;

namespace Views.Instances
{
    public class CellViewRecord : EnhancedScrollerCellView
    {
        [SerializeField] private TextMeshProUGUI textMeshProRank;
        [SerializeField] private TextMeshProUGUI textMeshProPlayerName;
        [SerializeField] private TextMeshProUGUI textMeshProScore;

        [SerializeField] private Color colorNormal;
        [SerializeField] private Color colorMine;
        [SerializeField] private Color colorFriend;
    
        public void SetParams(CellViewRecordParamsGroup paramsGroup)
        {
            var color = paramsGroup.status switch
            {
                CellViewRecordStatus.Normal => colorNormal,
                CellViewRecordStatus.Mine => colorMine,
                CellViewRecordStatus.Friend => colorFriend,
                _ => Color.white
            };

            textMeshProRank.color = color;
            textMeshProPlayerName.color = color;
            textMeshProScore.color = color;
        
            textMeshProRank.text = paramsGroup.rank.ToString();
            textMeshProPlayerName.text = paramsGroup.playerName;
            textMeshProScore.text = paramsGroup.score.ToString("N");
        }
    }
}
