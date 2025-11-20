using UnityEngine.UI;
using TMPro;

namespace MiniIT.ARKANOID
{
    public class GameHudUiReferences
    {
        public GameHudUiReferences(TextMeshProUGUI scoreText, Image winWindow, TextMeshProUGUI winScoreText,
            Button winRestartButton, Image loseWindow, TextMeshProUGUI loseScoreText, Button loseRestartButton)
        {
            ScoreText = scoreText;
            WinWindow = winWindow;
            WinScoreText = winScoreText;
            WinRestartButton = winRestartButton;
            LoseWindow = loseWindow;
            LoseScoreText = loseScoreText;
            LoseRestartButton = loseRestartButton;
        }
    
        public Image           LoseWindow { get; }
        public Image           WinWindow { get; }
        public TextMeshProUGUI ScoreText { get; }
        public TextMeshProUGUI WinScoreText { get; }
        public TextMeshProUGUI LoseScoreText { get; }
        public Button          WinRestartButton { get; }
        public Button          LoseRestartButton { get; }
    }
}