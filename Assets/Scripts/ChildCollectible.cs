using UnityEngine;

public class ChildCollectible : MonoBehaviour
{
    void Start()
    {
        // Disable the child collectible at the start of the game
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the parent that this collectible has been collected
            SpecialCollectible parentScript = GetComponentInParent<SpecialCollectible>();
            if (parentScript != null)
            {
                parentScript.CollectChild();
            }

            // Destroy this child collectible
            Destroy(gameObject);
        }
    }
}
