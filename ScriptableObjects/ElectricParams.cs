using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ElectricParams")]
    public class ElectricParams : ScriptableObject
    {
        [SerializeField] private float hueNormal;
        [SerializeField] private float huePlus;
        [SerializeField] private float hueMinus;
        [SerializeField] private float hueAlternating;
        [SerializeField] private float hueMixed;
        public float GetHue(ElectricStatus electricStatus)
        {
            switch (electricStatus)
            {
                case ElectricStatus.Normal:
                {
                    return hueNormal;
                }
                case ElectricStatus.Plus:
                {
                    return huePlus;
                }
                case ElectricStatus.Minus:
                {
                    return hueMinus;
                }
                case ElectricStatus.Alternating:
                {
                    return hueAlternating;
                }
            }

            return 0.0f;
        }

        public float GetHue(TextEffectMaterialType textEffectMaterialType)
        {
            switch (textEffectMaterialType)
            {
                case TextEffectMaterialType.ElectricNormal:
                {
                    return hueNormal;
                }
                case TextEffectMaterialType.ElectricPlus:
                {
                    return huePlus;
                }
                case TextEffectMaterialType.ElectricMinus:
                {
                    return hueMinus;
                }
                case TextEffectMaterialType.ElectricAlternating:
                {
                    return hueAlternating;
                }
                case TextEffectMaterialType.ElectricMixed:
                {
                    return hueMixed;
                }
            }

            return 0.0f;
        }
    }
}