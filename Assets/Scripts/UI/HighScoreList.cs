using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private Transform recordListPanel;
    [SerializeField] private GameObject recordPrefab;
    [SerializeField] private Transform firstRecord;

    private RectTransform _rectTransform;
    
    private List<HighScore> highScores;
    
    private void Start()
    {
        _rectTransform = recordPrefab.GetComponent<RectTransform>();
        LoadHighScores();
        DisplayHighScores();
    }
    
    private void LoadHighScores()
    { 
        highScores = SaveManager.LoadHighScores().OrderByDescending(score => score.score).ToList();
    }
    
    private void DisplayHighScores()
    {
        foreach (Transform child in recordListPanel)
        {
            Destroy(child.gameObject);
        }

        var yPosition = firstRecord.position.y;
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject recordObject = Instantiate(recordPrefab, recordListPanel);
            
            Vector3 currentPosition = recordObject.transform.position;
            currentPosition.y = yPosition;
            recordObject.transform.position = currentPosition;
            yPosition -= _rectTransform.rect.height;

            TextMeshProUGUI numberText = recordObject.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dateText = recordObject.transform.Find("DateText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = recordObject.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            numberText.text = (i + 1) + ".";
            dateText.text = highScores[i].date;
            scoreText.text = highScores[i].score.ToString();
        }
    }
}
