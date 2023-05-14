using Enums;
using Processes;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnPointerEventsFiredHeader : IInitializable
    {
        private readonly SelectHeaderButtonProcess _selectHeaderButtonProcess;
        private readonly Header _header;

        [Inject]
        public OnPointerEventsFiredHeader(SelectHeaderButtonProcess selectHeaderButtonProcess, Header header)
        {
            _selectHeaderButtonProcess = selectHeaderButtonProcess;
            _header = header;
        }

        public void Initialize()
        {
            _header.OnPointerEnterImageButtonReturn.Subscribe(_ =>
            {
                _selectHeaderButtonProcess.SelectProcess(HeaderButtonName.Return);
            });

            _header.OnPointerExitImageButtonReturn.Subscribe(_ =>
            {
                _selectHeaderButtonProcess.DeselectProcess(HeaderButtonName.Return);
            });
        }
    }
}