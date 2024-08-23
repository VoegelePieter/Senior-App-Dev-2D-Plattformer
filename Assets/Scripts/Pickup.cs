using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool isGem;
    public bool isHeal;

    public int gemScore;
    public int healAmount;

    public GameObject pickupEffect;

    public AudioClip pickupSound;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            bool collected = false;

            if (isGem) collected = CollectGem();
            else if (isHeal && PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth) collected = CollectMilk();

            if (collected) PlayerController.instance.PlayerSoundPitched(pickupSound);
        }
    }

    private bool CollectGem()
    {
        LevelManager.instance.AddScore(gemScore);
        LevelManager.instance.gemsCollected++;
        UIController.instance.UpdateGemCount();

        Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        return true;
    }

    private bool CollectMilk()
    {
        PlayerHealthController.instance.HealPlayer(healAmount);
        Destroy(gameObject);

        return true;
    }
}
