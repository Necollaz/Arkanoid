using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallPhysics
    {
        private readonly BallConfig    config;
        private readonly BallState     state;
        private readonly BallBody      body;
        private readonly GameSession   gameSession;
        private readonly GameplayAudio gameplayAudio;

        public BallPhysics(
            BallConfig config,
            BallState state,
            BallBody body,
            GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            this.config = config;
            this.state = state;
            this.body = body;
            this.gameSession = gameSession;
            this.gameplayAudio = gameplayAudio;
        }

        public void SimulateStep(float deltaTime)
        {
            Vector2 startPosition = body.Position;
            float distance = state.CurrentSpeed * deltaTime;

            RaycastHit2D hit = Physics2D.CircleCast(startPosition, body.Radius, state.Direction, distance,
                config.CollisionMask);

            if (hit.collider != null && !hit.collider.isTrigger)
            {
                Vector2 hitPoint = hit.point;
                Vector2 normal = hit.normal;
                Vector2 positionAtHit = hitPoint + normal * body.Radius;

                if (hit.collider.TryGetComponent(out PlatformView _))
                {
                    gameplayAudio.PlayBallHitWallOrPlatform();
                    state.ApplyPlatformBounce(hit.collider, positionAtHit);
                }
                else if (hit.collider.TryGetComponent(out BrickView brickView))
                {
                    gameplayAudio.PlayBallHitBrick();
                    brickView.OnBallHit();

                    if (gameSession.IsGameOver)
                    {
                        return;
                    }

                    state.ApplyWallBounce(normal);
                }
                else
                {
                    gameplayAudio.PlayBallHitWallOrPlatform();
                    state.ApplyWallBounce(normal);
                }

                body.MoveTo(positionAtHit);
            }
            else
            {
                Vector2 newPosition = startPosition + state.Direction * distance;
                body.MoveTo(newPosition);
            }
        }
    }
}