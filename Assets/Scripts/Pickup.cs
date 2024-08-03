using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool isGem;
    public bool isHeal;

    public int healAmound;

    private bool isCollected;

    public GameObject pickupEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;
                isCollected = true;
                UIController.instance.UpdateGemCount();

                Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            if (isHeal && PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.HealPlayer(healAmound);
                Destroy(gameObject);
            }
        }


    }
}
