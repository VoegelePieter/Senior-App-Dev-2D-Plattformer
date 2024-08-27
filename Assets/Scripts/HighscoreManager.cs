using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;

    private List<HighscoreEntry> highscores = new List<HighscoreEntry>();
    private const int maxHighscores = 10;
    private const string highscoresKey = "highscores";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighscores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public void AddNewHighscore(int score)
    {
        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        HighscoreEntry newEntry = new HighscoreEntry(score, currentDate);

        highscores.Add(newEntry);
        highscores.Sort((x, y) => y.score.CompareTo(x.score));
        if (highscores.Count > maxHighscores)
        {
            highscores.RemoveAt(highscores.Count - 1);
        }

        SaveHighscores();
    }

    public List<HighscoreEntry> GetHighscores()
    {
        return highscores;
    }

    private void SaveHighscores()
    {
        string json = JsonUtility.ToJson(new HighscoreList(highscores));
        PlayerPrefs.SetString(highscoresKey, json);
        PlayerPrefs.Save();
    }

    private void LoadHighscores()
    {
        if (PlayerPrefs.HasKey(highscoresKey))
        {
            string json = PlayerPrefs.GetString(highscoresKey);
            HighscoreList loadedData = JsonUtility.FromJson<HighscoreList>(json);
            highscores = loadedData.highscores;
        }
    }

    [System.Serializable]
    private class HighscoreList
    {
        public List<HighscoreEntry> highscores;

        public HighscoreList(List<HighscoreEntry> highscores)
        {
            this.highscores = highscores;
        }
    }
}
