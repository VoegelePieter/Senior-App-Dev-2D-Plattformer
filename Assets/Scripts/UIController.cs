using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Image health1, health2, health3;

    public Sprite healthFull, healthEmpty;

    public GameObject levelCompleteText;

    public TextMeshProUGUI gemText;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
        UpdateScoreCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay()
    {

        //Change Displayed Health Sprite based on current HP (6 at the moment)
        switch(PlayerHealthController.instance.currentHealth)
        {
            case 0:
                health1.sprite = healthEmpty;
                health2.sprite = healthEmpty;
                health3.sprite = healthEmpty;

                break;

            case 1:
                health1.sprite = healthFull;
                health2.sprite = healthEmpty;
                health3.sprite = healthEmpty;

                break;

            case 2:
                health1.sprite = healthFull;
                health2.sprite = healthFull;
                health3.sprite = healthEmpty;

                break;

            case 3: 
                health1.sprite = healthFull;
                health2.sprite = healthFull;
                health3.sprite = healthFull;

                break;

        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void UpdateScoreCount()
    {
        scoreText.text = LevelManager.instance.totalScore.ToString();
    }

}
