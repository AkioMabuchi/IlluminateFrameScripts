using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using Steamworks;
using UnityEngine;
using VContainer;
using Views;

namespace Processes
{
    public class GameStartProcess
    {
        private readonly GameStateModel _gameStateModel;
        private readonly SteamModel _steamModel;
        
        private readonly BlackScreen _blackScreen;
        
        private readonly ShowUpTitleScreenProcess _showUpTitleScreenProcess;
        private readonly LoadSettingsProcess _loadSettingsProcess;

        [Inject]
        public GameStartProcess(GameStateModel gameStateModel, SteamModel steamModel, BlackScreen blackScreen,
            ShowUpTitleScreenProcess showUpTitleScreenProcess, LoadSettingsProcess loadSettingsProcess)
        {
            _gameStateModel = gameStateModel;
            _steamModel = steamModel;
            
            _blackScreen = blackScreen;
            _showUpTitleScreenProcess = showUpTitleScreenProcess;
            _loadSettingsProcess = loadSettingsProcess;
        }

        public async UniTask AsyncGameStart()
        {
            if (SteamAPI.Init())
            {
                _steamModel.Initialize();
                await _loadSettingsProcess.LoadSettings();
                _blackScreen.FadeOut();
                Debug.Log(SteamUtils.GetSteamUILanguage());

                Debug.Log(SteamFriends.GetPersonaName());
                await UniTask.Delay(TimeSpan.FromSeconds(1.0));
                _showUpTitleScreenProcess.ShowUpTitleScreen();
            }
            else
            {
                _gameStateModel.SetGameStateName(GameStateName.Invalid);
                _blackScreen.ChangeMessage(BlackScreenMessageType.NoSteam);
            }
        }
    }
}