using UnityEngine;
#if TMP_PRESENT
using TMPro;
#endif

public class ScoreHUD : MonoBehaviour
{
#if TMP_PRESENT
    [SerializeField] private TMP_Text playerAndScoreText;
    [SerializeField] private TMP_Text highScoreText;
#else
    [SerializeField] private UnityEngine.UI.Text playerAndScoreText;
    [SerializeField] private UnityEngine.UI.Text highScoreText;
#endif

    private void Start()
    {
        GameSession.Ensure();
        RefreshAll();
    }

    private void Update()
    {
        // Cheap, reliable refresh; for bigger projects, event this instead.
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