using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views.Screens.Prior;

namespace Presenters.Screens.Prior
{
    public class HeaderPresenter : IInitializable
    {
        private readonly GameStateModel _gameStateModel;

        private Header _header;
        
        [Inject]
        public HeaderPresenter(GameStateModel gameStateModel, Header header)
        {
            _gameStateModel = gameStateModel;

            _header = header;
        }
        
        public void Initialize()
        {
            _gameStateModel.OnChangedGameStateName.Subscribe(gameStateName =>
            {
                _header.SetGameStateName(gameStateName);
            });
        }
    }
}