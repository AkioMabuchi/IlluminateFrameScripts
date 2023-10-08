using Enums;
using UnityEngine;

namespace Views.Controllers
{
    public class BoardTilePositioner
    {
        private const float TileSize = 0.1f;
        private readonly Vector3 _smallFrameBasePosition = new(0.0f, 0.01f, 0.0f);
        private readonly Vector3 _mediumFrameBasePosition = new(0.05f, 0.01f, 0.0f);
        private readonly Vector3 _largeFrameBasePosition = new(0.0f, 0.01f, 0.0f);

        private FrameSize _frameSize = FrameSize.None;

        public void SetFrameSize(FrameSize frameSize)
        {
            _frameSize = frameSize;
        }

        public Vector3 GetPosition(Vector2Int cellPosition)
        {
            var offset = new Vector3(cellPosition.x * TileSize, 0.0f, cellPosition.y * TileSize);
            return _frameSize switch
            {
                FrameSize.Small => _smallFrameBasePosition + offset,
                FrameSize.Medium => _mediumFrameBasePosition + offset,
                FrameSize.Large => _largeFrameBasePosition + offset,
                _ => offset
            };
        }
    }
}