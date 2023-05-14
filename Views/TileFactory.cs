using System.Collections.Generic;
using Classes.Statics;
using DG.Tweening;
using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Views.Instances.Tiles;

namespace Views
{
    public class TileFactory : MonoBehaviour
    {
        [SerializeField] private TileLine1 prefabTileStraight;
        [SerializeField] private TileLine1 prefabTileCurve;
        [SerializeField] private TileLine2 prefabTileTwinCurves;
        [SerializeField] private TileLine2 prefabTileCross;
        [SerializeField] private TileDistributor3 prefabTileDistributor3;
        [SerializeField] private TileDistributor4 prefabTileDistributor4;
        [SerializeField] private TileBulb prefabTileBulb;
        [SerializeField] private TilePower2 prefabTilePowerNormal;
        [SerializeField] private TilePower1 prefabTilePowerPlus;
        [SerializeField] private TilePower1 prefabTilePowerMinus;
        [SerializeField] private TilePower2 prefabTilePowerAlternating;
        [SerializeField] private TileTerminal prefabTileTerminalNormalL;
        [SerializeField] private TileTerminal prefabTileTerminalNormalR;
        [SerializeField] private TileTerminal prefabTileTerminalPlus;
        [SerializeField] private TileTerminal prefabTileTerminalMinus;
        [SerializeField] private TileTerminal prefabTileTerminalAlternatingL;
        [SerializeField] private TileTerminal prefabTileTerminalAlternatingR;

        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;

        [SerializeField] private Vector3 nextTilePositionSmall;
        [SerializeField] private Vector3 nextTilePositionMedium;
        [SerializeField] private Vector3 nextTilePositionLarge;

        [SerializeField] private Vector3 boardBasePositionSmall;
        [SerializeField] private Vector3 boardBasePositionMedium;
        [SerializeField] private Vector3 boardBasePositionLarge;

        [SerializeField] private float durationCurrentTile;
        [SerializeField] private float durationNextTile;

        private readonly Dictionary<int, TileBase> _tiles = new();

        private int? _currentTileId;
        private int? _nextTileId;
        
        private Vector3 _generatePosition = Vector3.zero;
        private Vector3 _nextTilePosition = Vector3.zero;
        private Vector3 _baseTilePosition = Vector3.zero;
        private Vector3 _currentTilePosition = Vector3.zero;
        private Vector3 _boardBasePosition = Vector3.zero;

        private float _currentTileLerpValue;
        private float _nextTileLerpValue;
        
