using System.Collections.Generic;
using Enums;

namespace Interfaces.Tiles
{
    public interface IRenderableTile
    {
        public void Render();
        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut);
        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses);
        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut);

        public void RenderReset();
    }
}