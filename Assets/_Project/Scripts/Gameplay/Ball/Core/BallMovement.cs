using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallMovement
    {
        private readonly GameSession _gameSession;
        private readonly BallConfig  _config;
        private readonly BallState   _state;
        private readonly BallBody    _body;
        private readonly BallPhysics _physics;
        
        private readonly Collider2D  _bottomLoseTrigger;
    
        public BallMovement(Rigidbody2D rigidbody, CircleCollider2D ballCollider, Collider2D bottomLoseTrigger,
            Transform spawnPoint, BallConfig config, GameSession gameSession, GameplayAudio gameplayAudio)
        {
            _config = config;
            _gameSession = gameSession;
            _bottomLoseTrigger = bottomLoseTrigger;
    
            _state = new BallState(_config);
            _body = new BallBody(rigidbody, ballCollider, spawnPoint);
            _physics = new BallPhysics(_config, _state, _body, _gameSession, gameplayAudio);
    
            _state.Reset();
            _body.MoveToSpawnPoint();
    
            _gameSession.LevelCompleted += OnLevelCompleted;
        }
    
        public void OnTriggerEnter(Collider2D other)
        {
            if (other != _bottomLoseTrigger || _gameSession.IsGameOver)
            {
                return;
            }
    
            _state.Stop();
            _body.MoveToSpawnPoint();
            _gameSession.OnBallLost();
        }
        
        public void FixedTick(float deltaTime)
        {
            if (!_state.IsLaunched || _gameSession.IsGameOver)
            {
                _body.FollowSpawnPoint();
                
                return;
            }
    
            _physics.SimulateStep(deltaTime);
        }
    
        public void Launch()
        {
            if (_gameSession.IsGameOver)
            {
                return;
            }
    
            if (_state.IsLaunched)
            {
                return;
            }
    
            _state.Launch();
        }
    
        private void OnLevelCompleted()
        {
            _state.Stop();
            _body.MoveToSpawnPoint();
        }
    }
}