using UnityEngine;
using DG.Tweening;

namespace MiniIT.ARKANOID
{
    public class BrickEffectsAnimator
    {
        private readonly BrickView                 _view;
        private readonly Transform                 _transform;
        private readonly BoxCollider2D             _boxCollider;

        private ObjectPool<BrickView>              _brickPool;
        private ObjectPool<BrickDestroyEffectView> _effectPool;

        private Tween                              _currentTween;
        private Vector3                            _originalLocalScale;

        public BrickEffectsAnimator(BrickView view, Transform transform, BoxCollider2D boxCollider)
        {
            _view = view;
            _transform = transform;
            _boxCollider = boxCollider;

            _originalLocalScale = _transform.localScale;
        }

        public void Initialize(ObjectPool<BrickView> brickPool, ObjectPool<BrickDestroyEffectView> effectPool)
        {
            _brickPool = brickPool;
            _effectPool = effectPool;
            _originalLocalScale = _transform.localScale;
        }

        public void OnDisable()
        {
            KillTween();
        }

        public void ResetState()
        {
            KillTween();

            _transform.localScale = _originalLocalScale;

            if (_boxCollider != null)
            {
                _boxCollider.enabled = true;
            }
        }

        public void TryPlayHit()
        {
            if (_transform == null || !_transform.gameObject.activeInHierarchy)
            {
                return;
            }

            KillTween();

            _currentTween = _transform
                .DOShakePosition(0.15f, 0.1f, 10, 90.0f, false, false)
                .SetLink(_transform.gameObject);
        }

        public void TryPlayDestroy()
        {
            GameObject gameObject = _transform.gameObject;

            if (gameObject == null || !gameObject.activeInHierarchy)
            {
                ReturnToPool();
                return;
            }

            KillTween();

            if (_boxCollider != null)
            {
                _boxCollider.enabled = false;
            }

            if (_effectPool != null)
            {
                BrickDestroyEffectView effect = _effectPool.Get();

                if (effect != null)
                {
                    effect.TryPlay(_transform.position);
                }
            }

            _currentTween = _transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack)
                .OnComplete(ReturnToPool).SetLink(gameObject);
        }

        private void ReturnToPool()
        {
            _transform.localScale = _originalLocalScale;

            if (_boxCollider != null)
            {
                _boxCollider.enabled = true;
            }

            _currentTween = null;

            if (_brickPool != null && _view != null)
            {
                _brickPool.Return(_view);
            }
            else if (_transform != null)
            {
                _transform.gameObject.SetActive(false);
            }
        }

        private void KillTween()
        {
            if (_currentTween == null)
            {
                return;
            }

            _currentTween.Kill();
            _currentTween = null;
        }
    }
}