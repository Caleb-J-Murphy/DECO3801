using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVisualliser : MonoBehaviour
{
    public Transform objectToCheck; // Assign the object you want to check collisions against
    public Transform ragdollRoot;   // Assign the root of the ragdoll

    private void OnDrawGizmos()
    {
        if (objectToCheck == null || ragdollRoot == null)
            return;

        Collider objectCollider = objectToCheck.GetComponent<Collider>();
        Collider[] ragdollColliders = ragdollRoot.GetComponentsInChildren<Collider>();

        if (objectCollider != null)
        {
            foreach (Collider ragdollCollider in ragdollColliders)
            {
                if (Physics.Raycast(objectToCheck.position, objectToCheck.forward, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider == ragdollCollider)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(objectToCheck.position, hit.point);
                    }
                }
            }
        }
    }
}
