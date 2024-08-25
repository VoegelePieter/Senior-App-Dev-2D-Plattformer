using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever : MonoBehaviour
{
    public Sprite activatedSprite; 
    public Tilemap bossWall;
    public Tilemap bossGround; 
    public GameObject boss; 

    private SpriteRenderer spriteRenderer; 
    private bool isActivated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isActivated)
        {
            RemoveTilemaps();
            if (boss != null)
            {
                // Stop the boss's behavior and make it fall
                boss.GetComponent<Boss>().StopAllBehavior();
            }

            isActivated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        if (spriteRenderer != null && activatedSprite != null)
        {
            spriteRenderer.sprite = activatedSprite;
        }
    }

    private void RemoveTilemaps()
    {
        if (bossWall != null)
        {
            bossWall.ClearAllTiles();
        }

        if (bossGround != null)
        {
            bossGround.ClearAllTiles();
        }
    }
}
