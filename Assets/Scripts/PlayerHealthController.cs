using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public GameObject deathEffect;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
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
                theSR.color = theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        } 
    }

    public void DealDamage() 
    {

        //damage player if not dead laready
        if (invincibleCounter <= 0)
        {
            currentHealth--;
        }


        //if player hp 0 = delete player
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
            LevelManager.instance.RespawnPlayer();
        }

        //start invincibility timer and make player opacity .5 and knock him back if not currently on knockbacklength timer
        if(invincibleCounter <= 0)
        {
            invincibleCounter = invincibleLength;
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
            PlayerController.instance.KnockBack();
        }
            


        UIController.instance.UpdateHealthDisplay();
    }

    public void HealPlayer(int amound)
    {
        if (currentHealth + amound >= maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += amound;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
