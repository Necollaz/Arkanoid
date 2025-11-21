using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BrickVisual
    {
        private readonly SpriteRenderer spriteRenderer;
        private readonly BoxCollider2D  boxCollider;
        private readonly Transform      transform;
    
        public BrickVisual(SpriteRenderer spriteRenderer, BoxCollider2D boxCollider, Transform transform)
        {
            this.spriteRenderer = spriteRenderer;
            this.boxCollider = boxCollider;
            this.transform = transform;
        }
    
        public void ApplyVisual(BrickConfig config)
        {
            if (spriteRenderer == null || config == null)
            {
                return;
            }
    
            spriteRenderer.sprite = config.Sprite;
            spriteRenderer.color = config.TintColor;
    
            TryUpdateColliderSize();
        }
    
        public void SetSize(Vector2 cellSize)
        {
            if (spriteRenderer == null || spriteRenderer.sprite == null)
            {
                return;
            }
    
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
    
            if (spriteSize.x <= 0.0f || spriteSize.y <= 0.0f)
            {
                return;
            }
    
            Vector3 localScale = transform.localScale;
    
            float scaleX = cellSize.x / spriteSize.x;
            float scaleY = cellSize.y / spriteSize.y;
    
            transform.localScale = new Vector3(scaleX, scaleY, localScale.z);
    
            TryUpdateColliderSize();
        }
    
        private void TryUpdateColliderSize()
        {
            if (boxCollider == null || spriteRenderer == null || spriteRenderer.sprite == null)
            {
                return;
            }
    
            Bounds spriteBounds = spriteRenderer.sprite.bounds;
            boxCollider.size = spriteBounds.size;
            boxCollider.offset = spriteBounds.center;
        }
    }
}