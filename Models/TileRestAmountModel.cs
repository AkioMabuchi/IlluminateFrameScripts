using System;
using Parameters.Enums;
using UniRx;

namespace Models
{
    public class TileRestAmountModel
    {
        private readonly ReactiveProperty<int> _reactivePropertyTileRestAmount = new(0);
        public IObservable<int> OnChangedTileRestAmount => _reactivePropertyTileRestAmount;

        public void ResetTileRestAmount(PanelSize panelSize)
        {
            _reactivePropertyTileRestAmount.Value = panelSize switch
            {
                PanelSize.Small => 30,
                PanelSize.Medium => 60,
                PanelSize.Large => 100,
                _ => 0
            };
        }

        public void DecreaseTileRestAmount()
        {
            _reactivePropertyTileRestAmount.Value--;
        }
    }
}