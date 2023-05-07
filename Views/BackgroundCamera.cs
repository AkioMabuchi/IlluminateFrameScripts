using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Camera))]
    public class BackgroundCamera : MonoBehaviour
    {
        [SerializeField] private Camera backgroundCamera;

        private void Reset()
        {
            backgroundCamera = GetComponent<Camera>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Screen.width * 9 / Screen.height < 16)
                    {
                        backgroundCamera.orthographicSize = Screen.height * 19.2f / Screen.width;
                    }
                    else
                    {
                        backgroundCamera.orthographicSize = 10.8f;
                    }
                }).AddTo(gameObject);
        }
    }
}
