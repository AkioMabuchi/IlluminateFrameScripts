using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameLargeModel : IFrameModel
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

        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalAlternatingLeft = new(false);

        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalAlternatingLeft =>
            _reactivePropertyIsIlluminatedBulbTerminalAlternatingLeft;

        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalAlternatingRight = new(false);

        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalAlternatingRight =>
            _reactivePropertyIsIlluminatedBulbTerminalAlternatingRight;
        
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbIllumination = new(false);

        public IObservable<bool> OnChangedIsIlluminatedBulbIllumination =>
            _reactivePropertyIsIlluminatedBulbIllumination;

        public IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(1, 0), TileType.PlusPower),
            (new Vector2Int(-1, 0), TileType.MinusPower),
            (new Vector2Int(0, 0), TileType.AlternatingPower),
            (new Vector2Int(-7, 6), TileType.PlusTerminal),
            (new Vector2Int(-7, -6), TileType.PlusTerminal),
            (new Vector2Int(7, 6), TileType.MinusTerminal),
            (new Vector2Int(7, -6), TileType.MinusTerminal),
            (new Vector2Int(-7, 0), TileType.AlternatingTerminalLeft),
            (new Vector2Int(7, 0), TileType.AlternatingTerminalRight),
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
                       _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalAlternatingLeft.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalAlternatingRight.Value;
            }
        }

        public bool IsIlluminated => _reactivePropertyIsIlluminatedBulbIllumination.Value;
        public int IlluminateScore => 6000;

        public void Initialize()
        {
            _reactivePropertyIsIlluminatedBulbTerminalPlusTop.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalMinusTop.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalAlternatingLeft.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalAlternatingRight.Value = false;
            _reactivePropertyIsIlluminatedBulbIllumination.Value = false;
        }

        public void IlluminateTerminal(Vector2Int cellPosition)
        {
            var x = cellPosition.x;
            var y = cellPosition.y;
            switch (x, y)
            {
                case (-7, 6):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalPlusTop.Value = true;
                    break;
                }
                case (-7, -6):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalPlusBottom.Value = true;
                    break;
                }
                case (7, 6):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalMinusTop.Value = true;
                    break;
                }
                case (7, -6):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalMinusBottom.Value = true;
                    break;
                }
                case (-7, 0):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalAlternatingLeft.Value = true;
                    break;
                }
                case (7, 0):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalAlternatingRight.Value = true;
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