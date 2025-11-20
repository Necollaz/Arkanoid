using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BrickDestroyEffectView : MonoBehaviour
    {
        private Action<BrickDestroyEffectView>  _returnToPool;
    
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_particleSystem == null)
            {
                return;
            }

            if (!_particleSystem.IsAlive(true))
            {
                _returnToPool?.Invoke(this);
            }
        }
    
        public void TryPlay(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            if (_particleSystem == null)
            {
                return;
            }

            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _particleSystem.Play(true);
        }
    }
}