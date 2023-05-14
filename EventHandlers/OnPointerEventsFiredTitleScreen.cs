using Enums;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredTitleScreen : IInitializable
    {
        private readonly SelectTitleScreenButtonProcess _selectTitleScreenButtonProcess;
        private readonly TitleScreen _titleScreen;

        [Inject]
        public OnPointerEventsFiredTitleScreen(SelectTitleScreenButtonProcess selectTitleScreenButtonProcess,
            TitleScreen titleScreen)
        {
            _selectTitleScreenButtonProcess = selectTitleScreenButtonProcess;
            _titleScreen = titleScreen;
        }


        public void Initialize()
        {
            _titleScreen.OnPointerEnterImageButtonStart.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.GameStart);
            });
            
            _titleScreen.OnPointerEnterImageButtonTutorial.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Tutorial);
            });
            
            _titleScreen.OnPointerEnterImageButtonInstruction.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Instruction);
            });
            
            _titleScreen.OnPointerEnterImageButtonSettings.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Settings);
            });
            
            _titleScreen.OnPointerEnterImageButtonRecords.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Records);
            });
            
            _titleScreen.OnPointerEnterImageButtonAchievements.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Achievements);
            });
            
            _titleScreen.OnPointerEnterImageButtonCredits.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Credits);
            });
            
            _titleScreen.OnPointerEnterImageButtonQuit.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.SelectProcess(TitleScreenButtonName.Quit);
            });

            _titleScreen.OnPointerExitImageButtonStart.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.GameStart);
            });
            
            _titleScreen.OnPointerExitImageButtonTutorial.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Tutorial);
            });
            
            _titleScreen.OnPointerExitImageButtonInstruction.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Instruction);
            });
            
            _titleScreen.OnPointerExitImageButtonSettings.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Settings);
            });
            
            _titleScreen.OnPointerExitImageButtonRecords.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Records);
            });
            
            _titleScreen.OnPointerExitImageButtonAchievements.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Achievements);
            });
            
            _titleScreen.OnPointerExitImageButtonCredits.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Credits);
            });
            
            _titleScreen.OnPointerExitImageButtonQuit.Subscribe(_ =>
            {
                _selectTitleScreenButtonProcess.DeselectProcess(TitleScreenButtonName.Quit);
            });
        }
    }
}