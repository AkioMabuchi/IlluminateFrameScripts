using Interfaces.TileModels;
using Models.Instances.Tiles;
using UniRx;

namespace Models
{
    public class CurrentTileModel
    {
        private readonly ReactiveProperty<int?> _reactivePropertyTileId = new(null);
        private readonly ReactiveProperty<TileModelBase> _reactivePropertyTileModel = new(null);
        public TileModelBase TileModel => _reactivePropertyTileModel.Value;

        public void SetCurrentTileId(int tileId)
        {
            _reactivePropertyTileId.Value = tileId;
        }

        public void SetCurrentTileModel(TileModelBase tileModel)
        {
            _reactivePropertyTileModel.Value = tileModel;
        }

        public bool TryGetCurrentTileId(out int tileId)
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

        public void Rotate()
        {
            if (_reactivePropertyTileModel.Value is IRotatableTileModel rotatableTileModel)
            {
                rotatableTileModel.Rotate();
            }
        }
    }
}