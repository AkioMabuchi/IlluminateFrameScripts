using Parameters.Enums;
using UnityEngine;

namespace Parameters.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TileLineMaterials")]
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

        public Material GetMaterialElectric(ElectricStatus electricStatus)
        {
            return new Material(electricStatus switch
            {
                ElectricStatus.Normal => materialElectricNormal,
                ElectricStatus.Plus => materialElectricPlus,
                ElectricStatus.Minus => materialElectricMinus,
                ElectricStatus.Alternating => materialElectricAlternating,
                ElectricStatus.Shorted => materialElectricShorted,
                ElectricStatus.NormalIlluminate => materialElectricNormalIlluminate,
                ElectricStatus.PlusIlluminate => materialElectricPlusIlluminate,
                ElectricStatus.MinusIlluminate => materialElectricMinusIlluminate,
                ElectricStatus.AlternatingIlluminate => materialElectricAlternatingIlluminate,
                _ => materialElectricNone
            });
        }
    }
}
