using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
    public const string LastPlayerNameKey = "LastPlayerName";

    public string PlayerName { get; private set; } = "Player";
    public int CurrentScore { get; private set; } = 0;

    public static void Ensure()
    {
        if (Instance != null) return;
        var go = new GameObject("GameSession");
        Instance = go.AddComponent<GameSession>();
        DontDestroyOnLoad(go);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string name)
    {
        PlayerName = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        PlayerPrefs.SetString(LastPlayerNameKey, PlayerName);
        PlayerPrefs.Save();
    }

    public void AddScore(int delta)
    {
        if (delta == 0) return;
        CurrentScore = Mathf.Max(0, CurrentScore + delta);
        HighScoreService.TrySubmit(CurrentScore, PlayerName);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}