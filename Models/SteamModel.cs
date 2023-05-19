using UniRx;

namespace Models
{
    public class SteamModel
    {
        private readonly ReactiveProperty<bool> _reactivePropertyIsInitialized = new(false);
        public bool IsInitialized => _reactivePropertyIsInitialized.Value;

        public void Initialize()
        {
            _reactivePropertyIsInitialized.Value = true;
        }

        public void Shutdown()
        {
            _reactivePropertyIsInitialized.Value = false;
        }
    }
}