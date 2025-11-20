using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class GameHudPresenter
    {
        private readonly GameSession         _gameSession;
        private readonly GameHudUiReferences _ui;
        private readonly GameplayAudio       _gameplayAudio;
        private readonly GameHudAnimator     _animator;

        public GameHudPresenter(GameSession gameSession, GameHudUiReferences ui, GameplayAudio gameplayAudio,
            GameHudAnimator animator)
        {
            _gameSession = gameSession;
            _ui = ui;
            _gameplayAudio = gameplayAudio;
            _animator = animator;
        }

        public void Initialize()
        {
            SetScoreText(0);

            _ui.WinWindow.gameObject.SetActive(false);
            _ui.LoseWindow.gameObject.SetActive(false);

            _ui.WinRestartButton.onClick.AddListener(RestartLevel);
            _ui.LoseRestartButton.onClick.AddListener(RestartLevel);
        }

        public void Subscribe()
        {
            _gameSession.ScoreChanged += HandleScoreChanged;
            _gameSession.LevelCompleted += HandleGameWon;
            _gameSession.LevelFailed += HandleGameLost;
        }

        public void Unsubscribe()
        {
            _gameSession.ScoreChanged -= HandleScoreChanged;
            _gameSession.LevelCompleted -= HandleGameWon;
            _gameSession.LevelFailed -= HandleGameLost;

            _animator.KillAllTweens();
        }

        private void HandleScoreChanged(int score)
        {
            SetScoreText(score);
        }

        private void HandleGameWon()
        {
            _ui.WinWindow.gameObject.SetActive(true);
            _ui.WinScoreText.text = $"Score: {_gameSession.Score}";
            _gameplayAudio.PlayWinMenu();

            _animator.PlayWinWindow(_ui.WinWindow.rectTransform);
        }

        private void HandleGameLost()
        {
            _ui.LoseWindow.gameObject.SetActive(true);
            _ui.LoseScoreText.text = $"Score: {_gameSession.Score}";
            _gameplayAudio.PlayLoseMenu();

            _animator.PlayLoseWindow(_ui.LoseWindow.rectTransform);
        }

        private void SetScoreText(int score)
        {
            _ui.ScoreText.text = score.ToString();
        }

        private void RestartLevel()
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}