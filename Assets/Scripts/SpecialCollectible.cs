using UnityEngine;

public class SpecialCollectible : MonoBehaviour
{
    private int totalChildCollectibles;
    private int collectedChildCount = 0;
    private SpriteRenderer spriteRenderer;
    private Collider2D collectibleCollider2D;

    public AudioClip collectSound;

    public int collectedAllChildrenScoreReward;

    void Start()
    {
        // Get references to the main collectible's components
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectibleCollider2D = GetComponent<Collider2D>();

        // Count the number of child collectibles
        totalChildCollectibles = transform.childCount;

        // Disable all child collectibles at the start
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectMain();
        }
    }

    private void CollectMain()
    {
        PlayerController.instance.PlayerSoundPitched(collectSound, 1.7f);

        // Disable the main collectible's visual and interaction
        spriteRenderer.enabled = false;
        collectibleCollider2D.enabled = false;

        // Enable all child collectibles
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void CollectChild()
    {
        collectedChildCount++;

        if (collectedChildCount >= totalChildCollectibles)
        {
            AllChildrenCollected();
        }
    }

    private void AllChildrenCollected()
    {
        LevelManager.instance.AddScore(collectedAllChildrenScoreReward);
    }
}
