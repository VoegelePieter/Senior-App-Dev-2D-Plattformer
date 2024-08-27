using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public GameObject deathEffect;
    public int deathScorePenalty;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public float damageInterval;
    public int damageAmount;
    private float damageTimer;
    public AudioClip damageSound;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();

        damageTimer = damageInterval; // Initialize the timer with the interval
    }

    // Update is called once per frame
    void Update()
    {

        //decaying invincibility
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                //changing player opacity back to normal
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }

        // Automatic damage over time
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0)
            {
                DealDamage(damageAmount, false);
                damageTimer = damageInterval; // Reset the timer after dealing damage
            }
        }
    }

    public void DealDamage(int damageAmount = 1, bool applyKnockbackAndInvincible = true)
    {
        //damage player if not dead already
        if (invincibleCounter <= 0)
        {
            currentHealth -= damageAmount;
            switch (currentHealth)
            {
                case 0:
                    break;
                case 1:
                    PlayerController.instance.PlayerSoundPitched(damageSound, 2f);
                    break;
                default:
                    PlayerController.instance.PlayerSoundPitched(damageSound);
                    break;
            }
        }

        //if player hp 0 = delete player
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            LevelManager.instance.SubtractScore(deathScorePenalty);
            Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
            LevelManager.instance.RespawnPlayer();
        }

        //start invincibility timer, make player opacity .5 and knock him back if requested
        if (applyKnockbackAndInvincible && invincibleCounter <= 0)
        {
            invincibleCounter = invincibleLength;
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
            PlayerController.instance.KnockBack();
        }

        UIController.instance.UpdateHealthDisplay();
    }

    public void HealPlayer(int amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
