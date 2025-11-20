using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallState
    {
        private const float         HALF_WIDTH_MULTIPLIER = 0.5f;

        private readonly BallConfig _config;

        private Vector2             _direction;
        private float               _currentSpeed;
        private bool                _isLaunched;

        public BallState(BallConfig config)
        {
            _config = config;

            Reset();
        }

        public Vector2 Direction => _direction;
        public float   CurrentSpeed => _currentSpeed;
        public bool    IsLaunched => _isLaunched;

        public void Reset()
        {
            _currentSpeed = _config.BallSpeed;
            _direction = Vector2.zero;
            _isLaunched = false;
        }

        public void Stop()
        {
            _currentSpeed = _config.BallSpeed;
            _direction = Vector2.zero;
            _isLaunched = false;
        }

        public void Launch()
        {
            if (_isLaunched)
            {
                return;
            }

            float randomX = Random.Range(-_config.LaunchHorizontalRandomRange, _config.LaunchHorizontalRandomRange);
            _direction = new Vector2(randomX, _config.LaunchVerticalDirection).normalized;
            _currentSpeed = _config.BallSpeed;
            _isLaunched = true;
        }

        public void ApplyWallBounce(in Vector2 normal)
        {
            Vector2 reflected = Vector2.Reflect(_direction, normal).normalized;

            if (Mathf.Abs(reflected.y) < _config.MinVerticalNormal)
            {
                reflected.y = Mathf.Sign(reflected.y) * _config.MinVerticalNormal;
                reflected = reflected.normalized;
            }

            _direction = reflected;
        }

        public void ApplyPlatformBounce(Collider2D platformCollider, Vector2 hitPosition)
        {
            Transform platformTransform = platformCollider.transform;
            float platformWidth = platformCollider.bounds.size.x;
            float platformX = platformTransform.position.x;

            float hitPositionX = (hitPosition.x - platformX) / (platformWidth * HALF_WIDTH_MULTIPLIER);
            float maxNormalizedX = _config.PlatformHitMaxNormalizedX;
            hitPositionX = Mathf.Clamp(hitPositionX, -maxNormalizedX, maxNormalizedX);

            Vector2 direction = new Vector2(hitPositionX, _config.LaunchVerticalDirection).normalized;

            if (Mathf.Abs(direction.y) < _config.MinVerticalNormal)
            {
                direction.y = _config.MinVerticalNormal;
                direction = direction.normalized;
            }

            _direction = direction;
        }
    }
}