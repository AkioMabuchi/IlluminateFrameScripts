using System.Collections.Generic;
using Enums;

namespace Models
{
    public class SelectedRecordsScreenButtonModel
    {
        private readonly HashSet<RecordsScreenButtonName> _hashSetSelectedRecordsScreenButtonNames = new();

        public RecordsScreenButtonName SelectedRecordsScreenButtonName
        {
            get
            {
                foreach (var recordsScreenButtonName in new List<RecordsScreenButtonName>
                         {
                             RecordsScreenButtonName.Small,
                             RecordsScreenButtonName.Medium,
                             RecordsScreenButtonName.Large,
                             RecordsScreenButtonName.Global,
                             RecordsScreenButtonName.Friends
                         })
                {
                    if (_hashSetSelectedRecordsScreenButtonNames.Contains(recordsScreenButtonName))
                    {
                        return recordsScreenButtonName;
                    }
                }

                return RecordsScreenButtonName.None;
            }
        }

        public void Select(RecordsScreenButtonName recordsScreenButtonName)
        {
            _hashSetSelectedRecordsScreenButtonNames.Add(recordsScreenButtonName);
        }

        public void Deselect(RecordsScreenButtonName recordsScreenButtonName)
        {
            _hashSetSelectedRecordsScreenButtonNames.Remove(recordsScreenButtonName);
        }
    }
}