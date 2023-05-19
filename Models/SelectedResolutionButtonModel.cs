using System.Collections.Generic;

namespace Models
{
    public class SelectedResolutionButtonModel
    {
        private readonly HashSet<int> _hashSetSelectedResolutionButtonCodes = new();

        public int? SelectedResolutionButtonCode
        {
            get
            {
                for (var i = 0; i <= 4; i++)
                {
                    if (_hashSetSelectedResolutionButtonCodes.Contains(i))
                    {
                        return i;
                    }
                }

                return null;
            }
        }
        public void Select(int resolutionCode)
        {
            _hashSetSelectedResolutionButtonCodes.Add(resolutionCode);
        }

        public void Deselect(int resolutionCode)
        {
            _hashSetSelectedResolutionButtonCodes.Remove(resolutionCode);
        }

        public void Clear()
        {
            _hashSetSelectedResolutionButtonCodes.Clear();
        }
    }
}