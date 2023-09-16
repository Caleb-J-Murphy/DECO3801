using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public GameObject prefabToClone; // Reference to the prefab you want to clone.
    public GameObject previousClone;
    public Vector3 spawnOffset = Vector3.up; // Offset from the GameObject's position.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // You can change the tag or use a different condition.
        {
            // Calculate the spawn position based on the GameObject's position and offset.
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Clone the prefab at the calculated spawn position.
            GameObject clone = Instantiate(prefabToClone, spawnPosition, Quaternion.identity);

            // Remove the box collider so that it cannot trigger another one
            Destroy(GetComponent<BoxCollider>());


            Transform cloneTransform = clone.transform;

            Transform cloneTrigger = cloneTransform.GetChild(3);

            if (cloneTrigger != null)
            {
                TriggerSpawn childTriggerSpawn = cloneTrigger.GetComponent<TriggerSpawn>();
                if (childTriggerSpawn != null)
                {
                    childTriggerSpawn.previousClone = gameObject;
                }
            }

            // Check if the previousClone is not null before calling Die().
            if (previousClone != null)
            {
                TriggerSpawn previousTriggerSpawn = previousClone.GetComponent<TriggerSpawn>();
                if (previousTriggerSpawn != null)
                {
                    previousTriggerSpawn.Die();
                }
            }
        }
    }

    public void Die()
    {
        // Destroy the parent GameObject.
        Destroy(transform.parent.gameObject);
    }
}
