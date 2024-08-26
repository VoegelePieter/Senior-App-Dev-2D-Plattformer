using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropletSpawner : MonoBehaviour
{
    public GameObject waterDropletPrefab; // Reference to the water droplet prefab
    public float minSpawnInterval = 0.5f; // Minimum time interval between spawns in seconds
    public float maxSpawnInterval = 2f; // Maximum time interval between spawns in seconds

    private float timeSinceLastSpawn;
    private float currentSpawnInterval;

    void Start()
    {
        // Initialize the current spawn interval to a random value within the range
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Update the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new droplet
        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnWaterDroplet();
            timeSinceLastSpawn = 0f; // Reset the timer

            // Randomize the next spawn interval
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnWaterDroplet()
    {
        // Instantiate the water droplet at the current position of the spawner
        Instantiate(waterDropletPrefab, transform.position, Quaternion.identity);
    }
}
