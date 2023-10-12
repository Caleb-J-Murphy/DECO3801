using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject[] spawnItems;

    public float frequency;

    public bool isFrequencyRandom;

    float lastSpawnedTime;

    public float Rotation;

    void Start() 
    {
        isFrequencyRandom = true;
    }

    void Update()
    {
        if (Time.time > lastSpawnedTime + frequency)
        {
            Spawn();
            lastSpawnedTime = Time.time;
        }
    }

    public void Spawn()
    {
        int randomIndex = Random.Range(0, spawnItems.Length);
        GameObject newSpawnedObject = Instantiate(spawnItems[randomIndex], transform.position, Quaternion.Euler(0, Rotation, 0));
        if (isFrequencyRandom) {
            frequency = Random.Range(5, 10); // spawn cars every 5-10 seconds
        }
    }
}
