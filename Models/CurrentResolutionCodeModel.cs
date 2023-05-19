using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public class CurrentResolutionCodeModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyCurrentResolutionCode = new(0);
        public IObservable<int> OnChangedCurrentResolutionCode => _reactivePropertyCurrentResolutionCode;
        public int CurrentResolutionCode => _reactivePropertyCurrentResolutionCode.Value;

        public Vector2Int Resolution
        {
            get
            {
                var resolutions = new List<Vector2Int>
                {
                    new(960, 540),
                    new(1280, 720),
                    new(1920, 1080),
                    new(2560, 1440),
                    new(3840, 2160)
                };
                
                return resolutions[_reactivePropertyCurrentResolutionCode.Value];
            }
        }

        public void SetCurrentResolutionCode(int resolutionCode)
        {
            _reactivePropertyCurrentResolutionCode.Value = resolutionCode;
        }
    }
}