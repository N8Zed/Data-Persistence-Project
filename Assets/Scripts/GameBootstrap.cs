using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    private void Awake()
    {
        GameSession.Ensure();
        GameSession.Instance.ResetScore();   // ← fresh run
    }
}