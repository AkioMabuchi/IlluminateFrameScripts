using System.Collections.Generic;
using Models;
using Steamworks;
using UnityEngine;
using VContainer;

namespace Processes
{
    public class ChangeResolutionProcess
    {
        private readonly CurrentResolutionCodeModel _currentResolutionCodeModel;
        
        [Inject]
        public ChangeResolutionProcess(CurrentResolutionCodeModel currentResolutionCodeModel)
        {
            _currentResolutionCodeModel = currentResolutionCodeModel;
        }

        public void ChangeResolution(int resolutionCode)
        {
            if (_currentResolutionCodeModel.CurrentResolutionCode == resolutionCode)
            {
                return;
            }

            _currentResolutionCodeModel.SetCurrentResolutionCode(resolutionCode);
            
            SteamUserStats.SetStat("settingsResolution", resolutionCode);
            SteamUserStats.StoreStats();

            var resolution = _currentResolutionCodeModel.Resolution;

            Screen.SetResolution(resolution.x, resolution.y, true);
        }
    }
}