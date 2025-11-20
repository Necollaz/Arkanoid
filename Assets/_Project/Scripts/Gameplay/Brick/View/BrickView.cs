using UnityEngine;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class BrickView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer    _spriteRenderer;
    
        private BrickBehaviour                     _brickBehaviour;
        private BrickVisual                        _visual;
        private BrickEffectsAnimator               _effectsAnimator;
        private ObjectPool<BrickView>              _brickPool;
        private ObjectPool<BrickDestroyEffectView> _brickEffectPool;
        private BoxCollider2D                      _boxCollider;
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
            
            _visual = new BrickVisual(_spriteRenderer, _boxCollider, transform);
            _effectsAnimator = new BrickEffectsAnimator(this, transform, _boxCollider);
        }
    
        private void OnDisable()
        {
            _effectsAnimator?.OnDisable();
        }
        
        public void Initialize(BrickConfig config, ObjectPool<BrickView> brickPool, 
            ObjectPool<BrickDestroyEffectView> effectPool, GameSession gameSession, GameplayAudio gameplayAudio,
            Vector2 cellSize)
        {
            _brickPool = brickPool;
            _brickEffectPool = effectPool;
            
            _effectsAnimator.Initialize(_brickPool, _brickEffectPool);
            _effectsAnimator.ResetState();
    
            _visual.SetSize(cellSize);
            
            _brickBehaviour = new BrickBehaviour(config, this, gameSession, gameplayAudio);
        }
    
        public void ApplyVisual(BrickConfig config)
        {
            _visual.ApplyVisual(config);
        }
        
        public void OnBallHit()
        {
            _brickBehaviour?.Hit();
        }
        
        public void PlayHitShake()
        {
            _effectsAnimator?.TryPlayHit();
        }
    
        public void PlayDestroySequence()
        {
            _effectsAnimator?.TryPlayDestroy();
        }
    }
}