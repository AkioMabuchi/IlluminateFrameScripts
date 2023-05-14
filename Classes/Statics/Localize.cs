using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Classes.Statics
{
    public static class Localize
    {
        private const string TableName = "LocalizationStrings";
        private static readonly Dictionary<LocaleKey, string> _dictionary = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            foreach (var obj in Enum.GetValues(typeof(LocaleKey)))
            {
                if (obj is LocaleKey localeKey)
                {
                    _dictionary.Add(localeKey, localeKey.ToString());
                }
            }
        }

        public static string LocaleString(LocaleKey localeKey)
        {
            if (_dictionary.TryGetValue(localeKey, out var str))
            {
                return LocalizationSettings.StringDatabase.GetLocalizedString(TableName, str);
            }

            return "";
        }
    }
}