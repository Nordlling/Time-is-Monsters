using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreContainer
{
    public List<HighScore> highScores;

    public HighScoreContainer(List<HighScore> highScores)
    {
        this.highScores = highScores;
    }
}