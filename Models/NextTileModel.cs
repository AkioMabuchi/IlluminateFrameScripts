using System;
using UniRx;

namespace Models
{
    public class NextTileModel
    {
        private readonly ReactiveProperty<int?> _reactivePropertyNextTileId = new(null);
        public IObservable<int?> OnChangedNextTileId => _reactivePropertyNextTileId;
        public int? NextTileId => _reactivePropertyNextTileId.Value;

        public void SetNextTileTid(int tileId)
        {
            _reactivePropertyNextTileId.Value = tileId;
        }

        public void ResetNextTileId()
        {
            _reactivePropertyNextTileId.Value = null;
        }
    }
}