using System.Collections.Generic;
using Enums;

namespace Models
{
    public class SelectedSelectFrameSizeScreenButtonModel
    {
        private readonly HashSet<SelectFrameSizeScreenButtonName> 
            _hashSetSelectedSelectFrameSizeScreenButtonNames = new();

        public SelectFrameSizeScreenButtonName SelectFrameSizeScreenButtonName
        {
            get
            {
                foreach (var selectFrameSizeScreenButtonName in new List<SelectFrameSizeScreenButtonName>()
                         {
                             SelectFrameSizeScreenButtonName.Small,
                             SelectFrameSizeScreenButtonName.Medium,
                             SelectFrameSizeScreenButtonName.Large
                         })
                {
                    if (_hashSetSelectedSelectFrameSizeScreenButtonNames.Contains(selectFrameSizeScreenButtonName))
                    {
                        return selectFrameSizeScreenButtonName;
                    }
                }

                return SelectFrameSizeScreenButtonName.None;
            }
        }

        public void Select(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _hashSetSelectedSelectFrameSizeScreenButtonNames.Add(selectFrameSizeScreenButtonName);
        }

        public void Deselect(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _hashSetSelectedSelectFrameSizeScreenButtonNames.Remove(selectFrameSizeScreenButtonName);
        }

        public void Clear()
        {
            _hashSetSelectedSelectFrameSizeScreenButtonNames.Clear();
        }
    }
}