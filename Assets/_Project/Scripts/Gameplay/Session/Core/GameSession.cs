using System;

namespace MiniIT.ARKANOID
{
    public class GameSession
    {
        public event Action<int> ScoreChanged;
        public event Action      LevelCompleted;
        public event Action      LevelFailed;
    
        private int              _score;
        private int              _remainingDestructibleBricks;
        private bool             _isGameOver;
    
        public int Score => _score;
        public bool IsGameOver => _isGameOver;
    
        public void RegisterBrick(BrickConfig brickConfig)
        {
            if (brickConfig == null)
            {
                return;
            }
    
            if (!brickConfig.IsIndestructible)
            {
                _remainingDestructibleBricks++;
            }
        }
        
        public void Reset()
        {
            _score = 0;
            _remainingDestructibleBricks = 0;
            _isGameOver = false;
        }
    
        public void OnBrickDestroyed(int scoreReward)
        {
            if (_isGameOver)
            {
                return;
            }
    
            _score += scoreReward;
            
            ScoreChanged?.Invoke(_score);
    
            if (_remainingDestructibleBricks > 0)
            {
                _remainingDestructibleBricks--;
            }
    
            if (_remainingDestructibleBricks <= 0)
            {
                _remainingDestructibleBricks = 0;
                _isGameOver = true;
                
                LevelCompleted?.Invoke();
            }
        }
    
        public void OnBallLost()
        {
            if (_isGameOver)
            {
                return;
            }
    
            _isGameOver = true;
            
            LevelFailed?.Invoke();
        }
    }
}