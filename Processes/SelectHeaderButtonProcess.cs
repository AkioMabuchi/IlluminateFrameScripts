using System;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using VContainer;
using Views;

namespace Processes
{
    public class SelectHeaderButtonProcess
    {
        private readonly GameStateModel _gameStateModel;
        
        private readonly SelectedHeaderButtonModel _selectedHeaderButtonModel;
        private readonly Header _header;
        private readonly Footer _footer;
        private readonly TitleScreen _titleScreen;
        private readonly SelectFrameSizeScreen _selectFrameSizeScreen;

        [Inject]
        public SelectHeaderButtonProcess(GameStateModel gameStateModel,
            SelectedHeaderButtonModel selectedHeaderButtonModel, Header header, Footer footer, TitleScreen titleScreen,
            SelectFrameSizeScreen selectFrameSizeScreen)
        {
            _gameStateModel = gameStateModel;

            _selectedHeaderButtonModel = selectedHeaderButtonModel;
            _header = header;
            _footer = footer;
            _titleScreen = titleScreen;
            _selectFrameSizeScreen = selectFrameSizeScreen;
        }

        public void SelectProcess(HeaderButtonName headerButtonName)
        {
            _selectedHeaderButtonModel.Select(headerButtonName);
            
            _header.ZoomUpButtons(_selectedHeaderButtonModel.SelectedHeaderButtonName);

            _footer.ChangeFootingText(_selectedHeaderButtonModel.SelectedHeaderButtonName switch
            {
                HeaderButtonName.Return => FooterFootingText.ReturnToTitle,
                _ => FooterFootingText.None,
            });
        }

        public void DeselectProcess(HeaderButtonName headerButtonName)
        {
            _selectedHeaderButtonModel.Deselect(headerButtonName);
            
            _header.ZoomUpButtons(_selectedHeaderButtonModel.SelectedHeaderButtonName);
            
            _footer.ChangeFootingText(_selectedHeaderButtonModel.SelectedHeaderButtonName switch
            {
                HeaderButtonName.Return => FooterFootingText.ReturnToTitle,
                _ => FooterFootingText.None,
            });
        }

        public async UniTask DecideProcess()
        {
            switch (_selectedHeaderButtonModel.SelectedHeaderButtonName)
            {
                case HeaderButtonName.Return:
                {
                    _gameStateModel.SetGameStateName(GameStateName.None);
                    _header.PullUp();
                    _footer.PullDown();
                    _selectFrameSizeScreen.FadeOut();

                    await UniTask.Delay(TimeSpan.FromSeconds(1.0));

                    _gameStateModel.SetGameStateName(GameStateName.Title);
                    _titleScreen.FadeIn();
                    _titleScreen.ChangeImageButtonTexts();
                    _titleScreen.ResizeButtons();
                    break;
                }
            }
            
            _selectedHeaderButtonModel.Clear();
        }
    }
}