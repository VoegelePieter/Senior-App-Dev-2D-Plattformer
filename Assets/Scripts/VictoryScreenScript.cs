using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoresText;

    public string mainMenuScene;

    public Image starHolder;

    public Sprite[] starImages;

    // Start is called before the first frame update
    void Start()
    {
        int currentScore = LevelManager.instance.totalScore;

        scoreText.text = $"Your Score: {currentScore}";

        switch (LevelManager.instance.GetStarRating()) {
            case 0:
                break;
            case 1:
                starHolder.gameObject.SetActive(true);
                starHolder.sprite = starImages[0];
                break;
            case 2:
                starHolder.gameObject.SetActive(true);
                starHolder.sprite = starImages[1];
                break;
            case 3:
                starHolder.gameObject.SetActive(true);
                starHolder.sprite = starImages[2];
                break;
        }

        HighscoreManager.instance.AddNewHighscore(currentScore);
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

    public void ChanceSceneMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
