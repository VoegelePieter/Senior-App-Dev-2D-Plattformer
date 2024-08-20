using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreEntry
{
    public int score;
    public string date;

    public HighscoreEntry(int score, string date)
    {
        this.score = score;
        this.date = date;
    }
}
