using Enums;
using Enums.ScreenButtonNames.Prior;
using Models.ScreenButtons.Prior;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens.Prior;

namespace Processes.Events.Screens.Prior
{
    public class HeaderEventsProcess : IInitializable
    {
        private readonly SelectedHeaderButtonModel _selectedHeaderButtonModel;
        private readonly Header _header;
        private readonly Footer _footer;

        [Inject]
        public HeaderEventsProcess(SelectedHeaderButtonModel selectedHeaderButtonModel, Header header, Footer footer)
        {
            _selectedHeaderButtonModel = selectedHeaderButtonModel;
            _header = header;
            _footer = footer;
        }

        public void Initialize()
        {
            _header.ImageButtonReturn.OnPointerEnter.Subscribe(_ =>
            {
                _selectedHeaderButtonModel.Select(HeaderButtonName.Return);
                _header.ImageButtonReturn.ZoomUp();
                _footer.RenderText();
            });

            _header.ImageButtonReturn.OnPointerExit.Subscribe(_ =>
            {
                _selectedHeaderButtonModel.Deselect();
                _header.ImageButtonReturn.ZoomDown();
                _footer.RenderText();
            });
        }
    }
}