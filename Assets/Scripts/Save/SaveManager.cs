using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string HighScoresKey = "HighScores";
    private const int maxCount = 10;

    public static void SaveHighScore(int score)
    {
        List<HighScore> highScores = new List<HighScore>();
        string json;
        
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            highScores = JsonUtility.FromJson<HighScoreContainer>(PlayerPrefs.GetString(HighScoresKey)).highScores;
            if (highScores.Count < maxCount)
            {
                highScores.Add(new HighScore(DateTime.Now.ToString(), score));
            }
            else
            {
                HighScore lowestScore = highScores.OrderBy(e => e.score).FirstOrDefault();
                if (lowestScore == null || score < lowestScore.score)
                {
                    return;
                }
                highScores[highScores.IndexOf(lowestScore)] = new HighScore(DateTime.Now.ToString(), score);
            }
        }
        else
        {
            highScores.Add(new HighScore(DateTime.Now.ToString(), score));
        }
        
        HighScoreContainer container = new HighScoreContainer(highScores);
        json = JsonUtility.ToJson(container);
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }
    
    public static List<HighScore> LoadHighScores()
    {
        
        List<HighScore> result = null;
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            result = JsonUtility.FromJson<HighScoreContainer>(PlayerPrefs.GetString(HighScoresKey)).highScores;
        }

        return result != null ? result : new List<HighScore>();
    }
}
