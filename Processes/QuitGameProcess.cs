using Models;
using Steamworks;

namespace Processes
{
    public class QuitGameProcess
    {
        private readonly SteamModel _steamModel;

        public QuitGameProcess(SteamModel steamModel)
        {
            _steamModel = steamModel;
        }
        public void QuitGame()
        {
            _steamModel.Shutdown();
            SteamAPI.Shutdown();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}