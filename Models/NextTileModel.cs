using Models.Instances.Tiles;
using UniRx;

namespace Models
{
    public class NextTileModel
    {
        private readonly ReactiveProperty<int?> _reactivePropertyTileId = new(null);
        
        private readonly ReactiveProperty<TileModelBase> _reactivePropertyTileModel = new(null);
        public TileModelBase TileModel => _reactivePropertyTileModel.Value;

        public void SetTileId(int tileId)
        {
            _reactivePropertyTileId.Value = tileId;
        }

        public void SetTileModel(TileModelBase tileModelBase)
        {
            _reactivePropertyTileModel.Value = tileModelBase;
        }

        public bool TryGetTileId(out int tileId)
        {
            if (_reactivePropertyTileId.Value.HasValue)
            {
                tileId = _reactivePropertyTileId.Value.Value;
                return true;
            }

            tileId = default;
            return false;
        }

        public void Reset()
        {
            _reactivePropertyTileId.Value = null;
            _reactivePropertyTileModel.Value = null;
        }
    }
}