using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AchievementIcons")]
    public class AchievementIcons : ScriptableObject
    {
        [SerializeField] private Sprite spriteTutorialCompleted;
        [SerializeField] private Sprite spriteFirstSmall;
        [SerializeField] private Sprite spriteFirstMedium;
        [SerializeField] private Sprite spriteFirstLarge;
        [SerializeField] private Sprite spriteIlluminatedSmall;
        [SerializeField] private Sprite spriteIlluminatedMedium;
        [SerializeField] private Sprite spriteIlluminatedLarge;
        [SerializeField] private Sprite spriteClosedSmall;
        [SerializeField] private Sprite spriteClosedMedium;
        [SerializeField] private Sprite spriteClosedLarge;
        [SerializeField] private Sprite spriteShortedMedium;
        [SerializeField] private Sprite spriteShortedLarge;
        [SerializeField] private Sprite spriteFatalCircuit;
        [SerializeField] private Sprite spriteGreatSmall;
        [SerializeField] private Sprite spriteGreatMedium;
        [SerializeField] private Sprite spriteGreatLarge;
        [SerializeField] private Sprite spriteExcellentSmall;
        [SerializeField] private Sprite spriteExcellentMedium;
        [SerializeField] private Sprite spriteExcellentLarge;
        [SerializeField] private Sprite spriteLongLineSmall;
        [SerializeField] private Sprite spriteLongLineMedium;
        [SerializeField] private Sprite spriteLongLineLarge;
        [SerializeField] private Sprite spriteOdysseySmall;
        [SerializeField] private Sprite spriteOdysseyMedium;
        [SerializeField] private Sprite spriteOdysseyLarge;
        [SerializeField] private Sprite spriteGloomySmall;
        [SerializeField] private Sprite spriteGloomyMedium;
        [SerializeField] private Sprite spriteGloomyLarge;
        [SerializeField] private Sprite spriteRadiantSmall;
        [SerializeField] private Sprite spriteRadiantMedium;
        [SerializeField] private Sprite spriteRadiantLarge;
        [SerializeField] private Sprite spriteIlluminated100Smalls;
        [SerializeField] private Sprite spriteIlluminated100Mediums;
        [SerializeField] private Sprite spriteIlluminated100Larges;
        [SerializeField] private Sprite spritePlayed100Times;
        [SerializeField] private Sprite spritePlayed1000Times;

        public Sprite GetAchievementIcon(Achievement achievement)
        {
            return achievement switch
            {
                Achievement.TutorialCompleted => spriteTutorialCompleted,
                Achievement.FirstSmall => spriteFirstSmall,
                Achievement.FirstMedium => spriteFirstMedium,
                Achievement.FirstLarge => spriteFirstLarge,
                Achievement.IlluminatedSmall => spriteIlluminatedSmall,
                Achievement.IlluminatedMedium => spriteIlluminatedMedium,
                Achievement.IlluminatedLarge => spriteIlluminatedLarge,
                Achievement.ClosedSmall => spriteClosedSmall,
                Achievement.ClosedMedium => spriteClosedMedium,
                Achievement.ClosedLarge => spriteClosedLarge,
                Achievement.ShortedMedium => spriteShortedMedium,
                Achievement.ShortedLarge => spriteShortedLarge,
                Achievement.FatalCircuit => spriteFatalCircuit,
                Achievement.GreatSmall => spriteGreatSmall,
                Achievement.GreatMedium => spriteGreatMedium,
                Achievement.GreatLarge => spriteGreatLarge,
                Achievement.ExcellentSmall => spriteExcellentSmall,
                Achievement.ExcellentMedium => spriteExcellentMedium,
                Achievement.ExcellentLarge => spriteExcellentLarge,
                Achievement.LongLineSmall => spriteLongLineSmall,
                Achievement.LongLineMedium => spriteLongLineMedium,
                Achievement.LongLineLarge => spriteLongLineLarge,
                Achievement.OdysseySmall => spriteOdysseySmall,
                Achievement.OdysseyMedium => spriteOdysseyMedium,
                Achievement.OdysseyLarge => spriteOdysseyLarge,
                Achievement.GloomySmall => spriteGloomySmall,
                Achievement.GloomyMedium => spriteGloomyMedium,
                Achievement.GloomyLarge => spriteGloomyLarge,
                Achievement.RadiantSmall => spriteRadiantSmall,
                Achievement.RadiantMedium => spriteRadiantMedium,
                Achievement.RadiantLarge => spriteRadiantLarge,
                Achievement.Illuminated100Smalls => spriteIlluminated100Smalls,
                Achievement.Illuminated100Mediums => spriteIlluminated100Mediums,
                Achievement.Illuminated100Larges => spriteIlluminated100Larges,
                Achievement.Played100Times => spritePlayed100Times,
                Achievement.Played1000Times => spritePlayed1000Times,
                _ => null
            };
        }
    }
}