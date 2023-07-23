using System;
using UnityEngine;

public class GameOverNotifier : MonoBehaviour
{
    public event Action<int> OnFailDisplay;

    public void GameOver(int score)
    {
        NotifyDisplayGameOver(score);
    }
    
    private void NotifyDisplayGameOver(int score)
    {
        OnFailDisplay?.Invoke(score);
    }
}