using System;
using Consts;
using Enums;
using Enums.ScreenButtonNames;
using Models;
using Models.ScreenButtons;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Screens;

namespace Processes.Events.Screens
{
    public class SettingsScreenEventProcess : IInitializable, IDisposable
    {
        private readonly SelectedSettingsScreenButtonModel _selectedSettingsScreenButtonModel;
        
        private readonly SteamSettingsScreen _steamSettingsScreen;

        private readonly MusicPlayer _musicPlayer;
        private readonly SoundPlayer _soundPlayer;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public SettingsScreenEventProcess(SelectedSettingsScreenButtonModel selectedSettingsScreenButtonModel,SteamSettingsScreen steamSettingsScreen, MusicPlayer musicPlayer,
            SoundPlayer soundPlayer)
        {
            _selectedSettingsScreenButtonModel = selectedSettingsScreenButtonModel;
            
            _steamSettingsScreen = steamSettingsScreen;
            
            _musicPlayer = musicPlayer;
            _soundPlayer = soundPlayer;
        }

        public void Initialize()
        {
            _steamSettingsScreen.SliderMusic.OnValueChangedAsObservable().Subscribe(volume =>
            {
                _musicPlayer.ChangeVolume(volume);
            }).AddTo(_compositeDisposable);

            _steamSettingsScreen.SliderSound.OnValueChangedAsObservable().Subscribe(volume =>
            {
                _soundPlayer.ChangeVolume(volume);
            }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ObservableEventTriggerSliderMusic
                .OnPointerUpAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    ES3.Save(SaveKey.Music, _steamSettingsScreen.SliderMusic.value);
                })
                .AddTo(_compositeDisposable);

            _steamSettingsScreen.ObservableEventTriggerSliderSound
                .OnPointerUpAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    ES3.Save(SaveKey.Sound, _steamSettingsScreen.SliderSound.value);
                    _soundPlayer.PlayTestSound();
                })
                .AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution960X540.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution960X540))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.Resolution960X540);
                    _steamSettingsScreen.ImageButtonResolution960X540.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution1280X720.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution1280X720))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.Resolution1280X720);
                    _steamSettingsScreen.ImageButtonResolution1280X720.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution1920X1080.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution1920X1080))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.Resolution1920X1080);
                    _steamSettingsScreen.ImageButtonResolution1920X1080.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution2560X1440.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution2560X1440))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.Resolution2560X1440);
                    _steamSettingsScreen.ImageButtonResolution2560X1440.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution3840X2160.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution3840X2160))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.Resolution3840X2160);
                    _steamSettingsScreen.ImageButtonResolution3840X2160.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution960X540.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution960X540))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonResolution960X540.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution1280X720.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution1280X720))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonResolution1280X720.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution1920X1080.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution1920X1080))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonResolution1920X1080.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution2560X1440.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution2560X1440))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonResolution2560X1440.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonResolution3840X2160.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.Resolution3840X2160))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonResolution3840X2160.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityLow.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityLow))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.QualityLow);
                    _steamSettingsScreen.ImageButtonQualityLow.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityMedium.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityMedium))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.QualityMedium);
                    _steamSettingsScreen.ImageButtonQualityMedium.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityHigh.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityHigh))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.QualityHigh);
                    _steamSettingsScreen.ImageButtonQualityHigh.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityVeryHigh.ObservableEventTrigger
                .OnPointerEnterAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityVeryHigh))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Select(SettingsScreenButtonName.QualityVeryHigh);
                    _steamSettingsScreen.ImageButtonQualityVeryHigh.ChangeColorSelected();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityLow.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityLow))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonQualityLow.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityMedium.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityMedium))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonQualityMedium.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityHigh.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityHigh))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonQualityHigh.ChangeColorNone();
                }).AddTo(_compositeDisposable);

            _steamSettingsScreen.ImageButtonQualityVeryHigh.ObservableEventTrigger
                .OnPointerExitAsObservable()
                .AsUnitObservable()
                .Subscribe(_ =>
                {
                    if (_selectedSettingsScreenButtonModel.IsCurrent(SettingsScreenButtonName.QualityVeryHigh))
                    {
                        return;
                    }

                    _selectedSettingsScreenButtonModel.Deselect();
                    _steamSettingsScreen.ImageButtonQualityVeryHigh.ChangeColorNone();
                }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}