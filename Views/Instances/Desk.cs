using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Views.Instances
{
    public class Desk : MonoBehaviour
    {
        private const int QuadVertexCount = 4;
        private const int QuadAmount = 12;
    
        [SerializeField] private MeshFilter meshFilterScoreDisplay;
        [SerializeField] private MeshFilter meshFilterTileRestAmountDisplay;

        [SerializeField] private float durationDisplay;
    
        private int _scoreDisplayNumber;
        private int _currentScoreDisplayNumber;
        private int _tileRestAmountDisplayNumber;
    
        private Tweener _tweenerScoreDisplay;

        public void SetScoreDisplayNumber(int scoreDisplayNumber)
        {
            _scoreDisplayNumber = scoreDisplayNumber;
        }

        public void SetTileRestAmountDisplayNumber(int tileRestAmountDisplayNumber)
        {
            _tileRestAmountDisplayNumber = tileRestAmountDisplayNumber;
        }
    
        public void DisplayScore()
        {
            _tweenerScoreDisplay?.Kill();
            DisplayProcess(meshFilterScoreDisplay, _scoreDisplayNumber);
        }
        public void DisplayScoreTween()
        {
            _tweenerScoreDisplay?.Kill();
            _tweenerScoreDisplay = DOTween.To(() => _currentScoreDisplayNumber, value =>
                {
                    _currentScoreDisplayNumber = value;
                }, _scoreDisplayNumber, durationDisplay)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    DisplayProcess(meshFilterScoreDisplay, _currentScoreDisplayNumber);
                }).OnComplete(() =>
                {
                    _currentScoreDisplayNumber = _scoreDisplayNumber;
                })
                .SetLink(gameObject);
        }

        public void DisplayTileRestAmount()
        {
            DisplayProcess(meshFilterTileRestAmountDisplay, _tileRestAmountDisplayNumber);
        }

        private void DisplayProcess(MeshFilter meshFilter, int number)
        {
            var tmpNumber = number;
            var numbers = new List<int>
            {
                0, 0, 0, 0, 0, 0
            };

            var digit = 1;
            for (var i = 0; i < numbers.Count && tmpNumber > 0; i++)
            {
                numbers[i] = tmpNumber % 10;
                tmpNumber /= 10;
                digit = i + 1;
            }

            var meshStart = 6 - digit;

            var mesh = meshFilter.mesh;
            var uvs = mesh.uv;
            for (var i = 0; i < QuadAmount; i++)
            {
                for (var j = 0; j < QuadVertexCount; j++)
                {
                    uvs[i * QuadVertexCount + j] = Vector2.zero;
                }
            }

            for (var i = 0; i < digit; i++)
            {
                var uvx = numbers[i] * 0.1f;
                var index = (meshStart + i * 2) * QuadVertexCount;
                uvs[index] = new Vector2(uvx + 0.05f, 1.0f);
                uvs[index + 1] = new Vector2(uvx + 0.1f, 1.0f);
                uvs[index + 2] = new Vector2(uvx + 0.05f, 0.0f);
                uvs[index + 3] = new Vector2(uvx + 0.1f, 0.0f);
                uvs[index + 4] = new Vector2(uvx, 1.0f);
                uvs[index + 5] = new Vector2(uvx + 0.05f, 1.0f);
                uvs[index + 6] = new Vector2(uvx, 0.0f);
                uvs[index + 7] = new Vector2(uvx + 0.05f, 0.0f);
            }

            mesh.SetUVs(0, uvs);

            meshFilter.mesh = mesh;
        }
    }
}
