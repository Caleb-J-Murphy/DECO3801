using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public GameObject prefabToClone; // Reference to the prefab you want to clone.
    public Vector3 spawnOffset = Vector3.up; // Offset from the GameObject's position.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // You can change the tag or use a different condition.
        {
            // Calculate the spawn position based on the GameObject's position and offset.
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Clone the prefab at the calculated spawn position.
            Instantiate(prefabToClone, spawnPosition, Quaternion.identity);
        }
    }
}
