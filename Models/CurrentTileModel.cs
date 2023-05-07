using System;
using UniRx;

namespace Models
{
    public class CurrentTileModel
    {
        private readonly ReactiveProperty<int?> _reactivePropertyCurrentTileId = new(null);
        public IObservable<int?> OnChangedCurrentTileId => _reactivePropertyCurrentTileId;
        public int? CurrentTileId => _reactivePropertyCurrentTileId.Value;

        public void SetCurrentTileId(int tileId)
        {
            _reactivePropertyCurrentTileId.Value = tileId;
        }

        public void ResetCurrentTileId()
        {
            _reactivePropertyCurrentTileId.Value = null;
        }
    }
}