namespace MiniIT.ARKANOID
{
    public class BrickBehaviour
    {
        private readonly BrickConfig   _config;
        private readonly BrickView     _view;
        private readonly GameSession   _gameSession;
        private readonly GameplayAudio _gameplayAudio;
    
        private int                    _currentHitPoints;

        public BrickBehaviour(BrickConfig config, BrickView view, GameSession gameSession, GameplayAudio gameplayAudio)
        {
            _config = config;
            _view = view;
            _gameSession = gameSession;
            _gameplayAudio = gameplayAudio;
        
            _currentHitPoints = config.IsIndestructible ? int.MaxValue : _config.HitPoints;
        
            _view.ApplyVisual(_config);
        }

        public void Hit()
        {
            if (_config.IsIndestructible)
            {
                return;
            }

            _currentHitPoints--;

            if (_currentHitPoints > 0)
            {
                _view.PlayHitShake();
            
                return;
            }

            _gameplayAudio.PlayBrickDestroyed();
            _gameSession.OnBrickDestroyed(_config.ScoreReward);
            _view.PlayDestroySequence();
        }
    }
}