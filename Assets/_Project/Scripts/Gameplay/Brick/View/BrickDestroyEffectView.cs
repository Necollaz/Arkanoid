using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BrickDestroyEffectView : MonoBehaviour
    {
        private Action<BrickDestroyEffectView>  returnToPool;
    
        [SerializeField] private ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (particleSystem == null)
            {
                return;
            }

            if (!particleSystem.IsAlive(true))
            {
                returnToPool?.Invoke(this);
            }
        }
        
        public void Initialize(Action<BrickDestroyEffectView> returnToPool)
        {
            this.returnToPool = returnToPool;
        }
    
        public void TryPlay(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            if (particleSystem == null)
            {
                return;
            }

            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.Play(true);
        }
    }
}