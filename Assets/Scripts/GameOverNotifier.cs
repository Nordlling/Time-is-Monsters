using System;
using UnityEngine;
using UnityEngine.Events;

public class GameOverNotifier : MonoBehaviour
{
    public event Action OnGameOver;
    public event Action<int> OnFailDisplay;

    public void GameOver(int score)
    {
        NotifyEnemiesGameOver();
        NotifyDisplayGameOver(score);
    }
    
    private void NotifyEnemiesGameOver()
    {
        OnGameOver?.Invoke();
    }
    
    private void NotifyDisplayGameOver(int score)
    {
        OnFailDisplay?.Invoke(score);
    }
    
    

}