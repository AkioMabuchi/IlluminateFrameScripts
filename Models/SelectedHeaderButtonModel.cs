using System.Collections.Generic;
using Enums;

namespace Models
{
    public class SelectedHeaderButtonModel
    {
        private readonly HashSet<HeaderButtonName> _hashSetSelectedHeaderButtonNames = new();

        public HeaderButtonName SelectedHeaderButtonName
        {
            get
            {
                foreach (var headerButtonName in new List<HeaderButtonName>
                         {
                             HeaderButtonName.Return
                         })
                {
                    if (_hashSetSelectedHeaderButtonNames.Contains(headerButtonName))
                    {
                        return headerButtonName;
                    }
                }

                return HeaderButtonName.None;
            }
        }

        public void Select(HeaderButtonName headerButtonName)
        {
            _hashSetSelectedHeaderButtonNames.Add(headerButtonName);
        }

        public void Deselect(HeaderButtonName headerButtonName)
        {
            _hashSetSelectedHeaderButtonNames.Remove(headerButtonName);
        }

        public void Clear()
        {
            _hashSetSelectedHeaderButtonNames.Clear();
        }
    }
}