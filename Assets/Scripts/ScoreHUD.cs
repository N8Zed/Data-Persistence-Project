using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text playerAndScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        GameSession.Ensure();
        RefreshAll();
    }

    private void Update()
    {
        RefreshCurrent();
        RefreshHighScore();
    }

    private void RefreshAll()
    {
        RefreshCurrent();
        RefreshHighScore();
    }

    private void RefreshCurrent()
    {
        if (playerAndScoreText != null && GameSession.Instance != null)
        {
            playerAndScoreText.text = $"{GameSession.Instance.PlayerName} — Score: {GameSession.Instance.CurrentScore}";
        }
    }

    private void RefreshHighScore()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {HighScoreService.HighScore} by {HighScoreService.HighScoreName}";
        }
    }
}