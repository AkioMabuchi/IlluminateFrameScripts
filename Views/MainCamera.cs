using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Reset()
        {
            mainCamera = GetComponent<Camera>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Screen.width * 9 / Screen.height < 16)
                    {
                        mainCamera.fieldOfView = Screen.height * 9.6f * 40.0f / 5.4f / Screen.width;
                    }
                    else
                    {
                        mainCamera.fieldOfView = 40.0f;
                    }
                }).AddTo(gameObject);
        }
    }
}
