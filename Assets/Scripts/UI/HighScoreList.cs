using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private Transform recordListPanel;
    [SerializeField] private GameObject recordPrefab;
    
    private List<HighScore> _highScores;
    
    private void Start()
    {
        LoadHighScores();
        DisplayHighScores();
    }
    
    private void LoadHighScores()
    { 
        _highScores = SaveManager.LoadHighScores().OrderByDescending(score => score.score).ToList();
    }
    
    private void DisplayHighScores()
    {
        foreach (Transform child in recordListPanel)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < _highScores.Count; i++)
        {
            GameObject recordObject = Instantiate(recordPrefab, recordListPanel);

            TextMeshProUGUI numberText = recordObject.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dateText = recordObject.transform.Find("DateText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = recordObject.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            numberText.text = (i + 1) + ".";
            dateText.text = _highScores[i].date;
            scoreText.text = _highScores[i].score.ToString();
        }
    }
}
