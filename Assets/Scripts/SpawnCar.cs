using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{

    public GameObject[] spawnItems;

    public float frequency;

    float lastSpawnedTime;

    public float Rotation;


    // Update is called once per frame
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
  
    }
}
