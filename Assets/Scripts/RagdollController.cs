using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public float collisionVelocity = 5;
    public CapsuleCollider ragdollCollider; // Reference to the capsule collider

    private Animator animator;
    private Rigidbody[] rigidbodies;

    private bool ragdollEnabled = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollEnabled(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ragdollEnabled)
        {
            ragdollEnabled = true;
            SetRagdollEnabled(true);

            foreach (Rigidbody rb in rigidbodies)
            {
                // Calculate the direction away from the collision point
                Vector3 awayDirection = rb.position - collision.contacts[0].point;
                // Apply a velocity in the opposite direction
                rb.velocity = awayDirection.normalized * collisionVelocity; // Adjust the velocity as needed
            }

            // Check if the collision was with an object on the "Ground" layer
            Collider collisionCollider = collision.collider;
            if (!IsInGroundLayer(collisionCollider))
            {
                // Disable the capsule collider
                DisableCapsuleCollider();
            }
        }
    }

    private bool IsInGroundLayer(Collider collider)
    {
        int groundLayerMask = LayerMask.GetMask("Ground"); // Make sure the "Ground" layer is correctly named
        return (groundLayerMask & (1 << collider.gameObject.layer)) != 0;
    }

    private void SetRagdollEnabled(bool enabled)
    {
        // Enable or disable all rigidbodies in the ragdoll
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !enabled;
            rb.useGravity = enabled;
        }

        // Enable or disable the animator
        animator.enabled = !enabled;
    }

    private void DisableCapsuleCollider()
    {
        if (ragdollCollider != null)
        {
            ragdollCollider.enabled = false;
        }
    }
}
