using System;
using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class TutorialGuides : MonoBehaviour
    {
        private static readonly int _alphaControl = Shader.PropertyToID("_AlphaControl");
        private static readonly int _alpha = Shader.PropertyToID("_Alpha");
        
        [SerializeField] private Material materialCellPointerA;
        [SerializeField] private MeshRenderer[] meshRenderersCellPointers;
        
        [SerializeField] private Material[] materialsTiles;
        [SerializeField] private MeshRenderer[] meshRenderersTiles;
        
        [SerializeField] private float durationCellPointersA;
        [SerializeField] private float durationTiles;
        [SerializeField] private Ease easeTiles;
        [SerializeField] private float intervalTiles;
        
        private Sequence _sequenceCellPointersA;
        private Sequence _sequenceTiles;

        private float _currentAlphaValueTiles;

        private void Start()
        {
            foreach (var meshRendererCellPointers in meshRenderersCellPointers)
            {
                meshRendererCellPointers.enabled = false;
            }
            
            foreach (var meshRendererTiles in meshRenderersTiles)
            {
                meshRendererTiles.enabled = false;
            }
        }

        public void ShowCellPointersA()
        {
            _sequenceCellPointersA?.Kill();
            _sequenceCellPointersA = DOTween.Sequence()
                .Append(materialCellPointerA.DOFloat(1.0f, _alphaControl, durationCellPointersA)
                    .SetEase(Ease.Linear)
                    .From(0.0f))
                .OnStart(() =>
                {
                    foreach (var meshRendererCellPointers in meshRenderersCellPointers)
                    {
                        meshRendererCellPointers.enabled = true;
                    }
                })
                .OnComplete(() =>
                {
                    foreach (var meshRendererCellPointers in meshRenderersCellPointers)
                    {
                        meshRendererCellPointers.enabled = false;
                    }
                }).SetLink(gameObject);
        }

        public void ShowTiles()
        {
            _sequenceTiles?.Kill();
            _sequenceTiles = DOTween.Sequence()
                .Append(DOTween.To(() => 0.0f, value =>
                    {
                        _currentAlphaValueTiles = value;
                    }, 1.0f, durationTiles)
                    .SetEase(easeTiles))
                .AppendInterval(intervalTiles)
                .Append(DOTween.To(() => 1.0f, value =>
                {
                    _currentAlphaValueTiles = value;
                }, 0.0f, durationTiles))
                .OnStart(() =>
                {
                    foreach (var meshRendererTiles in meshRenderersTiles)
                    {
                        meshRendererTiles.enabled = true;
                    }
                })
                .OnUpdate(() =>
                {
                    foreach (var materialTiles in materialsTiles)
                    {
                        materialTiles.SetFloat(_alpha, _currentAlphaValueTiles);
                    }
                })
                .OnComplete(() =>
                {
                    foreach (var meshRendererTiles in meshRenderersTiles)
                    {
                        meshRendererTiles.enabled = false;
                    }
                }).SetLink(gameObject);
        }
    }
}