using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Presenters.Instances
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RecordsBoardMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        [SerializeField] private float loadingCycle;
        
        [SerializeField] private Color colorLoading;
        [SerializeField] private Color colorFailed;
        
        private void Reset()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        
        private void Start()
        {
            textMeshPro.text = "";
        }

        public void RenderClear()
        {
            StopAllCoroutines();

            IEnumerator Coroutine()
            {
                textMeshPro.text = "";
                yield break;
            }

            StartCoroutine(Coroutine());
        }

        public void RenderLoading()
        {
            StopAllCoroutines();

            IEnumerator Coroutine()
            {
                textMeshPro.color = colorLoading;
                for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
                {
                    textMeshPro.text = "NOW LOADING";
                    yield return new WaitForSeconds(loadingCycle);
                    textMeshPro.text = ". NOW LOADING .";
                    yield return new WaitForSeconds(loadingCycle);
                    textMeshPro.text = ".. NOW LOADING ..";
                    yield return new WaitForSeconds(loadingCycle);
                    textMeshPro.text = "... NOW LOADING ...";
                    yield return new WaitForSeconds(loadingCycle);
                }
            }

            StartCoroutine(Coroutine());
        }

        public void RenderFailed()
        {
            StopAllCoroutines();

            IEnumerator Coroutine()
            {
                textMeshPro.color = colorFailed;
                textMeshPro.text = "FAILED TO LOAD";
                yield break;
            }

            StartCoroutine(Coroutine());
        }
    }
}