using System;

namespace MiniIT.ARKANOID
{
    public class GameSession
    {
        public event Action<int> ScoreChanged;
        public event Action      LevelCompleted;
        public event Action      LevelFailed;
    
        private int              score;
        private int              remainingDestructibleBricks;
        private bool             isGameOver;
    
        public int Score => score;
        public bool IsGameOver => isGameOver;
    
        public void RegisterBrick(BrickConfig brickConfig)
        {
            if (brickConfig == null)
            {
                return;
            }
    
            if (!brickConfig.IsIndestructible)
            {
                remainingDestructibleBricks++;
            }
        }
        
        public void Reset()
        {
            score = 0;
            remainingDestructibleBricks = 0;
            isGameOver = false;
        }
    
        public void OnBrickDestroyed(int scoreReward)
        {
            if (isGameOver)
            {
                return;
            }
    
            score += scoreReward;
            
            ScoreChanged?.Invoke(score);
    
            if (remainingDestructibleBricks > 0)
            {
                remainingDestructibleBricks--;
            }
    
            if (remainingDestructibleBricks <= 0)
            {
                remainingDestructibleBricks = 0;
                isGameOver = true;
                
                LevelCompleted?.Invoke();
            }
        }
    
        public void OnBallLost()
        {
            if (isGameOver)
            {
                return;
            }
    
            isGameOver = true;
            
            LevelFailed?.Invoke();
        }
    }
}