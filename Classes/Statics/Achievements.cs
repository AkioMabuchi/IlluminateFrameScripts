using System.Collections.Generic;
using Enums;

namespace Classes.Statics
{
    public static class Achievements
    {
        private static readonly Dictionary<Achievement, string> _dictionary = new()
        {
            {Achievement.TutorialCompleted, "tutorialCompleted"},
            {Achievement.FirstSmall, "firstSmall"},
            {Achievement.FirstMedium, "firstMedium"},
            {Achievement.FirstLarge, "firstLarge"},
            {Achievement.IlluminatedSmall, "illuminatedSmall"},
            {Achievement.IlluminatedMedium, "illuminatedMedium"},
            {Achievement.IlluminatedLarge, "illuminatedLarge"},
            {Achievement.ClosedSmall, "closedSmall"},
            {Achievement.ClosedMedium, "closedMedium"},
            {Achievement.ClosedLarge, "closedLarge"},
            {Achievement.ShortedMedium, "shortedMedium"},
            {Achievement.ShortedLarge, "shortedLarge"},
            {Achievement.FatalCircuit, "fatalCircuit"},
            {Achievement.GreatSmall, "greatSmall"},
            {Achievement.GreatMedium, "greatMedium"},
            {Achievement.GreatLarge, "greatLarge"},
            {Achievement.ExcellentSmall, "excellentSmall"},
            {Achievement.ExcellentMedium, "excellentMedium"},
            {Achievement.ExcellentLarge, "excellentLarge"},
            {Achievement.LongLineSmall, "longLineSmall"},
            {Achievement.LongLineMedium, "longLineMedium"},
            {Achievement.LongLineLarge, "longLineLarge"},
            {Achievement.OdysseySmall, "odysseySmall"},
            {Achievement.OdysseyMedium, "odysseyMedium"},
            {Achievement.OdysseyLarge, "odysseyLarge"},
            {Achievement.GloomySmall, "gloomySmall"},
            {Achievement.GloomyMedium, "gloomyMedium"},
            {Achievement.GloomyLarge, "gloomyLarge"},
            {Achievement.RadiantSmall, "radiantSmall"},
            {Achievement.RadiantMedium, "radiantMedium"},
            {Achievement.RadiantLarge, "radiantLarge"},
            {Achievement.Illuminated100Smalls, "illuminated100Smalls"},
            {Achievement.Illuminated100Mediums, "illuminated100Mediums"},
            {Achievement.Illuminated100Larges, "illuminated100Larges"},
            {Achievement.Played100Times, "played100Times"},
            {Achievement.Played1000Times, "played1000Times"}
        };

        public static IReadOnlyDictionary<Achievement, string> Dictionary => _dictionary;
    }
}