using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Views.Instances;

namespace Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI textMeshProLabelMusic;
        [SerializeField] private TextMeshProUGUI textMeshProLabelSound;
        [SerializeField] private TextMeshProUGUI textMeshProLabelResolution;

        [SerializeField] private Slider sliderMusic;
        [SerializeField] private Slider sliderSound;

        [SerializeField] private ObservableEventTrigger observableEventTriggerSliderMusic;
        [SerializeField] private ObservableEventTrigger observableEventTriggerSliderSound;
        [SerializeField] private ImageButtonSettingsResolution[] imageButtonsSettingsResolution;

        public IObservable<float> OnChangedSliderMusicValue => sliderMusic.OnValueChangedAsObservable();
        public IObservable<float> OnChangedSliderSoundValue => sliderSound.OnValueChangedAsObservable();
        private readonly Subject<int> _subjectOnPointerEnterImageButtonResolution = new();
        public IObservable<int> OnPointerEnterImageButtonResolution => _subjectOnPointerEnterImageButtonResolution;
        private readonly Subject<int> _subjectOnPointerExitImageButtonResolution = new();
        public IObservable<int> OnPointerExitImageButtonResolution => _subjectOnPointerExitImageButtonResolution;

        private readonly Subject<float> _subjectOnPointerUpSliderMusic = new();
        public IObservable<float> OnPointerUpSliderMusic => _subjectOnPointerUpSliderMusic;
        private readonly Subject<float> _subjectOnPointerUpSliderSound = new();
        public IObservable<float> OnPointerUpSliderSound => _subjectOnPointerUpSliderSound;
        private int _currentResolutionCode;
        private Sequence _sequenceFade;
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            observableEventTriggerSliderMusic.OnPointerUpAsObservable().Subscribe(_ =>
            {
                _subjectOnPointerUpSliderMusic.OnNext(sliderMusic.value);
            }).AddTo(gameObject);

            observableEventTriggerSliderSound.OnPointerUpAsObservable().Subscribe(_ =>
            {
                _subjectOnPointerUpSliderSound.OnNext(sliderSound.value);
            }).AddTo(gameObject);
            
            for (var i = 0; i < imageButtonsSettingsResolution.Length; i++)
            {
                var ii = i;
                imageButtonsSettingsResolution[i].OnPointerEnter.Subscribe(_ =>
                {
                    _subjectOnPointerEnterImageButtonResolution.OnNext(ii);
                }).AddTo(gameObject);
                imageButtonsSettingsResolution[i].OnPointerExit.Subscribe(_ =>
                {
                    _subjectOnPointerExitImageButtonResolution.OnNext(ii);
                }).AddTo(gameObject);
            }
        }

        private void Start()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void SetCurrentResolutionCode(int currentResolutionCode)
        {
            _currentResolutionCode = currentResolutionCode;
        }

        public void Localize()
        {
            
        }

        public void ChangeSliderValueMusic(float value)
        {
            sliderMusic.value = value;
        }

        public void ChangeSliderValueSound(float value)
        {
            sliderSound.value = value;
        }

        public void FadeIn()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1.0f, 1.0f)
                    .SetEase(Ease.InOutSine)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }).SetLink(gameObject);
        }

        public void FadeOut()
        {
            _sequenceFade?.Kill();
            _sequenceFade = DOTween.Sequence()
                .Append(canvasGroup.DOFade(0.0f, 1.0f)
                    .SetEase(Ease.InOutSine)
                ).OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }).SetLink(gameObject);
        }

        public void ChangeImageButtonResolutionColors(int? nullableSelectedResolutionCode)
        {
            for (var i = 0; i < imageButtonsSettingsResolution.Length; i++)
            {
                if (i == _currentResolutionCode)
                {
                    imageButtonsSettingsResolution[i].ChangeColorCurrent();
                }
                else if(nullableSelectedResolutionCode.HasValue)
                {
                    var selectedResolutionCode = nullableSelectedResolutionCode.Value;
                    if (i == selectedResolutionCode)
                    {
                        imageButtonsSettingsResolution[i].ChangeColorSelected();
                    }
                    else
                    {
                        imageButtonsSettingsResolution[i].ChangeColorNone();
                    }
                }
                else
                {
                    imageButtonsSettingsResolution[i].ChangeColorNone();
                }
            }
        }
    }
}
