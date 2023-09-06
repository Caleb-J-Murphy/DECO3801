using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidbody;
    private bool isFalling = true;

    public float fallSpeed = 5.0f;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFalling)
        {
            // Calculate the position below the capsule collider
            Vector3 bottom = transform.position - new Vector3(0, capsuleCollider.height / 2 - capsuleCollider.radius, 0);

            // Check if the capsule is touching the ground
            bool isTouchingGround = CheckIfTouchingGround(bottom);

            if (isTouchingGround)
            {
                // Stop falling when touching the ground
                isFalling = false;
            }
            else
            {
                // Move the object towards the ground
                Vector3 newPosition = transform.position - Vector3.up * fallSpeed * Time.deltaTime;
                rigidbody.MovePosition(newPosition);
            }
        }
    }

    private bool CheckIfTouchingGround(Vector3 bottom)
    {
        float radius = capsuleCollider.radius * 0.9f; // Slightly reduce the radius to avoid false positives
        float checkDistance = 0.05f; // Distance to check below the capsule bottom

        return Physics.CheckCapsule(bottom, bottom - Vector3.up * checkDistance, radius, LayerMask.GetMask("Ground"));
    }
}
