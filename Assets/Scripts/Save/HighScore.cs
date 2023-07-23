using System;

[Serializable]
public class HighScore
{
    public string date;
    public int score;

    public HighScore(string date, int score)
    {
        this.date = date;
        this.score = score;
    }
}