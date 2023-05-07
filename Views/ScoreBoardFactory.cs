using System;
using System.Collections.Generic;
using Parameters.Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class ScoreBoardFactory : MonoBehaviour
    {
        [SerializeField] private NumberDisplayBoard prefabScoreBoard;

        private NumberDisplayBoard _scoreBoard;
        
        public void SetScoreBoardDisplayMode(NumberDisplayMode displayMode)
        {
            if (_scoreBoard)
            {
                _scoreBoard.SetDisplayMode(displayMode);
            }
        }

        public void SetScoreBoardScore(int score)
        {
            if (_scoreBoard)
            {
                _scoreBoard.SetDisplayNumber(score);
            }
        }
        
        public void GenerateScoreBoard(PanelSize panelSize)
        {
            var position = panelSize switch
            {
                PanelSize.Large => new Vector3(1.05f, 0.0f, 0.5f),
                _ => Vector3.zero
            };

            var rotation = panelSize switch
            {
                _ => Quaternion.identity
            };

            _scoreBoard = Instantiate(prefabScoreBoard, position, rotation, transform);
        }
    }
}
