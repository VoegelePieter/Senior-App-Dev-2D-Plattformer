using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    public GameObject drop;
    [Range(0, 100)] public float dropChance;

    public GameObject deathEffect;

    public float bounceForce;

    public Transform[] points;
    public float moveSpeed;

    private int currentPoint;

    public SpriteRenderer theSR;
    public Rigidbody2D theRB;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;

    public float waitAfterAttack;
    private float attackCounter;

    public int killScore;
    public AudioClip killSound;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else 
        { 
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {

                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }

                }

                if(transform.position.x < points[currentPoint].position.x)
                {
                    theSR.flipX = true;
                } else
                {
                    theSR.flipX = false;
                }
            } else
            {
                if(attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyEnemy.instance.Destroy(theRB.gameObject, other, killScore, killSound, bounceForce, dropChance, drop, deathEffect);
    }
}
