using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Views.Instances
{
    public class NumberDisplayBoard : MonoBehaviour
    {
        private const int QuadVertexCount = 4;
        private const int QuadAmount = 12;

        [SerializeField] private MeshFilter meshFilterDisplay;
        
        [SerializeField] private float durationDisplay;
        
        private NumberDisplayMode _displayMode = NumberDisplayMode.None;
        private int _displayNumber;
        private int _currentDisplayNumber;

        private Tweener _tweenerDisplay;

        public void SetDisplayMode(NumberDisplayMode displayMode)
        {
            _displayMode = displayMode;
        }
        
        public void SetDisplayNumber(int displayNumber)
        {
            _displayNumber = displayNumber;
        }
        
        public void Landing(float duration)
        {
            transform.DOMoveY(0.0f, duration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        public void Draw()
        {
            _tweenerDisplay?.Kill();
            _tweenerDisplay = DOTween.To(() => _currentDisplayNumber, value =>
                {
                    _currentDisplayNumber = value;
                }, _displayNumber, durationDisplay)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    DrawProcess();
                }).OnComplete(() =>
                {
                    _currentDisplayNumber = _displayNumber;
                })
                .SetLink(gameObject);
        }

        public void DrawImmediate()
        {
            _tweenerDisplay?.Kill();
            _currentDisplayNumber = _displayNumber;
            DrawProcess();
        }

        private void DrawProcess()
        {
            var scoreNumber = _currentDisplayNumber;
            var numbers = new List<int>
            {
                0, 0, 0, 0, 0, 0
            };

            var digit = 1;
            for (var i = 0; i < numbers.Count && scoreNumber > 0; i++)
            {
                numbers[i] = scoreNumber % 10;
                scoreNumber /= 10;
                digit = i + 1;
            }

            var meshStart = 6 - digit;

            var mesh = meshFilterDisplay.mesh;
            var uvs = mesh.uv;
            for (var i = 0; i < QuadAmount; i++)
            {
                for (var j = 0; j < QuadVertexCount; j++)
                {
                    uvs[i * QuadVertexCount + j] = Vector2.zero;
                }
            }

            switch (_displayMode)
            {
                case NumberDisplayMode.Show:
                {
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

                    break;
                }
            }

            mesh.SetUVs(0, uvs);

            meshFilterDisplay.mesh = mesh;
        }
    }
}
