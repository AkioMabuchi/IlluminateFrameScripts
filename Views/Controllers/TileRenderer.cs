using System.Collections.Generic;
using Enums;
using Interfaces.Tiles;

namespace Views.Controllers
{
    public class TileRenderer
    {
        private readonly Dictionary<int, IRenderableTile> _renderableTiles = new();

        public void AddTile(int tileId, IRenderableTile renderableTile)
        {
            _renderableTiles.Add(tileId, renderableTile);
        }

        public void RenderAllTiles()
        {
            foreach (var renderableTile in _renderableTiles.Values)
            {
                renderableTile.Render();
            }
        }
        
        public void RenderIlluminateTile(int tileId, LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            if (_renderableTiles.TryGetValue(tileId, out var renderableTile))
            {
                renderableTile.RenderIlluminate(lineDirectionIn, lineDirectionOut);
            }
        }

        public void RenderDarkenAllTiles(IEnumerable<ElectricStatus> electricStatuses)
        {
            var electricStatusesList = new List<ElectricStatus>(electricStatuses);
            foreach (var renderableTile in _renderableTiles.Values)
            {
                renderableTile.RenderDarken(electricStatusesList);
            }
        }

        public void RenderDarkenAllTilesComplete()
        {
            var electricStatuses = new List<ElectricStatus>
            {
                ElectricStatus.Normal,
                ElectricStatus.Plus,
                ElectricStatus.Minus,
                ElectricStatus.Alternating
            };
            
            foreach (var renderableTile in _renderableTiles.Values)
            {
                renderableTile.RenderDarken(electricStatuses);
            }
        }

        public void RenderShortTile(int tileId, ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            if (_renderableTiles.TryGetValue(tileId, out var renderableTile))
            {
                renderableTile.RenderShort(shortedStatus, lineDirectionIn, lineDirectionOut);
            }
        }

        public void ClearAllTiles()
        {
            _renderableTiles.Clear();
        }
    }
}