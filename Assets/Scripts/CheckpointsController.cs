using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointsController : MonoBehaviour
{

    public static CheckpointsController instance;

    public Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    { 
        spawnPoint = newSpawnPoint;
    }
}
