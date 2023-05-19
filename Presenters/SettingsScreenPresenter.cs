using Models;
using UniRx;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class SettingsScreenPresenter : IInitializable
    {
        private readonly CurrentResolutionCodeModel _currentResolutionCodeModel;
        private readonly SettingsScreen _settingsScreen;

        public SettingsScreenPresenter(CurrentResolutionCodeModel currentResolutionCodeModel,
            SettingsScreen settingsScreen)
        {
            _currentResolutionCodeModel = currentResolutionCodeModel;
            _settingsScreen = settingsScreen;
        }
        
        public void Initialize()
        {
            _currentResolutionCodeModel.OnChangedCurrentResolutionCode.Subscribe(currentResolutionCode =>
            {
                _settingsScreen.SetCurrentResolutionCode(currentResolutionCode);
            });
        }
    }
}