using UnityEngine;

public class ClearHighScoreOnce : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey("HighScore_Value");
        PlayerPrefs.DeleteKey("HighScore_Name");
        PlayerPrefs.Save();

        Debug.Log("✅ High Score Reset!");
        Destroy(gameObject); // erase itself so it never runs again
    }
}
