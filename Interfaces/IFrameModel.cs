using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IFrameModel
    {
        public IEnumerable<(Vector2Int, TileType)> InitialTiles
        {
            get;
        }

        public bool CanIlluminate
        {
            get;
        }

        public bool IsIlluminated
        {
            get;
        }

        public int IlluminateScore
        {
            get;
        }

        public void Initialize();

        public void IlluminateTerminal(Vector2Int cellPosition);

        public void IlluminateIllumination();

        public void DarkenIllumination();
    }
}