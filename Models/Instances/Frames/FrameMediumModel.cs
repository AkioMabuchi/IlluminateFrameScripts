using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameMediumModel : IFrameModel
    {
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalPlusTop = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalPlusTop => 
            _reactivePropertyIsIlluminatedBulbTerminalPlusTop;
        
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalPlusBottom = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalPlusBottom => 
            _reactivePropertyIsIlluminatedBulbTerminalPlusBottom;
        
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalMinusTop = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalMinusTop => 
            _reactivePropertyIsIlluminatedBulbTerminalMinusTop;
        
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalMinusBottom = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalMinusBottom => 
            _reactivePropertyIsIlluminatedBulbTerminalMinusBottom;
        
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbIllumination = new(false);

        public IObservable<bool> OnChangedIsIlluminatedBulbIllumination =>
            _reactivePropertyIsIlluminatedBulbIllumination;
        public IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(0, 0), TileType.PlusPower),
            (new Vector2Int(-1, 0), TileType.MinusPower),
            (new Vector2Int(-6, 5), TileType.PlusTerminal),
            (new Vector2Int(-6, -5), TileType.PlusTerminal),
            (new Vector2Int(5, 5), TileType.MinusTerminal),
            (new Vector2Int(5, -5), TileType.MinusTerminal),
        };

        public bool CanIlluminate
        {
            get
            {
                if (_reactivePropertyIsIlluminatedBulbIllumination.Value)
                {
                    return false;
                }

                return _reactivePropertyIsIlluminatedBulbTerminalPlusTop.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalMinusTop.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalMinusBottom.Value;
            }
        }

        public bool IsIlluminated => _reactivePropertyIsIlluminatedBulbIllumination.Value;
        public int IlluminateScore => 3000;

        public void Initialize()
        {
            _reactivePropertyIsIlluminatedBulbTerminalPlusTop.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalMinusTop.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalMinusBottom.Value = false;
            _reactivePropertyIsIlluminatedBulbIllumination.Value = false;
        }

        public void IlluminateTerminal(Vector2Int cellPosition)
        {
            var x = cellPosition.x;
            var y = cellPosition.y;
            switch (x, y)
            {
                case (-6, 5):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalPlusTop.Value = true;
                    break;
                }
                case (-6, -5):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value = true;
                    break;
                }
                case (5, 5):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalMinusTop.Value = true;
                    break;
                }
                case (5, -5):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalMinusBottom.Value = true;
                    break;
                }
            }
        }

        public void IlluminateIllumination()
        {
            _reactivePropertyIsIlluminatedBulbIllumination.Value = true;
        }
        
        public void DarkenIllumination()
        {
            _reactivePropertyIsIlluminatedBulbIllumination.Value = false;
        }
    }
}