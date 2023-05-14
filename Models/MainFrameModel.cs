using System;
using Enums;
using Models.Instances.Frames;
using UniRx;

namespace Models
{
    public class MainFrameModel
    {
        private readonly ReactiveProperty<FrameBaseModel> _reactivePropertyFrameModel = new(new FrameNoneModel());
        public IObservable<FrameBaseModel> OnChangedFrameModel => _reactivePropertyFrameModel;
        public FrameBaseModel Frame => _reactivePropertyFrameModel.Value;

        public void SetMainFrame(FrameSize frameSize)
        {
            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    _reactivePropertyFrameModel.Value = new FrameSmallModel();
                    break;
                }
                case FrameSize.Medium:
                {
                    _reactivePropertyFrameModel.Value = new FrameMediumModel();
                    break;
                }
                case FrameSize.Large:
                {
                    _reactivePropertyFrameModel.Value = new FrameLargeModel();
                    break;
                }
                default:
                {
                    _reactivePropertyFrameModel.Value = new FrameNoneModel();
                    break;
                }
            }
        }
    }
}