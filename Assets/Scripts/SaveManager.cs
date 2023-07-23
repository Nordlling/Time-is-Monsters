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
                highScores.Add(new HighScore { date = DateTime.Now.ToString(), score = score });
            }
            else
            {
                HighScore highScore = highScores.OrderBy(e => e.score).FirstOrDefault();
                if (highScore == null || score < highScore.score)
                {
                    return;
                }
                highScores[highScores.IndexOf(highScore)] = new HighScore { date = DateTime.Now.ToString(), score = score };

            }
        }
        else
        {
            highScores.Add(new HighScore { date = DateTime.Now.ToString(), score = score });
        }
        
        HighScoreContainer container = new HighScoreContainer { highScores = highScores };
        json = JsonUtility.ToJson(container);
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }
    
    public static List<HighScore> LoadHighScores()
    {
        List<HighScore> result = new List<HighScore>();
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            result = JsonUtility.FromJson<HighScoreContainer>(PlayerPrefs.GetString(HighScoresKey)).highScores;
            // foreach (var highScore in result)
            // {
            //     Debug.Log(highScore.date + " - " + highScore.score);
            // }
        }

        return result;
    }
    
    
    
}

[Serializable]
public class HighScoreContainer
{
    public List<HighScore> highScores = new List<HighScore>();
}

[Serializable]
public class HighScore
{
    public string date;
    public int score;
}
