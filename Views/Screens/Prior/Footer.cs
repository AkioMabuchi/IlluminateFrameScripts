using DG.Tweening;
using Enums;
using Enums.ScreenButtonNames;
using Enums.ScreenButtonNames.Prior;
using TMPro;
using UnityEngine;

namespace Views.Screens.Prior
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Footer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransformBackground;

        [SerializeField] private TextMeshProUGUI textMeshProFooting;

        [SerializeField] private Localizer localizer;

        [SerializeField] private float durationShow;
        [SerializeField] private Ease easeShow;

        [SerializeField] private float durationHide;
        [SerializeField] private Ease easeHide;

        private Tweener _tweener;

        private GameStateName _gameStateName;
        private HeaderButtonName _headerButtonName;
        private SelectFrameSizeScreenButtonName _selectFrameSizeScreenButtonName;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 1.0f;
            rectTransformBackground.anchoredPosition = new Vector2(0.0f, -rectTransformBackground.sizeDelta.y);
            textMeshProFooting.text = "";
        }

        public void SetGameStateName(GameStateName gameStateName)
        {
            _gameStateName = gameStateName;
        }

        public void SetHeaderButtonName(HeaderButtonName headerButtonName)
        {
            _headerButtonName = headerButtonName;
        }

        public void SetSelectFrameSizeScreenButtonName(SelectFrameSizeScreenButtonName selectFrameSizeScreenButtonName)
        {
            _selectFrameSizeScreenButtonName = selectFrameSizeScreenButtonName;
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            _tweener?.Kill();
            _tweener = rectTransformBackground.DOAnchorPosY(0.0f, durationShow)
                .SetEase(easeShow)
                .SetLink(gameObject);
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _tweener?.Kill();
            _tweener = rectTransformBackground.DOAnchorPosY(-rectTransformBackground.sizeDelta.y, durationHide)
                .SetEase(easeHide)
                .SetLink(gameObject);
        }

        public void RenderText()
        {
            switch (_gameStateName)
            {
                case GameStateName.Main:
                {
                    textMeshProFooting.text = localizer.CurrentLocale.FooterMainGame;
                    break;
                }
                case GameStateName.SelectFrameSize:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            switch (_selectFrameSizeScreenButtonName)
                            {
                                case SelectFrameSizeScreenButtonName.Small:
                                {
                                    textMeshProFooting.text = localizer.CurrentLocale.FooterSelectFrameSizeSmall;
                                    break;
                                }
                                case SelectFrameSizeScreenButtonName.Medium:
                                {
                                    textMeshProFooting.text = localizer.CurrentLocale.FooterSelectFrameSizeMedium;
                                    break;
                                }
                                case SelectFrameSizeScreenButtonName.Large:
                                {
                                    textMeshProFooting.text = localizer.CurrentLocale.FooterSelectFrameSizeLarge;
                                    break;
                                }
                                default:
                                {
                                    textMeshProFooting.text = "";
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Instruction:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Settings:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Records:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Achievements:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Statistics:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Credits:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.Licenses:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToTitle;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                case GameStateName.ResultRecords:
                {
                    switch (_headerButtonName)
                    {
                        case HeaderButtonName.Return:
                        {
                            textMeshProFooting.text = localizer.CurrentLocale.FooterReturnToResult;
                            break;
                        }
                        default:
                        {
                            textMeshProFooting.text = "";
                            break;
                        }
                    }

                    break;
                }
                default:
                {
                    textMeshProFooting.text = "";
                    break;
                }
            }
        }
    }
}
