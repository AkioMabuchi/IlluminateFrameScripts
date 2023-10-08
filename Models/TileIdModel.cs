using UniRx;

namespace Models
{
    public class TileIdModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyTileId = new(0);

        public void Initialize()
        {
            _reactivePropertyTileId.Value = 0;
        }

        public int GetTileId()
        {
            return _reactivePropertyTileId.Value++;
        }
    }
}