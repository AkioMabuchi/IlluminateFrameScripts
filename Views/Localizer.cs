using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField] private Locale localeEnglish;
        [SerializeField] private Locale localeJapanese;
        
        private LanguageName _languageName = LanguageName.English;

        public Locale CurrentLocale
        {
            get
            {
                return _languageName switch
                {
                    LanguageName.English => localeEnglish,
                    LanguageName.Japanese => localeJapanese,
                    _ => localeEnglish
                };
            }
        }
        public void SetLanguage(LanguageName languageName)
        {
            _languageName = languageName;
        }
    }
}