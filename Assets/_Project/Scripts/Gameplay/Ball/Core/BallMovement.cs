using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallMovement
    {
        private readonly GameSession gameSession;
        private readonly BallConfig  config;
        private readonly BallState   state;
        private readonly BallBody    body;
        private readonly BallPhysics physics;
        
        private readonly Collider2D  bottomLoseTrigger;
    
        public BallMovement(
            Rigidbody2D rigidbody,
            CircleCollider2D ballCollider,
            Collider2D bottomLoseTrigger,
            Transform spawnPoint,
            BallConfig config,
            GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            this.config = config;
            this.gameSession = gameSession;
            this.bottomLoseTrigger = bottomLoseTrigger;
    
            state = new BallState(this.config);
            body = new BallBody(rigidbody, ballCollider, spawnPoint);
            physics = new BallPhysics(this.config, state, body, this.gameSession, gameplayAudio);
    
            state.Reset();
            body.MoveToSpawnPoint();
    
            this.gameSession.LevelCompleted += OnLevelCompleted;
        }
    
        public void OnTriggerEnter(Collider2D other)
        {
            if (other != bottomLoseTrigger || gameSession.IsGameOver)
            {
                return;
            }
    
            state.Stop();
            body.MoveToSpawnPoint();
            gameSession.OnBallLost();
        }
        
        public void FixedTick(float deltaTime)
        {
            if (!state.IsLaunched || gameSession.IsGameOver)
            {
                body.FollowSpawnPoint();
                
                return;
            }
    
            physics.SimulateStep(deltaTime);
        }
    
        public void Launch()
        {
            if (gameSession.IsGameOver)
            {
                return;
            }
    
            if (state.IsLaunched)
            {
                return;
            }
    
            state.Launch();
        }
    
        private void OnLevelCompleted()
        {
            state.Stop();
            body.MoveToSpawnPoint();
        }
    }
}