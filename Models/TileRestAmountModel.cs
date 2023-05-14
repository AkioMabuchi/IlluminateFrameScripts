using System;
using Enums;
using UniRx;

namespace Models
{
    public class TileRestAmountModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyTileRestAmount = new(0);
        public IObservable<int> OnChangedTileRestAmount => _reactivePropertyTileRestAmount;
        public bool IsRunOUt => _reactivePropertyTileRestAmount.Value <= 0;

        public void ResetTileRestAmount(FrameSize frameSize)
        {
            _reactivePropertyTileRestAmount.Value = frameSize switch
            {
                FrameSize.Small => 30,
                FrameSize.Medium => 60,
                FrameSize.Large => 100,
                _ => 0
            };
        }

        public void DecreaseTileRestAmount()
        {
            _reactivePropertyTileRestAmount.Value--;
        }
    }
}