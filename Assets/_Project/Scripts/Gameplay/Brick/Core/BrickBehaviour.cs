namespace MiniIT.ARKANOID
{
    public class BrickBehaviour
    {
        private readonly BrickConfig   config;
        private readonly BrickView     view;
        private readonly GameSession   gameSession;
        private readonly GameplayAudio gameplayAudio;
    
        private int                    currentHitPoints;

        public BrickBehaviour(BrickConfig config, BrickView view, GameSession gameSession, GameplayAudio gameplayAudio)
        {
            this.config = config;
            this.view = view;
            this.gameSession = gameSession;
            this.gameplayAudio = gameplayAudio;
        
            currentHitPoints = config.IsIndestructible ? int.MaxValue : this.config.HitPoints;
        
            this.view.ApplyVisual(this.config);
        }

        public void Hit()
        {
            if (config.IsIndestructible)
            {
                return;
            }

            currentHitPoints--;

            if (currentHitPoints > 0)
            {
                view.PlayHitShake();
            
                return;
            }

            gameplayAudio.PlayBrickDestroyed();
            gameSession.OnBrickDestroyed(config.ScoreReward);
            view.PlayDestroySequence();
        }
    }
}