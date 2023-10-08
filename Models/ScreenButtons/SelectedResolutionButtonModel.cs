using System;
using System.Collections.Generic;
using Enums;
using Enums.ScreenButtonNames;
using UniRx;

namespace Models.ScreenButtons
{
    public class SelectedSettingsScreenButtonModel
    {
        private readonly ReactiveProperty<SettingsScreenButtonName> 
            _reactivePropertySelectedSettingsScreenButtonName = new(SettingsScreenButtonName.None);

        public IObservable<SettingsScreenButtonName>
            OnChangedSelectedSettingsScreenButtonName => _reactivePropertySelectedSettingsScreenButtonName;

        public SettingsScreenButtonName 
            SelectedSettingsScreenButtonName => _reactivePropertySelectedSettingsScreenButtonName.Value;

        private readonly ReactiveProperty<ResolutionSize>
            _reactivePropertyCurrentResolution = new(ResolutionSize.Size1920X1080);

        private readonly ReactiveProperty<RenderQuality>
            _reactivePropertyCurrentRenderQuality = new(RenderQuality.Medium);

        public bool IsCurrent(SettingsScreenButtonName settingsScreenButtonName)
        {
            switch (settingsScreenButtonName)
            {
                case SettingsScreenButtonName.Resolution960X540:
                {
                    switch (_reactivePropertyCurrentResolution.Value)
                    {
                        case ResolutionSize.Size960X540:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.Resolution1280X720:
                {
                    switch (_reactivePropertyCurrentResolution.Value)
                    {
                        case ResolutionSize.Size1280X720:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.Resolution1920X1080:
                {
                    switch (_reactivePropertyCurrentResolution.Value)
                    {
                        case ResolutionSize.Size1920X1080:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.Resolution2560X1440:
                {
                    switch (_reactivePropertyCurrentResolution.Value)
                    {
                        case ResolutionSize.Size2560X1440:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.Resolution3840X2160:
                {
                    switch (_reactivePropertyCurrentResolution.Value)
                    {
                        case ResolutionSize.Size3840X2160:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.QualityLow:
                {
                    switch (_reactivePropertyCurrentRenderQuality.Value)
                    {
                        case RenderQuality.Low:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.QualityMedium:
                {
                    switch (_reactivePropertyCurrentRenderQuality.Value)
                    {
                        case RenderQuality.Medium:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.QualityHigh:
                {
                    switch (_reactivePropertyCurrentRenderQuality.Value)
                    {
                        case RenderQuality.High:
                        {
                            return true;
                        }
                    }

                    break;
                }
                case SettingsScreenButtonName.QualityVeryHigh:
                {
                    switch (_reactivePropertyCurrentRenderQuality.Value)
                    {
                        case RenderQuality.VeryHigh:
                        {
                            return true;
                        }
                    }

                    break;
                }
            }

            return false;
        }

        public void SetCurrentResolution(ResolutionSize resolutionSize)
        {
            _reactivePropertyCurrentResolution.Value = resolutionSize;
        }

        public void SetCurrentRenderQuality(RenderQuality renderQuality)
        {
            _reactivePropertyCurrentRenderQuality.Value = renderQuality;
        }

        public void Select(SettingsScreenButtonName settingsScreenButtonName)
        {
            _reactivePropertySelectedSettingsScreenButtonName.Value = settingsScreenButtonName;
        }

        public void Deselect()
        {
            _reactivePropertySelectedSettingsScreenButtonName.Value = SettingsScreenButtonName.None;
        }
    }
}