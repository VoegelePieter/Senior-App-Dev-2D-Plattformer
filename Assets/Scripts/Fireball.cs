using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float maxTravelDistance = 10f;

    private string groundLayerName;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        startPosition = rb.position;

        if (rb.velocity.x < 0)
        {
            sr.flipX = true; // Facing left
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false; // Facing right
        }
    }

    private void Update()
    {

        float distanceTraveled = Vector2.Distance(startPosition, rb.position);


        if (distanceTraveled >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(string groundLayer)
    {
        groundLayerName = groundLayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            Destroy(gameObject);
        }
    }
}
