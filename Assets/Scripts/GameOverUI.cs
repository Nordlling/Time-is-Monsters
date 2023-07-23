using TMPro;
using UnityEngine;
using Zenject;

public class GameOverUI : MonoBehaviour
{
    [Inject] private GameOverNotifier gameOverNotifier;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        gameOverNotifier.OnFailDisplay += UpdateUI;
    }
    
    private void OnDisable()
    {
        gameOverNotifier.OnFailDisplay -= UpdateUI;
    }

    private void UpdateUI(int score)
    {
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
    }
}
