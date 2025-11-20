using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallPhysics
    {
        private readonly BallConfig    _config;
        private readonly BallState     _state;
        private readonly BallBody      _body;
        private readonly GameSession   _gameSession;
        private readonly GameplayAudio _gameplayAudio;

        public BallPhysics(BallConfig config, BallState state, BallBody body, GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            _config = config;
            _state = state;
            _body = body;
            _gameSession = gameSession;
            _gameplayAudio = gameplayAudio;
        }

        public void SimulateStep(float deltaTime)
        {
            Vector2 startPosition = _body.Position;
            float distance = _state.CurrentSpeed * deltaTime;

            RaycastHit2D hit = Physics2D.CircleCast(startPosition, _body.Radius, _state.Direction, distance,
                _config.CollisionMask);

            if (hit.collider != null && !hit.collider.isTrigger)
            {
                Vector2 hitPoint = hit.point;
                Vector2 normal = hit.normal;
                Vector2 positionAtHit = hitPoint + normal * _body.Radius;

                if (hit.collider.TryGetComponent(out PlatformView _))
                {
                    _gameplayAudio.PlayBallHitWallOrPlatform();
                    _state.ApplyPlatformBounce(hit.collider, positionAtHit);
                }
                else if (hit.collider.TryGetComponent(out BrickView brickView))
                {
                    _gameplayAudio.PlayBallHitBrick();
                    brickView.OnBallHit();

                    if (_gameSession.IsGameOver)
                    {
                        return;
                    }

                    _state.ApplyWallBounce(normal);
                }
                else
                {
                    _gameplayAudio.PlayBallHitWallOrPlatform();
                    _state.ApplyWallBounce(normal);
                }

                _body.MoveTo(positionAtHit);
            }
            else
            {
                Vector2 newPosition = startPosition + _state.Direction * distance;
                _body.MoveTo(newPosition);
            }
        }
    }
}