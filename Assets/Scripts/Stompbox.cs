using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    public GameObject drop;
    [Range(0, 100)]public float dropChance;

    public GameObject deathEffect;

    public float defaultBounceForce;
    public float springBounceForce;

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
        if(other.tag == "Enemy")
        {
            Debug.Log("hit");
            other.transform.parent.gameObject.SetActive(false);

            if(other.name == "Spring Sprite")
            {
                PlayerController.instance.Bounce(springBounceForce);
            } 
            else
            {
                PlayerController.instance.Bounce(defaultBounceForce);
            }

            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <= dropChance)
            {
                Instantiate(drop, other.transform.position, other.transform.rotation);
            }

            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        }
    }
}
