using UnityEngine;

public class SpecialCollectible : MonoBehaviour
{
    private int totalChildCollectibles;
    private int collectedChildCount = 0;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    void Start()
    {
        // Get references to the main collectible's components
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

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
        // Disable the main collectible's visual and interaction
        spriteRenderer.enabled = false;
        collider2D.enabled = false;

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
            AllChildrenCollecte();
        }
    }

    private void AllChildrenCollecte()
    {
        Debug.Log("All child collectibles collected! Execute special action.");
    }
}
