using System;
using UnityEngine;
using UnityEngine.Events;

public class GameOverNotifier : MonoBehaviour
{
    public event Action OnGameOver;
    public event Action OnFailDisplay;

    public void GameOver()
    {
        NotifyEnemiesGameOver();
        NotifyDisplayGameOver();
    }
    
    private void NotifyEnemiesGameOver()
    {
        OnGameOver?.Invoke();
    }
    
    private void NotifyDisplayGameOver()
    {
        OnFailDisplay?.Invoke();
    }
    
    

}