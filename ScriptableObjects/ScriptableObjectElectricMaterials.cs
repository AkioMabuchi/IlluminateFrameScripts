using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ElectricMaterials")]
    public class ScriptableObjectElectricMaterials : ScriptableObject
    {
        [SerializeField] private Material materialElectricNone;
        [SerializeField] private Material materialElectricNormal;
        [SerializeField] private Material materialElectricPlus;
        [SerializeField] private Material materialElectricMinus;
        [SerializeField] private Material materialElectricAlternating;
        [SerializeField] private Material materialElectricNormalIlluminate;
        [SerializeField] private Material materialElectricPlusIlluminate;
        [SerializeField] private Material materialElectricMinusIlluminate;
        [SerializeField] private Material materialElectricAlternatingIlluminate;
        [SerializeField] private Material materialElectricShorted;
        [SerializeField] private Material materialElectricFatal;

        public Material None => materialElectricNone;
        
        public Material GetElectricMaterial(ElectricStatus electricStatus)
        {
            return electricStatus switch
            {
                ElectricStatus.Normal => materialElectricNormal,
                ElectricStatus.Plus => materialElectricPlus,
                ElectricStatus.Minus => materialElectricMinus,
                ElectricStatus.Alternating => materialElectricAlternating,
                _ => materialElectricNone
            };
        }

        public Material GetIlluminatedMaterial(ElectricStatus electricStatus)
        {
            return electricStatus switch
            {
                ElectricStatus.Normal => materialElectricNormalIlluminate,
                ElectricStatus.Plus => materialElectricPlusIlluminate,
                ElectricStatus.Minus => materialElectricMinusIlluminate,
                ElectricStatus.Alternating => materialElectricAlternatingIlluminate,
                _ => materialElectricNone
            };
        }

        public Material GetShortedMaterial(ShortedStatus shortedStatus)
        {
            return shortedStatus switch
            {
                ShortedStatus.Shorted => materialElectricShorted,
                ShortedStatus.Fatal => materialElectricFatal,
                _ => materialElectricNone
            };
        }
    }
}
