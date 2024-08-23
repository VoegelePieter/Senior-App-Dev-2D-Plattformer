using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;

    public int totalScore;

    public string levelToLoad;

    public int[] starScoreRequirements = new int[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.PlayerSoundPitched(PlayerController.instance.deathSound);
        PlayerController.instance.anim.SetBool("dead", true);
        PlayerController.instance.theRB.simulated = false;
        PlayerController.instance.stopInput = true;

        yield return new WaitForSeconds(waitToRespawn);

        PlayerController.instance.anim.SetBool("dead", false);
        PlayerController.instance.theRB.simulated = true;
        PlayerController.instance.stopInput = false;

        PlayerController.instance.theRB.velocity = new Vector2(0f, 0f);
        PlayerController.instance.transform.position = CheckpointsController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true;
        UIController.instance.levelCompleteText.SetActive(true);


        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(levelToLoad);
    }

    public void AddScore(int addNum)
    {
        totalScore += addNum;
        UIController.instance.UpdateScoreCount();
    }

    public void SubtractScore(int subNum)
    {
        totalScore -= subNum;
        UIController.instance.UpdateScoreCount();
    }

    public int GetStarRating()
    {
        // Unfortunately, the Switch statement for this would look equally bad
        if (totalScore >= starScoreRequirements[2]) return 3;
        if (totalScore >= starScoreRequirements[1]) return 2;
        if (totalScore >= starScoreRequirements[0]) return 1;
        return 0;
    }
}
