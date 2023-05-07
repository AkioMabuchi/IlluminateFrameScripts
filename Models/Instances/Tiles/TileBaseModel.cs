using System;
using System.Collections.Generic;
using Parameters.Enums;
using Parameters.Structs;
using UniRx;
using UnityEngine;

namespace Models.Instances.Tiles
{
    public abstract class TileBaseModel
    {
        private readonly ReactiveProperty<RotateStatus> _reactivePropertyRotateStatus = new(RotateStatus.Rotate0);
        public IObservable<RotateStatus> OnChangedRotateStatus => _reactivePropertyRotateStatus;
        public RotateStatus RotateStatus => _reactivePropertyRotateStatus.Value;

        public abstract TileType TileType
        {
            get;
        }

        public void Rotate()
        { ;
            _reactivePropertyRotateStatus.Value = _reactivePropertyRotateStatus.Value switch
            {
                RotateStatus.Rotate0 => RotateStatus.Rotate90,
                RotateStatus.Rotate90 => RotateStatus.Rotate180,
                RotateStatus.Rotate180 => RotateStatus.Rotate270,
                RotateStatus.Rotate270 => RotateStatus.Rotate0,
                _ => RotateStatus.Rotate0
            };
        }

        public TileEdgeType GetTileEdgeType(LineDirection lineDirection)
        {
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    return GetTileEdgeTypeSub(lineDirection);
                }
                case RotateStatus.Rotate90:
                {
                    return GetTileEdgeTypeSub(lineDirection switch
                    {
                        LineDirection.Up => LineDirection.Left,
                        LineDirection.Right => LineDirection.Up,
                        LineDirection.Down => LineDirection.Right,
                        LineDirection.Left => LineDirection.Down,
                        _ => lineDirection
                    });
                }
                case RotateStatus.Rotate180:
                {
                    return GetTileEdgeTypeSub(lineDirection switch
                    {
                        LineDirection.Up => LineDirection.Down,
                        LineDirection.Right => LineDirection.Left,
                        LineDirection.Down => LineDirection.Up,
                        LineDirection.Left => LineDirection.Right,
                        _ => lineDirection
                    });
                }
                case RotateStatus.Rotate270:
                {
                    return GetTileEdgeTypeSub(lineDirection switch
                    {
                        LineDirection.Up => LineDirection.Right,
                        LineDirection.Right => LineDirection.Down,
                        LineDirection.Down => LineDirection.Left,
                        LineDirection.Left => LineDirection.Up,
                        _ => lineDirection
                    });
                }
            }

