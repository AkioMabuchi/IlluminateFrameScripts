using Models;
using Models.ScreenButtons;
using Models.ScreenButtons.Prior;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views.Screens.Prior;

namespace Presenters.Screens.Prior
{
    public class FooterPresenter : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly SelectedHeaderButtonModel _selectedHeaderButtonModel;
        private readonly SelectedSelectFrameSizeScreenButtonModel _selectedSelectFrameSizeScreenButtonModel;

        private readonly Footer _footer;

        [Inject]
        public FooterPresenter(GameStateModel gameStateModel, SelectedHeaderButtonModel selectedHeaderButtonModel,
            SelectedSelectFrameSizeScreenButtonModel selectedSelectFrameSizeScreenButtonModel, Footer footer)
        {
            _gameStateModel = gameStateModel;
            _selectedHeaderButtonModel = selectedHeaderButtonModel;
            _selectedSelectFrameSizeScreenButtonModel = selectedSelectFrameSizeScreenButtonModel;

            _footer = footer;
        }

        public void Initialize()
        {
            _gameStateModel.OnChangedGameStateName.Subscribe(gameStateName =>
            {
                _footer.SetGameStateName(gameStateName);
            });

            _selectedHeaderButtonModel.OnChangedSelectedHeaderButtonName.Subscribe(headerButtonName =>
            {
                _footer.SetHeaderButtonName(headerButtonName);
            });

            _selectedSelectFrameSizeScreenButtonModel.OnChangedSelectedSelectFrameSizeScreenButton.Subscribe(
                selectFrameSizeScreenButtonName =>
                {
                    _footer.SetSelectFrameSizeScreenButtonName(selectFrameSizeScreenButtonName);
                });
        }
    }
}