using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Models.Instances.Frames
{
    public class FrameSmallModel : IFrameModel
    {
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalA = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalA => _reactivePropertyIsIlluminatedBulbTerminalA;
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalB = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalB => _reactivePropertyIsIlluminatedBulbTerminalB;
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalC= new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalC => _reactivePropertyIsIlluminatedBulbTerminalC;
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbTerminalD = new(false);
        public IObservable<bool> OnChangedIsIlluminatedBulbTerminalD => _reactivePropertyIsIlluminatedBulbTerminalD;
        private readonly ReactiveProperty<bool> _reactivePropertyIsIlluminatedBulbIllumination = new(false);

        public IObservable<bool> OnChangedIsIlluminatedBulbIllumination =>
            _reactivePropertyIsIlluminatedBulbIllumination;
        public IEnumerable<(Vector2Int, TileType)> InitialTiles => new List<(Vector2Int, TileType)>
        {
            (new Vector2Int(0, 0), TileType.NormalPower),
            (new Vector2Int(-4, 3), TileType.NormalTerminalLeft),
            (new Vector2Int(-4, -3), TileType.NormalTerminalLeft),
            (new Vector2Int(4, 3), TileType.NormalTerminalRight),
            (new Vector2Int(4, -3), TileType.NormalTerminalRight)
        };

        public bool CanIlluminate
        {
            get
            {
                if (_reactivePropertyIsIlluminatedBulbIllumination.Value)
                {
                    return false;
                }

                return _reactivePropertyIsIlluminatedBulbTerminalA.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalB.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalC.Value &&
                       _reactivePropertyIsIlluminatedBulbTerminalD.Value;
            }
        }

        public bool IsIlluminated => _reactivePropertyIsIlluminatedBulbIllumination.Value;
        public int IlluminateScore => 1200;

        public void Initialize()
        {
            _reactivePropertyIsIlluminatedBulbTerminalA.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalB.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalC.Value = false;
            _reactivePropertyIsIlluminatedBulbTerminalD.Value = false;
            _reactivePropertyIsIlluminatedBulbIllumination.Value = false;
        }
        public void IlluminateTerminal(Vector2Int cellPosition)
        {
            var x = cellPosition.x;
            var y = cellPosition.y;
            switch (x, y)
            {
                case (-4, 3):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalA.Value = true;
                    break;
                }
                case (4, 3):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalB.Value = true;
                    break;
                }
                case (-4, -3):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalC.Value = true;
                    break;
                }
                case (4, -3):
                {
                    _reactivePropertyIsIlluminatedBulbTerminalD.Value = true;
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