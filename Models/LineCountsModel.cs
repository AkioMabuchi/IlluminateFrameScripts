using System;
using Enums;
using Structs;
using UniRx;

namespace Models
{
    public class LineCountsModel
    {
        private readonly ReactiveCollection<LineCount> _reactiveCollectionLineCounts = new();

        public IObservable<CollectionAddEvent<LineCount>>
            OnAddedLineCount => _reactiveCollectionLineCounts.ObserveAdd();

        public IObservable<Unit>
            OnResetLineCounts => _reactiveCollectionLineCounts.ObserveReset();

        public int LongestLineCount
        {
            get
            {
                var result = 0;
                foreach (var lineCount in _reactiveCollectionLineCounts)
                {
                    if (lineCount.count > result)
                    {
                        result = lineCount.count;
                    }
                }

                return result;
            }
        }

        public void AddLineCount(LineStatus status, int count)
        {
            _reactiveCollectionLineCounts.Add(new LineCount
            {
                status = status,
                count = count
            });
        }

        public void Reset()
        {
            _reactiveCollectionLineCounts.Clear();
        }
    }
}