            return TileEdgeType.Free;
        }

        protected abstract TileEdgeType GetTileEdgeTypeSub(LineDirection lineDirection);

        public IEnumerable<ConductOutput> Conduct(ElectricStatus electricStatus, LineDirection lineDirection)
        {
            var outputs = _reactivePropertyRotateStatus.Value switch
            {
                RotateStatus.Rotate0 => ConductSub(electricStatus, lineDirection),
                RotateStatus.Rotate90 => ConductSub(electricStatus, lineDirection switch
                {
                    LineDirection.Up => LineDirection.Left,
                    LineDirection.Right => LineDirection.Up,
                    LineDirection.Down => LineDirection.Right,
                    LineDirection.Left => LineDirection.Down,
                    _ => lineDirection
                }),
                RotateStatus.Rotate180 => ConductSub(electricStatus, lineDirection switch
                {
                    LineDirection.Up => LineDirection.Down,
                    LineDirection.Right => LineDirection.Left,
                    LineDirection.Down => LineDirection.Up,
                    LineDirection.Left => LineDirection.Right,
                    _ => lineDirection
                }),
                RotateStatus.Rotate270 => ConductSub(electricStatus, lineDirection switch
                {
                    LineDirection.Up => LineDirection.Right,
                    LineDirection.Right => LineDirection.Down,
                    LineDirection.Down => LineDirection.Left,
                    LineDirection.Left => LineDirection.Up,
                    _ => lineDirection
                }),
                _ => new List<ConductOutput>()
            };


            for (var i = 0; i < outputs.Count; i++)
            {
                var output = outputs[i];
                var tmpLineDirection = _reactivePropertyRotateStatus.Value switch
                {
                    RotateStatus.Rotate0 => output.LineDirection,
                    RotateStatus.Rotate90 => output.LineDirection switch
                    {
                        LineDirection.Up => LineDirection.Right,
                        LineDirection.Right => LineDirection.Down,
                        LineDirection.Down => LineDirection.Left,
                        LineDirection.Left => LineDirection.Up,
                        _ => output.LineDirection
                    },
                    RotateStatus.Rotate180 => output.LineDirection switch
                    {
                        LineDirection.Up => LineDirection.Down,
                        LineDirection.Right => LineDirection.Left,
                        LineDirection.Down => LineDirection.Up,
                        LineDirection.Left => LineDirection.Right,
                        _ => output.LineDirection
                    },
                    RotateStatus.Rotate270 => output.LineDirection switch
                    {
                        LineDirection.Up => LineDirection.Left,
                        LineDirection.Right => LineDirection.Up,
                        LineDirection.Down => LineDirection.Right,
                        LineDirection.Left => LineDirection.Down,
                        _ => output.LineDirection
                    },
                    _ => output.LineDirection
                };
                output.LineDirection = tmpLineDirection;
                outputs[i] = output;
            }

            return outputs;
        }

        protected abstract List<ConductOutput> ConductSub(ElectricStatus electricStatus, LineDirection lineDirection);

        public void Illuminate(ElectricStatus electricStatus, LineDirection inputLineDirection,
            LineDirection outputLineDirection)
        {
            switch (_reactivePropertyRotateStatus.Value)
            {
                case RotateStatus.Rotate0:
                {
                    IlluminateSub(electricStatus, inputLineDirection, outputLineDirection);
                    break;
                }
                case RotateStatus.Rotate90:
                {
                    IlluminateSub(electricStatus, inputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Left,
                        LineDirection.Right => LineDirection.Up,
                        LineDirection.Down => LineDirection.Right,
                        LineDirection.Left => LineDirection.Down,
                        _ => inputLineDirection
                    }, outputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Left,
                        LineDirection.Right => LineDirection.Up,
                        LineDirection.Down => LineDirection.Right,
                        LineDirection.Left => LineDirection.Down,
                        _ => outputLineDirection
                    });
                    break;
                }
                case RotateStatus.Rotate180:
                {
                    IlluminateSub(electricStatus, inputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Down,
                        LineDirection.Right => LineDirection.Left,
                        LineDirection.Down => LineDirection.Up,
                        LineDirection.Left => LineDirection.Right,
                        _ => inputLineDirection
                    }, outputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Down,
                        LineDirection.Right => LineDirection.Left,
                        LineDirection.Down => LineDirection.Up,
                        LineDirection.Left => LineDirection.Right,
                        _ => outputLineDirection
                    });
                    break;
                }
                case RotateStatus.Rotate270:
                {
                    IlluminateSub(electricStatus, inputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Right,
                        LineDirection.Right => LineDirection.Down,
                        LineDirection.Down => LineDirection.Left,
                        LineDirection.Left => LineDirection.Up,
                        _ => inputLineDirection
                    }, outputLineDirection switch
                    {
                        LineDirection.Up => LineDirection.Right,
                        LineDirection.Right => LineDirection.Down,
                        LineDirection.Down => LineDirection.Left,
                        LineDirection.Left => LineDirection.Up,
                        _ => outputLineDirection
                    });
                    break;
                }
            }
        }

        protected abstract void IlluminateSub(ElectricStatus electricStatus, LineDirection inputLineDirection,
            LineDirection outputLineDirection);
    
    }
}