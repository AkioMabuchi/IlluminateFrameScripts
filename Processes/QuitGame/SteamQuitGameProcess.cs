using Interfaces.Processes;
using Models;
using Steamworks;
using UnityEngine;
using VContainer;

namespace Processes.QuitGame
{
    public class SteamQuitGameProcess : IQuitGameProcess
    {
        private readonly SteamModel _steamModel;

        [Inject]
        public SteamQuitGameProcess(SteamModel steamModel)
        {
            _steamModel = steamModel;
        }

        public void QuitGame()
        {
            _steamModel.Shutdown();
            SteamAPI.Shutdown();
            Application.Quit();
        }
    }
}