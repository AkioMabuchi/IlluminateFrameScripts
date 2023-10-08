using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Views.Instances
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particleSystems;

        [SerializeField] private float duration;

        private readonly Subject<Explosion> _subjectOnFinished = new();
        public IObservable<Explosion> OnFinished => _subjectOnFinished;
        
        public void Take()
        {

        }

        public void Release()
        {

        }

        public void BeginEffect()
        {
            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(duration);
                _subjectOnFinished.OnNext(this);
            }
            
            foreach (var particle in particleSystems)
            {
                particle.time = 0.0f;
                particle.Play();
            }

            StartCoroutine(Coroutine());
        }
    }
}