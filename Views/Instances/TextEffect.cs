using System.Collections;
using DG.Tweening;
using Enums;
using Structs;
using TMPro;
using UnityEngine;

namespace Views.Instances
{
    public class TextEffect : MonoBehaviour
    {
        private static readonly int _shaderPropertyAlpha = Shader.PropertyToID("Alpha");
        
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private TextMeshPro textMeshPro;

        [SerializeField] private Material materialElectricNormal;
        [SerializeField] private Material materialElectricPlus;
        [SerializeField] private Material materialElectricMinus;
        [SerializeField] private Material materialElectricAlternating;
        [SerializeField] private Material materialNoPlace;
        
        private float _startPositionY;
        private float _endPositionY;
        private float _duration;
        private float _alpha;
        private Material _material;

        private IEnumerator Start()
        {
            DOTween.Sequence()
                .Append(textMeshPro.transform
                    .DOLocalMoveY(_endPositionY, _duration)
                    .From(_startPositionY)
                    .SetEase(Ease.OutQuad))
                .Join(DOTween.To(() => 1.0f, alpha => { _alpha = alpha; }, 0.0f, _duration)
                    .OnUpdate(() =>
                    {
                        _material.SetFloat(_shaderPropertyAlpha, _alpha);
                        meshRenderer.material = _material;
                    })
                    .OnComplete(() =>
                    {
                        _material = null;
                    })
                    .SetEase(Ease.InSine))
                .SetLink(gameObject);

            yield return new WaitForSeconds(_duration + 1.0f);
            Destroy(gameObject);
        }

        public void SetParams(TextEffectParamsGroup textEffectParamsGroup)
        {
            _startPositionY = textEffectParamsGroup.startPositionY;
            _endPositionY = textEffectParamsGroup.endPositionY;
            _duration = textEffectParamsGroup.duration;
            textMeshPro.text = textEffectParamsGroup.text;
            _material = textEffectParamsGroup.materialType switch
            {
                TextEffectMaterialType.ElectricNormal => new Material(materialElectricNormal),
                TextEffectMaterialType.ElectricPlus => new Material(materialElectricPlus),
                TextEffectMaterialType.ElectricMinus => new Material(materialElectricMinus),
                TextEffectMaterialType.ElectricAlternating => new Material(materialElectricAlternating),
                TextEffectMaterialType.NoPlace => new Material(materialNoPlace),
                _ => null
            };
        }
    }
}
