using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreenScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Your Score: " + LevelManager.instance.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
