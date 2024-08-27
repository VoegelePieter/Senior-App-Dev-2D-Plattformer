using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public SpriteRenderer theSR;

    public Sprite cpOn, cpOff;

    public bool scoreAwarded;

    private AudioSource checkpointSound;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && theSR.sprite != cpOn)
        {
            if(!scoreAwarded)
            {
                LevelManager.instance.AddScore(200);
                scoreAwarded = true;
            }
            CheckpointsController.instance.DeactivateCheckpoints();
            theSR.sprite = cpOn;
            checkpointSound.Play();
            CheckpointsController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
}
