using UnityEngine;

public static class HighScoreService
{
    private const string HighScoreKey = "HighScore_Value";
    private const string HighScoreNameKey = "HighScore_Name";

    public static int HighScore => PlayerPrefs.GetInt(HighScoreKey, 0);
    public static string HighScoreName => PlayerPrefs.GetString(HighScoreNameKey, "—");

    /// <summary>
    /// Returns true if a new high score was recorded.
    /// </summary>
    public static bool TrySubmit(int score, string playerName)
    {
        if (score > HighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.SetString(HighScoreNameKey, string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}