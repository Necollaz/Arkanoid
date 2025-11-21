using UnityEngine;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class BrickView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer    spriteRenderer;
    
        private BrickBehaviour                     brickBehaviour;
        private BrickVisual                        visual;
        private BrickEffectsAnimator               effectsAnimator;
        private ObjectPool<BrickView>              brickPool;
        private ObjectPool<BrickDestroyEffectView> brickEffectPool;
        private BoxCollider2D                      boxCollider;
    
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
            
            visual = new BrickVisual(spriteRenderer, boxCollider, transform);
            effectsAnimator = new BrickEffectsAnimator(this, transform, boxCollider);
        }
    
        private void OnDisable()
        {
            effectsAnimator?.OnDisable();
        }
        
        public void Initialize(
            BrickConfig config,
            ObjectPool<BrickView> brickPool, 
            ObjectPool<BrickDestroyEffectView> effectPool,
            GameSession gameSession,
            GameplayAudio gameplayAudio,
            Vector2 cellSize)
        {
            this.brickPool = brickPool;
            brickEffectPool = effectPool;
            
            effectsAnimator.Initialize(this.brickPool, brickEffectPool);
            effectsAnimator.ResetState();
    
            visual.SetSize(cellSize);
            
            brickBehaviour = new BrickBehaviour(config, this, gameSession, gameplayAudio);
        }
    
        public void ApplyVisual(BrickConfig config)
        {
            visual.ApplyVisual(config);
        }
        
        public void OnBallHit()
        {
            brickBehaviour?.Hit();
        }
        
        public void PlayHitShake()
        {
            effectsAnimator?.TryPlayHit();
        }
    
        public void PlayDestroySequence()
        {
            effectsAnimator?.TryPlayDestroy();
        }
    }
}