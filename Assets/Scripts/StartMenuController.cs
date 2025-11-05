using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private TMP_Text bestScoreText;

    [Header("Scenes")]
    [SerializeField] private string mainSceneName = "main";

    private void Awake()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = $"Best Score : {HighScoreService.HighScoreName} : {HighScoreService.HighScore}";
        }

        if (warningText) warningText.text = "";

        var lastName = PlayerPrefs.GetString(GameSession.LastPlayerNameKey, "");
        if (!string.IsNullOrEmpty(lastName) && nameField) nameField.text = lastName;
    }

    public void OnClickStart()
    {
        var entered = (nameField != null ? nameField.text : "").Trim();
        if (string.IsNullOrEmpty(entered))
        {
            if (warningText) warningText.text = "Please enter a name.";
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(mainSceneName))
        {
            Debug.LogError($"Scene '{mainSceneName}' not in Build Settings.");
            return;
        }

        GameSession.Ensure();
        GameSession.Instance.SetPlayerName(entered);
        GameSession.Instance.ResetScore();

        SceneManager.LoadScene(mainSceneName);
    }

    // ---------------------------
    //  ADD THIS ↓
    // ---------------------------
    public void OnClickQuit()
    {
        Debug.Log("Quit button clicked");

        Application.Quit();

#if UNITY_EDITOR
        // So quitting actually works inside the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}