using System;
using Enums;
using Enums.ScreenButtonNames;
using Models;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;

namespace Processes.Events.Screens
{
    public class TitleScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedTitleScreenButtonModel _selectedTitleScreenButtonModel;
        
        private readonly TitleScreen _titleScreen;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public TitleScreenEventProcess(SelectedTitleScreenButtonModel selectedTitleScreenButtonModel,
            TitleScreen titleScreen)
        {
            _selectedTitleScreenButtonModel = selectedTitleScreenButtonModel;
            _titleScreen = titleScreen;
        }

        public void Initialize()
        {
            _titleScreen.ImageButtonGameStart.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.GameStart);
                _titleScreen.ImageButtonGameStart.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonTutorial.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Tutorial);
                _titleScreen.ImageButtonTutorial.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonInstruction.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Instruction);
                _titleScreen.ImageButtonInstruction.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonSettings.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Settings);
                _titleScreen.ImageButtonSettings.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonRecords.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Records);
                _titleScreen.ImageButtonRecords.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonAchievements.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Achievements);
                _titleScreen.ImageButtonAchievements.ZoomUp();
            }).AddTo(_compositeDisposable);

            _titleScreen.ImageButtonStatistics.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Statistics);
                _titleScreen.ImageButtonStatistics.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonQuit.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Quit);
                _titleScreen.ImageButtonQuit.ZoomUp();
            }).AddTo(_compositeDisposable);

            _titleScreen.ImageButtonCredits.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Credits);
                _titleScreen.ImageButtonCredits.ZoomUp();
            }).AddTo(_compositeDisposable);

            _titleScreen.ImageButtonLicenses.OnPointerEnter.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Select(TitleScreenButtonName.Licences);
                _titleScreen.ImageButtonLicenses.ZoomUp();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonGameStart.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonGameStart.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonTutorial.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonTutorial.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonInstruction.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonInstruction.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonSettings.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonSettings.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonRecords.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonRecords.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonAchievements.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonAchievements.ZoomDown();
            }).AddTo(_compositeDisposable);

            _titleScreen.ImageButtonStatistics.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonStatistics.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonQuit.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonQuit.ZoomDown();
            }).AddTo(_compositeDisposable);
            
            _titleScreen.ImageButtonCredits.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonCredits.ZoomDown();
            }).AddTo(_compositeDisposable);

            _titleScreen.ImageButtonLicenses.OnPointerExit.Subscribe(_ =>
            {
                _selectedTitleScreenButtonModel.Deselect();
                _titleScreen.ImageButtonLicenses.ZoomDown();
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}