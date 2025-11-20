using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BrickVisual
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly BoxCollider2D  _boxCollider;
        private readonly Transform      _transform;
    
        public BrickVisual(SpriteRenderer spriteRenderer, BoxCollider2D boxCollider, Transform transform)
        {
            _spriteRenderer = spriteRenderer;
            _boxCollider = boxCollider;
            _transform = transform;
        }
    
        public void ApplyVisual(BrickConfig config)
        {
            if (_spriteRenderer == null || config == null)
            {
                return;
            }
    
            _spriteRenderer.sprite = config.Sprite;
            _spriteRenderer.color = config.TintColor;
    
            TryUpdateColliderSize();
        }
    
        public void SetSize(Vector2 cellSize)
        {
            if (_spriteRenderer == null || _spriteRenderer.sprite == null)
            {
                return;
            }
    
            Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
    
            if (spriteSize.x <= 0.0f || spriteSize.y <= 0.0f)
            {
                return;
            }
    
            Vector3 localScale = _transform.localScale;
    
            float scaleX = cellSize.x / spriteSize.x;
            float scaleY = cellSize.y / spriteSize.y;
    
            _transform.localScale = new Vector3(scaleX, scaleY, localScale.z);
    
            TryUpdateColliderSize();
        }
    
        public void ResetCollider()
        {
            if (_boxCollider == null)
            {
                return;
            }
    
            _boxCollider.enabled = true;
        }
    
        public void DisableCollider()
        {
            if (_boxCollider == null)
            {
                return;
            }
    
            _boxCollider.enabled = false;
        }
    
        private void TryUpdateColliderSize()
        {
            if (_boxCollider == null || _spriteRenderer == null || _spriteRenderer.sprite == null)
            {
                return;
            }
    
            Bounds spriteBounds = _spriteRenderer.sprite.bounds;
            _boxCollider.size = spriteBounds.size;
            _boxCollider.offset = spriteBounds.center;
        }
    }
}