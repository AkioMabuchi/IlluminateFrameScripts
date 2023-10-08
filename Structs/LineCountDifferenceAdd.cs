using System;
using Interfaces;

namespace Structs
{
    [Serializable]
    public struct LineCountDifferenceAdd: ILineCountDifference
    {
        public int lineCount;
    }
}