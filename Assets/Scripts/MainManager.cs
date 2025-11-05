using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    public GameObject NewPlayerButton;
    public GameObject RestartButton;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        var bestName = HighScoreService.HighScoreName;
        var bestScore = HighScoreService.HighScore;
        ScoreText.text = $"Best Score : {bestName} : {bestScore}";

        m_Points = 0;
        ScoreText.text = "Score : 0";

        // Update the best score header from the persistent service
        BestScoreText.text = $"Best Score : {HighScoreService.HighScoreName} : {HighScoreService.HighScore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        // NEW: tell the session so your persistent system knows
        GameSession.Ensure();
        GameSession.Instance.AddScore(point);   // send the delta, not the total
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        NewPlayerButton.SetActive(true);
        RestartButton.SetActive(true);

        GameSession.Ensure();
        var playerName = GameSession.Instance.PlayerName;
        var wasHigh = HighScoreService.TrySubmit(m_Points, playerName);
        if (wasHigh)
        {
            // refresh the “Best Score” line if you show it on this scene
            BestScoreText.text = $"Best Score : {playerName} : {HighScoreService.HighScore}";
        }
    }

    public void LoadMenu()
    {
        // Replace "StartMenu" with your actual menu scene name
        SceneManager.LoadScene("menu");
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame() clicked");   // should print when you click
        m_Points = 0;
        GameSession.Ensure();
        GameSession.Instance.ResetScore();

        var current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
