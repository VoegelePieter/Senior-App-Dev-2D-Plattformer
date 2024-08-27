using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string startScene;

    public TextMeshProUGUI highscoresText;

    // Start is called before the first frame update
    void Start()
    {
        DisplayHighscores();
    }

    private void DisplayHighscores()
    {
        highscoresText.text = "Top 10 Highscores:\n";
        var highscores = HighscoreManager.instance.GetHighscores();

        for (int i = 0; i < highscores.Count; i++)
        {
            highscoresText.text += $"{i + 1}. {highscores[i].score} - {highscores[i].date}\n";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
}
