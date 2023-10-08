using Models;
using Steamworks;
using VContainer;
using VContainer.Unity;

namespace Processes.Updates
{
    public class SteamUpdateProcess: ITickable
    {
        private readonly SteamModel _steamModel;

        [Inject]
        public SteamUpdateProcess(SteamModel steamModel)
        {
            _steamModel = steamModel;
        }
        
        public void Tick()
        {
            if (_steamModel.IsInitialized)
            {
                SteamAPI.RunCallbacks();
            }
        }
    }
}