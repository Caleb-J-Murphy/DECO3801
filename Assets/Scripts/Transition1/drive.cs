using UnityEngine;
using System.Collections;

public class drive : MonoBehaviour
{
    public float thrust = 12.0f;
    private Rigidbody rb;
    private bool canMove = false; // Control variable for movement

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found!");
            return; // exit if no Rigidbody is attached
        }

        StartCoroutine(StartMovementAfterDelay(30.0f)); // start delay coroutine
    }

    private void FixedUpdate()
    {
        // Only apply force if canMove is true
        if (canMove)
        {
            rb.AddForce(transform.right * thrust);
        }
    }

    private IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // wait for specified seconds
        canMove = true; // enable movement
    }
}
