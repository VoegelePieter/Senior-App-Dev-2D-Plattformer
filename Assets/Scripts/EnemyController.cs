using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingDirection; //true = right | false = left

    private Rigidbody2D theRB;
    public SpriteRenderer theSR;

    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    public int killScore;


    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {         

            moveCount -= Time.deltaTime;

            if (movingDirection) //if direction right
            {
                theRB.velocity = new Vector2 (moveSpeed, theRB.velocity.y);
                anim.SetBool("isMoving", true);

                if (transform.position.x > rightPoint.position.x)
                {
                    theSR.flipX = false;
                    movingDirection = false;
                }

            } else // if direction left
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                anim.SetBool("isMoving", true);

                if (transform.position.x < leftPoint.position.x)
                {
                    theSR.flipX = true;
                    movingDirection = true;
                }
            }
            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .5f, waitTime * 1.5f);
            }
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            anim.SetBool("isMoving", false);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .5f, moveTime * 1.5f); ;
            }
        }
    }

    private void OnDisable()
    {
        LevelManager.instance.AddScore(killScore);
    }
}
