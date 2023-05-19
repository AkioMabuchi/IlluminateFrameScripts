using System.Collections.Generic;
using Enums;
using UniRx;

namespace Models
{
    public class SelectedTitleScreenButtonModel
    {
        private readonly HashSet<TitleScreenButtonName> _hashSetSelectedTitleScreenButtonNames = new();
        public TitleScreenButtonName SelectedTitleScreenButtonName
        {
            get
            {
                foreach (var titleButtonName in new List<TitleScreenButtonName>
                         {
                             TitleScreenButtonName.GameStart,
                             TitleScreenButtonName.Tutorial,
                             TitleScreenButtonName.Instruction,
                             TitleScreenButtonName.Settings,
                             TitleScreenButtonName.Records,
                             TitleScreenButtonName.Achievements,
                             TitleScreenButtonName.Credits,
                             TitleScreenButtonName.Quit
                         })
                {
                    if (_hashSetSelectedTitleScreenButtonNames.Contains(titleButtonName))
                    {
                        return titleButtonName;
                    }
                }

                return TitleScreenButtonName.None;
            }
        }
        public void Select(TitleScreenButtonName titleScreenButtonName)
        {
            _hashSetSelectedTitleScreenButtonNames.Add(titleScreenButtonName);
        }

        public void Deselect(TitleScreenButtonName titleScreenButtonName)
        {
            _hashSetSelectedTitleScreenButtonNames.Remove(titleScreenButtonName);
        }

        public void Clear()
        {
            _hashSetSelectedTitleScreenButtonNames.Clear();
        }
    }
}