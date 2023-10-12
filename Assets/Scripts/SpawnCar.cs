using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject[] spawnItems;

    public float frequency;

    public float deleteRange = 100f; 

    public bool isFrequencyRandom;

    float lastSpawnedTime;

    public float Rotation;

    private Transform playerCamera;

    void Start() 
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>().transform;  // Assumes the player's camera is the main camera
        isFrequencyRandom = true;
    }

    void Update()
    {
        if (Time.time > lastSpawnedTime + frequency)
        {
            Spawn();
            lastSpawnedTime = Time.time;
        }

        // Check distance to player camera
        float distanceToCamera = Vector3.Distance(transform.position, playerCamera.position);
        if (distanceToCamera < deleteRange)
        {
            Destroy(gameObject);  // Destroy spawner if within range
            return;  // Exit early to prevent any further actions this frame
        }
    }

    public void Spawn()
    {
        int randomIndex = Random.Range(0, spawnItems.Length);
        GameObject newSpawnedObject = Instantiate(spawnItems[randomIndex], transform.position, Quaternion.Euler(0, Rotation, 0));
        if (isFrequencyRandom) 
        {
            frequency = Random.Range(8, 20); // spawn cars every 10-20 seconds
        }
    }
}
