using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallState
    {
        private const float         HALF_WIDTH_MULTIPLIER = 0.5f;

        private readonly BallConfig config;

        private Vector2             direction;
        private float               currentSpeed;
        private bool                isLaunched;

        public BallState(BallConfig config)
        {
            this.config = config;

            Reset();
        }

        public Vector2 Direction => direction;
        public float   CurrentSpeed => currentSpeed;
        public bool    IsLaunched => isLaunched;

        public void Reset()
        {
            currentSpeed = config.BallSpeed;
            direction = Vector2.zero;
            isLaunched = false;
        }

        public void Stop()
        {
            currentSpeed = config.BallSpeed;
            direction = Vector2.zero;
            isLaunched = false;
        }

        public void Launch()
        {
            if (isLaunched)
            {
                return;
            }

            float randomX = Random.Range(-config.LaunchHorizontalRandomRange, config.LaunchHorizontalRandomRange);
            direction = new Vector2(randomX, config.LaunchVerticalDirection).normalized;
            currentSpeed = config.BallSpeed;
            isLaunched = true;
        }

        public void ApplyWallBounce(in Vector2 normal)
        {
            Vector2 reflected = Vector2.Reflect(direction, normal).normalized;

            if (Mathf.Abs(reflected.y) < config.MinVerticalNormal)
            {
                reflected.y = Mathf.Sign(reflected.y) * config.MinVerticalNormal;
                reflected = reflected.normalized;
            }

            direction = reflected;
        }

        public void ApplyPlatformBounce(Collider2D platformCollider, Vector2 hitPosition)
        {
            Transform platformTransform = platformCollider.transform;
            float platformWidth = platformCollider.bounds.size.x;
            float platformX = platformTransform.position.x;

            float hitPositionX = (hitPosition.x - platformX) / (platformWidth * HALF_WIDTH_MULTIPLIER);
            float maxNormalizedX = config.PlatformHitMaxNormalizedX;
            hitPositionX = Mathf.Clamp(hitPositionX, -maxNormalizedX, maxNormalizedX);

            Vector2 direction = new Vector2(hitPositionX, config.LaunchVerticalDirection).normalized;

            if (Mathf.Abs(direction.y) < config.MinVerticalNormal)
            {
                direction.y = config.MinVerticalNormal;
                direction = direction.normalized;
            }

            this.direction = direction;
        }
    }
}