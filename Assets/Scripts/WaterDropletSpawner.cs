using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropletSpawner : MonoBehaviour
{
    public GameObject waterDropletPrefab; // Reference to the water droplet prefab
    public float spawnInterval = 1f; // Time interval between spawns in seconds

    private float timeSinceLastSpawn;

    void Update()
    {
        // Update the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new droplet
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnWaterDroplet();
            timeSinceLastSpawn = 0f; // Reset the timer
        }
    }

    void SpawnWaterDroplet()
    {
        // Instantiate the water droplet at the current position of the spawner
        Instantiate(waterDropletPrefab, transform.position, Quaternion.identity);
    }
}
