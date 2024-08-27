using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint; // Points between which the boss will move

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed;
    public float fireballInterval = 2f;

    public float jumpInterval = 5f;
    public float jumpHeight = 5f;
    public float jumpSpeed = 2f;
    public AudioClip jumpSound;
    private AudioSource bossAudio;
    public float bossAudioDistance = 10f;

    private bool movingRight = false;
    private bool isJumping = false;
    private bool isMovingUp = false; 
    private bool isFalling = false; 
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;

    private float fireballTimer; // Timer to track fireball shooting
    private float jumpTimer;

    private Vector2 originalPosition; // The original position before jumping

    public static Boss instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        bossAudio = GetComponent<AudioSource>();

        // Detach the points from the boss, so they stay fixed in place
        leftPoint.parent = null;
        rightPoint.parent = null;

        fireballTimer = fireballInterval;
        jumpTimer = jumpInterval;
    }

    private void Update()
    {
        if (isFalling)
        {
            return; 
        }

        
        if (!isJumping)
        {
            HandleMovement();
        }

        fireballTimer -= Time.deltaTime;
        if (fireballTimer <= 0f)
        {
            ShootFireball();
            fireballTimer = fireballInterval;
        }

        // Handle jumping
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0f && !isJumping)
        {
            StartJump();
            jumpTimer = jumpInterval;
        }

        if (isJumping)
        {
            HandleJump();
        }
    }

    private void HandleMovement()
    {
        // Check which direction the boss is moving and set velocity accordingly
        if (movingRight)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

            // If the boss has reached the right point, switch direction
            if (transform.position.x >= rightPoint.position.x)
            {
                movingRight = false;
                theSR.flipX = false; // Flip the sprite to face left
            }
        }
        else
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

            // If the boss has reached the left point, switch direction
            if (transform.position.x <= leftPoint.position.x)
            {
                movingRight = true;
                theSR.flipX = true; 
            }
        }
    }

    private void StartJump()
    {
        BossSoundPitched(jumpSound, 0.2f);
        isJumping = true;
        isMovingUp = true;
        originalPosition = transform.position; 

        theRB.velocity = Vector2.zero;
    }

    private void HandleJump()
    {
        if (isMovingUp)
        {
 
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, originalPosition.y + jumpHeight), jumpSpeed * Time.deltaTime);

            if (transform.position.y >= originalPosition.y + jumpHeight)
            {
                isMovingUp = false;
            }
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, originalPosition.y), jumpSpeed * Time.deltaTime);

            if (transform.position.y <= originalPosition.y)
            {
                isJumping = false;

                // Resume horizontal movement after landing
                theRB.velocity = movingRight ? new Vector2(moveSpeed, 0) : new Vector2(-moveSpeed, 0);
            }
        }
    }
    public void BossSoundPitched(AudioClip clip, float pitch = 1.0f)
    {
        if (theRB.position.x - PlayerController.instance.theRB.position.x < bossAudioDistance)
        {
            bossAudio.mute = false;
            bossAudio.pitch = pitch;
            bossAudio.PlayOneShot(clip);
        }
        else
        {
            bossAudio.mute = true;
        }
    }

    public void StopAllBehavior()
    {
        isFalling = true;
        theRB.velocity = new Vector2(0, -jumpSpeed*2); // Make the boss fall straight down

        // Disable the boss after 2 seconds
        Invoke("DisableBoss", 2f);
    }

    private void DisableBoss()
    {
        gameObject.SetActive(false);
    }

    private void ShootFireball()
    {
        Vector2 fireballSpawnPosition = new Vector2(fireballSpawnPoint.position.x, transform.position.y);
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPosition, Quaternion.identity);

        Rigidbody2D fireballRB = fireball.GetComponent<Rigidbody2D>();

        float fireballDirection = movingRight ? 1f : -1f;
        fireballRB.velocity = new Vector2(fireballSpeed * fireballDirection, 0f);

        fireball.GetComponent<Fireball>().Initialize("Ground");
    }
}
