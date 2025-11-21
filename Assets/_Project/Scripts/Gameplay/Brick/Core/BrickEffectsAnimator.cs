using UnityEngine;
using DG.Tweening;

namespace MiniIT.ARKANOID
{
    public class BrickEffectsAnimator
    {
        private const int                          HIT_SHAKE_VIBRATO = 10;
        private const float                        HIT_SHAKE_DURATION = 0.15f;
        private const float                        HIT_SHAKE_STRENGTH = 0.1f;
        private const float                        HIT_SHAKE_RANDOMNESS = 90.0f;
        private const float                        DESTROY_SCALE_DURATION = 0.2f;
        
        private readonly BrickView                 view;
        private readonly Transform                 transform;
        private readonly BoxCollider2D             boxCollider;

        private ObjectPool<BrickView>              brickPool;
        private ObjectPool<BrickDestroyEffectView> effectPool;

        private Tween                              currentTween;
        private Vector3                            originalLocalScale;

        public BrickEffectsAnimator(BrickView view, Transform transform, BoxCollider2D boxCollider)
        {
            this.view = view;
            this.transform = transform;
            this.boxCollider = boxCollider;

            originalLocalScale = this.transform.localScale;
        }

        public void Initialize(ObjectPool<BrickView> brickPool, ObjectPool<BrickDestroyEffectView> effectPool)
        {
            this.brickPool = brickPool;
            this.effectPool = effectPool;
            originalLocalScale = transform.localScale;
        }

        public void OnDisable()
        {
            KillTween();
        }

        public void ResetState()
        {
            KillTween();

            transform.localScale = originalLocalScale;

            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }
        }

        public void TryPlayHit()
        {
            if (transform == null || !transform.gameObject.activeInHierarchy)
            {
                return;
            }

            KillTween();

            currentTween = transform.DOShakePosition(HIT_SHAKE_DURATION, HIT_SHAKE_STRENGTH, HIT_SHAKE_VIBRATO,
                    HIT_SHAKE_RANDOMNESS, false, false).SetLink(transform.gameObject);
        }

        public void TryPlayDestroy()
        {
            GameObject gameObject = transform.gameObject;

            if (gameObject == null || !gameObject.activeInHierarchy)
            {
                ReturnToPool();
                
                return;
            }

            KillTween();

            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }

            if (effectPool != null)
            {
                BrickDestroyEffectView effect = effectPool.Get();

                if (effect != null)
                {
                    effect.Initialize(ReturnEffectToPool);
                    effect.TryPlay(transform.position);
                }
            }

            currentTween = transform.DOScale(Vector3.zero, DESTROY_SCALE_DURATION).SetEase(Ease.InBack)
                .OnComplete(ReturnToPool).SetLink(gameObject);
        }
        
        private void ReturnEffectToPool(BrickDestroyEffectView effect)
        {
            if (effect == null || effectPool == null)
            {
                return;
            }

            effectPool.Return(effect);
        }

        private void ReturnToPool()
        {
            transform.localScale = originalLocalScale;

            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }

            currentTween = null;

            if (brickPool != null && view != null)
            {
                brickPool.Return(view);
            }
            else if (transform != null)
            {
                transform.gameObject.SetActive(false);
            }
        }

        private void KillTween()
        {
            if (currentTween == null)
            {
                return;
            }

            currentTween.Kill();
            currentTween = null;
        }
    }
}