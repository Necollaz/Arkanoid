using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class GameHudPresenter
    {
        private readonly GameSession         gameSession;
        private readonly GameHudUiReferences ui;
        private readonly GameplayAudio       gameplayAudio;
        private readonly GameHudAnimator     animator;

        public GameHudPresenter(GameSession gameSession, GameHudUiReferences ui, GameplayAudio gameplayAudio,
            GameHudAnimator animator)
        {
            this.gameSession = gameSession;
            this.ui = ui;
            this.gameplayAudio = gameplayAudio;
            this.animator = animator;
        }

        public void Initialize()
        {
            SetScoreText(0);

            ui.WinWindow.gameObject.SetActive(false);
            ui.LoseWindow.gameObject.SetActive(false);

            ui.WinRestartButton.onClick.AddListener(RestartLevel);
            ui.LoseRestartButton.onClick.AddListener(RestartLevel);
        }

        public void Subscribe()
        {
            gameSession.ScoreChanged += HandleScoreChanged;
            gameSession.LevelCompleted += HandleGameWon;
            gameSession.LevelFailed += HandleGameLost;
        }

        public void Unsubscribe()
        {
            gameSession.ScoreChanged -= HandleScoreChanged;
            gameSession.LevelCompleted -= HandleGameWon;
            gameSession.LevelFailed -= HandleGameLost;

            animator.KillAllTweens();
        }

        private void HandleScoreChanged(int score)
        {
            SetScoreText(score);
        }

        private void HandleGameWon()
        {
            ui.WinWindow.gameObject.SetActive(true);
            ui.WinScoreText.text = $"Score: {gameSession.Score}";
            gameplayAudio.PlayWinMenu();

            animator.PlayWinWindow(ui.WinWindow.rectTransform);
        }

        private void HandleGameLost()
        {
            ui.LoseWindow.gameObject.SetActive(true);
            ui.LoseScoreText.text = $"Score: {gameSession.Score}";
            gameplayAudio.PlayLoseMenu();

            animator.PlayLoseWindow(ui.LoseWindow.rectTransform);
        }

        private void SetScoreText(int score)
        {
            ui.ScoreText.text = score.ToString();
        }

        private void RestartLevel()
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}