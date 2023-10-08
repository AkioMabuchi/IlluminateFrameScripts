using System;
using Enums;
using UniRx;

namespace Models
{
    public class TileRestAmountModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyInitialTileRestAmount = new(0);
        private readonly ReactiveProperty<int> _reactivePropertyTileRestAmount = new(0);
        public IObservable<int> OnChangedTileRestAmount => _reactivePropertyTileRestAmount;
        public bool IsRunOUt => _reactivePropertyTileRestAmount.Value <= 0;

        public void Initialize(FrameSize frameSize)
        {
            _reactivePropertyInitialTileRestAmount.Value = frameSize switch
            {
                FrameSize.Small => 30,
                FrameSize.Medium => 60,
                FrameSize.Large => 100,
                _ => 0
            };

            _reactivePropertyTileRestAmount.Value = _reactivePropertyInitialTileRestAmount.Value;
        }

        public void DecreaseTileRestAmount()
        {
            _reactivePropertyTileRestAmount.Value--;
        }

        public void Reset()
        {
            _reactivePropertyTileRestAmount.Value = _reactivePropertyInitialTileRestAmount.Value;
        }
    }
}