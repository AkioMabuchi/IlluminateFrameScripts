using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
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
        
        [SerializeField] private Vector3 generatePosition; 
        [SerializeField] private Vector3 nextTilePosition;

        [SerializeField] private float durationCurrentTile;
        [SerializeField] private float durationNextTile;

        private readonly Dictionary<int, TileBase> _tiles = new();

        private int? _currentTileId;
        private int? _nextTileId;

        private Vector3 _baseTilePosition;
        private Vector3 _currentTilePosition;
        
        private float _currentTileLerpValue;
        private float _nextTileLerpValue;

        private Tweener _tweenerCurrentTileLerp;
        private Tweener _tweenerNextTile;

        private readonly float _mainBoardHeight = 0.01f;
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

        public TileLine1 GenerateTileStraight(int tileId)
        {
            var tile = Instantiate(prefabTileStraight, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine1 GenerateTileCurve(int tileId)
        {
            var tile = Instantiate(prefabTileCurve, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine2 GenerateTileTwinCurves(int tileId)
        {
            var tile = Instantiate(prefabTileTwinCurves, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileLine2 GenerateTileCross(int tileId)
        {
            var tile = Instantiate(prefabTileCross, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileDistributor3 GenerateTileDistributor3(int tileId)
        {
            var tile = Instantiate(prefabTileDistributor3, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileDistributor4 GenerateTileDistributor4(int tileId)
        {
            var tile = Instantiate(prefabTileDistributor4, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileBulb GenerateTileBulb(int tileId)
        {
            var tile = Instantiate(prefabTileBulb, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TilePower2 GenerateTilePowerNormal(int tileId)
        {
            var tile = Instantiate(prefabTilePowerNormal, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower1 GenerateTilePowerPlus(int tileId)
        {
            var tile = Instantiate(prefabTilePowerPlus, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower1 GenerateTilePowerMinus(int tileId)
        {
            var tile = Instantiate(prefabTilePowerMinus, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TilePower2 GenerateTilePowerAlternating(int tileId)
        {
            var tile = Instantiate(prefabTilePowerAlternating, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }

        public TileTerminal GenerateTileTerminalNormalL(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalNormalL, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalNormalR(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalNormalR, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalPlus(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalPlus, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalMinus(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalMinus, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalAlternatingL(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalAlternatingL, generatePosition, Quaternion.identity, transform);
            _tiles.Add(tileId, tile);
            return tile;
        }
        
        public TileTerminal GenerateTileTerminalAlternatingR(int tileId)
        {
            var tile = Instantiate(prefabTileTerminalAlternatingR, generatePosition, Quaternion.identity, transform);
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
                    _tweenerNextTile = tile.transform.DOMove(nextTilePosition, durationNextTile)
                        .From(generatePosition)
                        .SetEase(Ease.OutCubic)
                        .SetLink(tile.gameObject);
                }
            }
        }

        public void MoveTile(int tileId, Vector3 position)
        {
            if (_tiles.TryGetValue(tileId, out var tile))
            {
                tile.transform.position = position;
            }
        }

        public void RotateTileImmediate(int tileId)
        {
            if (_tiles.TryGetValue(tileId, out var tile))
            {
                tile.RotateTileImmediate();
            }
        }

        public void RotateCurrentTile()
        {
            if (_currentTileId.HasValue)
            {
                var currentTileId = _currentTileId.Value;
                if (_tiles.TryGetValue(currentTileId, out var tile))
                {
                    tile.RotateTile();
                }
            }
        }
    }
}
