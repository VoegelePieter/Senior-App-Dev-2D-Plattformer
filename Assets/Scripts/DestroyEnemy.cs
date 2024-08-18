using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public static DestroyEnemy instance;

    private void Awake()
    {
        instance = this;
    }

    public void Destroy(GameObject enemy, Collider2D otherEntity, int killScore, float bounceForce, float dropChance, GameObject drop, GameObject effect)
    {
        if (otherEntity.tag == "Stompbox")
        {
            enemy.SetActive(false);
            LevelManager.instance.AddScore(killScore);

            PlayerController.instance.Bounce(bounceForce);

            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <= dropChance)
            {
                Instantiate(drop, otherEntity.transform.position, otherEntity.transform.rotation);
            }

            Instantiate(effect, otherEntity.transform.position, otherEntity.transform.rotation);
        }
    }
}
