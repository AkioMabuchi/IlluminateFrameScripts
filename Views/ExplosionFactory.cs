using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Views.Instances;

namespace Views
{
    public class ExplosionFactory : MonoBehaviour
    {
        [SerializeField] private Explosion prefabExplosion;
        [SerializeField] private int preloadExplosionCount;

        private ObjectPool<Explosion> _objectPoolExplosion;

        private void Awake()
        {
            _objectPoolExplosion = new ObjectPool<Explosion>(() =>
                {
                    var explosion = Instantiate(prefabExplosion, transform);
                    explosion.OnFinished.Subscribe(finishedExplosion =>
                    {
                        _objectPoolExplosion.Release(finishedExplosion);
                    }).AddTo(explosion.gameObject);
                    return explosion;
                }, takenExplosion => takenExplosion.Take(),
                releasedExplosion => releasedExplosion.Release(),
                destroyedExplosion => Destroy(destroyedExplosion.gameObject));
        }

        private void Start()
        {
            var preloadedExplosions = new List<Explosion>();

            for (var i = 0; i < preloadExplosionCount; i++)
            {
                preloadedExplosions.Add(_objectPoolExplosion.Get());
            }

            foreach (var preloadedExplosion in preloadedExplosions)
            {
                _objectPoolExplosion.Release(preloadedExplosion);
            }
        }

        public void GenerateExplosion()
        {
            var explosion = _objectPoolExplosion.Get();
            explosion.BeginEffect();
        }
    }
}