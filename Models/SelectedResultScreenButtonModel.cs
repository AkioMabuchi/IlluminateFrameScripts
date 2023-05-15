using System.Collections.Generic;
using Enums;

namespace Models
{
    public class SelectedResultScreenButtonModel
    {
        private readonly HashSet<ResultScreenButtonName> _hashSetResultScreenButtonNames = new();

        public ResultScreenButtonName SelectedResultScreenButtonName
        {
            get
            {
                foreach (var resultScreenButtonName in new List<ResultScreenButtonName>
                         {
                             ResultScreenButtonName.Retry,
                             ResultScreenButtonName.Title,
                             ResultScreenButtonName.Records,
                             ResultScreenButtonName.Quit
                         })
                {
                    if (_hashSetResultScreenButtonNames.Contains(resultScreenButtonName))
                    {
                        return resultScreenButtonName;
                    }
                }

                return ResultScreenButtonName.None;
            }
        }

        public void Select(ResultScreenButtonName resultScreenButtonName)
        {
            _hashSetResultScreenButtonNames.Add(resultScreenButtonName);
        }

        public void Deselect(ResultScreenButtonName resultScreenButtonName)
        {
            _hashSetResultScreenButtonNames.Remove(resultScreenButtonName);
        }

        public void Clear()
        {
            _hashSetResultScreenButtonNames.Clear();
        }
    }
}