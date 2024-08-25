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

    public void Destroy(GameObject enemy, Collider2D otherEntity, int killScore, AudioClip killSound, float bounceForce, float dropChance, GameObject drop, GameObject effect, float killSoundPitch = 1.0f)
    {
        if (otherEntity.tag == "Stompbox")
        {
            enemy.SetActive(false);
            LevelManager.instance.AddScore(killScore);

            PlayerController.instance.Bounce(bounceForce);
            PlayerController.instance.PlayerSoundPitched(killSound, killSoundPitch);

            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <= dropChance)
            {
                Instantiate(drop, otherEntity.transform.position, otherEntity.transform.rotation);
            }

            Instantiate(effect, otherEntity.transform.position, otherEntity.transform.rotation);
        }
    }
}