        private Tweener _tweenerCurrentTileLerp;
        private Tweener _tweenerNextTile;
        
        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (_currentTileId.HasValue)
                    {
                        var currentTileId = _currentTileId.Value;
                        if (_tiles.TryGetValue(currentTileId, out var tile))
                        {
                            tile.transform.position = Vector3.Lerp(_baseTilePosition, _currentTilePosition,
                                _currentTileLerpValue);
                        }
                    }
                }).AddTo(gameObject);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Keyboard.current == null)
                    {
                        return;
                    }

                    if (Keyboard.current.tKey.wasPressedThisFrame)
                    {
                        ThrowNextTile();
                    }
                }).AddTo(gameObject);
        }

        public void SetCurrentTileId(int? currentTileId)
        {
            _currentTileId = currentTileId;
        }

        public void SetNextTileId(int? nextTileId)
        {
            _nextTileId = nextTileId;
        }

        public void SetBaseTilePosition()
        {
            if (_currentTileId.HasValue)
            {
                var currentTileId = _currentTileId.Value;
                if (_tiles.TryGetValue(currentTileId, out var tile))
                {
                    _baseTilePosition = tile.transform.position;
                }
            }
        }
        public void SetCurrentTilePosition(Vector3 currentTilePosition)
        {
            _currentTilePosition = currentTilePosition;
        }
        

        public void SetGeneratePosition(FrameSize frameSize)
        {
            _generatePosition = frameSize switch
            {
                FrameSize.Small => generatePositionSmall,
                FrameSize.Medium => generatePositionMedium,
                FrameSize.Large => generatePositionLarge,
                _ => Vector3.zero
            };
        }

        public void SetNextTilePosition(FrameSize frameSize)
        {
            _nextTilePosition = frameSize switch
            {
                FrameSize.Small => nextTilePositionSmall,
                FrameSize.Medium => nextTilePositionMedium,
                FrameSize.Large => nextTilePositionLarge,
                _ => Vector3.zero
            };
        }

        public void SetBoardBasePosition(FrameSize frameSize)
        {
            _boardBasePosition = frameSize switch
            {
                FrameSize.Small => boardBasePositionSmall,
                FrameSize.Medium => boardBasePositionMedium,
                FrameSize.Large => boardBasePositionLarge,
                _ => Vector3.zero
            };
        }

        public TileLine1 GenerateTileStraight(int tileId)
        {
            var tile = Instantiate(prefabTileStraight, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine1 GenerateTileCurve(int tileId)
        {
            var tile = Instantiate(prefabTileCurve, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine2 GenerateTileTwinCurves(int tileId)
        {
            var tile = Instantiate(prefabTileTwinCurves, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine2 GenerateTileCross(int tileId)
        {
            var tile = Instantiate(prefabTileCross, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileDistributor3 GenerateTileDistributor3(int tileId)
        {
            var tile = Instantiate(prefabTileDistributor3, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileDistributor4 GenerateTileDistributor4(int tileId)
        {
            var tile = Instantiate(prefabTileDistributor4, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileBulb GenerateTileBulb(int tileId)
        {
            var tile = Instantiate(prefabTileBulb, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TilePower2 GenerateTilePowerNormal(int tileId)
        {
            var tile = Instantiate(prefabTilePowerNormal, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower1 GenerateTilePowerPlus(int tileId)
        {
            var tile = Instantiate(prefabTilePowerPlus, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower1 GenerateTilePowerMinus(int tileId)
        {
            var tile = Instantiate(prefabTilePowerMinus, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower2 GenerateTilePowerAlternating(int tileId)
        {
            var tile = Instantiate(prefabTilePowerAlternating, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileTerminal GenerateTileTerminalNormalL(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalNormalL, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalNormalR(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalNormalR, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalPlus(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalPlus, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalMinus(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalMinus, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalAlternatingL(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalAlternatingL, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalAlternatingR(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalAlternatingR, _generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public void ClearTile(int tileId)
        {
            if (_tiles.TryGetValue(tileId, out var tile))
            {
                Destroy(tile.gameObject);
            }

            _tiles.Remove(tileId);
        }

        public void ClearAllTiles()
        {
            foreach (var tile in _tiles.Values)
            {
                Destroy(tile.gameObject);
            }

            _tiles.Clear();
        }

        public void TweenCurrentTile()
        {
            _tweenerCurrentTileLerp?.Kill();
            _tweenerNextTile?.Kill();
            
            _tweenerCurrentTileLerp = DOTween.To(() => 0.0f, value => { _currentTileLerpValue = value; }, 1.0f,
                    durationCurrentTile)
                .SetEase(Ease.OutQuart)
                .SetLink(gameObject);
        }

        public void TweenAndMoveNextTile()
        {
            if (_nextTileId.HasValue)
            {
                var nextTileId = _nextTileId.Value;
                if (_tiles.TryGetValue(nextTileId, out var tile))
                {
                    _tweenerNextTile = tile.transform.DOMove(_nextTilePosition, durationNextTile)
                        .From(_generatePosition)
                        .SetEase(Ease.OutCubic)
                        .SetLink(tile.gameObject);
                }
            }
        }

        public void ThrowNextTile()
        {
            if (_nextTileId.HasValue)
            {
                var nextTileId = _nextTileId.Value;
                if (_tiles.TryGetValue(nextTileId, out var tile))
                {
                    tile.Throw();
                }
            }
        }

        public void PutTileOnBoard(int tileId, Vector2Int cellPosition)
        {
            if (_tiles.TryGetValue(tileId, out var tile))
            {
                tile.transform.position =
                    new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize)
                    + _boardBasePosition;
            }
        }

        public void RotateTileImmediate(int tileId)
        {
            if (_tiles.TryGetValue(tileId, out var tile))
            {
                tile.RotateImmediate();
            }
        }

        public void RotateCurrentTile()
        {
            if (_currentTileId.HasValue)
            {
                var currentTileId = _currentTileId.Value;
                if (_tiles.TryGetValue(currentTileId, out var tile))
                {
                    tile.Rotate();
                }
            }
        }
    }
}
