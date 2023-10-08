using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Views.Instances
{
    public class ValueDisplay : MonoBehaviour
    {
        private const int QuadVertexCount = 4;
        private const int QuadAmount = 12;

        [SerializeField] private MeshFilter meshFilterDisplay;

        [SerializeField] private float durationDisplay;

        private int _displayValue;
        private int _currentDisplayValue;

        private Tweener _tweenerDisplay;

        public void SetDisplayNumber(int displayNumber)
        {
            _displayValue = displayNumber;
        }

        public void Draw()
        {
            _tweenerDisplay?.Kill();
            _tweenerDisplay = DOTween.To(() => _currentDisplayValue, value =>
                    {
                        _currentDisplayValue = value;
                    },
                    _displayValue, durationDisplay)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    DrawProcess();
                })
                .OnComplete(() =>
                {
                    _currentDisplayValue = _displayValue;
                })
                .SetLink(gameObject);
        }

        public void DrawImmediate()
        {
            _tweenerDisplay?.Kill();
            _currentDisplayValue = _displayValue;
            DrawProcess();
        }

        private void DrawProcess()
        {
            var scoreNumber = _currentDisplayValue;
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

            meshFilterDisplay.mesh = mesh;
        }
    }
}
