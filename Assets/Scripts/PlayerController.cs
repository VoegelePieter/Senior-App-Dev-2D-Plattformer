using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource playerAudio;

    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D theRB;


    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    private bool canDoubleJump;

    public Animator anim;
    private SpriteRenderer theSR;


    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public static PlayerController instance;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopInput)
        {

            if (knockBackCounter <= 0)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

#if UNITY_EDITOR
                // This overrides the mobile input, disabling the left and right buttons - don't worry about this,
                // it's just for testing purposes while running in the editor
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);

                if (Input.GetButtonDown("Jump")) Jump();
#endif

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                //change direction facinng
                if (theRB.velocity.x < 0f)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0f)
                {
                    theSR.flipX = false;
                }

                if (theRB.transform.position.y <= -10)
                {
                    LevelManager.instance.SubtractScore(PlayerHealthController.instance.deathScorePenalty);
                    LevelManager.instance.RespawnPlayer();
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
    }

    public void WalkRight()
    {
        if (!stopInput) theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
    }
    public void WalkLeft()
    {
        if (!stopInput) theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
    }

    public void Jump()
    {
        if (!stopInput)
        {
            if (isGrounded)
            {
                PlayerSoundPitched(jumpSound);
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                PlayerSoundPitched(jumpSound, 1.5f);
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    public void PlayerSoundPitched(AudioClip clip, float pitch = 1.0f)
    {
        playerAudio.pitch = pitch;
        playerAudio.PlayOneShot(clip);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        anim.SetTrigger("hurt");
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce(float bounceForce)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
