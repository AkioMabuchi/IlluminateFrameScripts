using Interfaces.Processes;
using Models;
using Steamworks;
using VContainer;

namespace Processes.QuitGame
{
    public class EditorSteamQuitGameProcess: IQuitGameProcess
    {
        private readonly SteamModel _steamModel;

        [Inject]
        public EditorSteamQuitGameProcess(SteamModel steamModel)
        {
            _steamModel = steamModel;
        }
        
        public void QuitGame()
        {
            _steamModel.Shutdown();
            SteamAPI.Shutdown();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}