using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailgatingCar : MonoBehaviour
{
    public float minDriveTime = 5f;
    public float maxDriveTime = 10f;
    public float maxSpeed = 10f;
    public float brakeForce = 10f;

    private Rigidbody rigidbody;
    private bool isDriving = true;


    //Get the initial y rotation and keep it there
    public float initialYRotation;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Start driving immediately
        rigidbody.velocity = transform.forward * maxSpeed;
        StartCoroutine(DriveRandomTime());
        t = GetComponent<Transform>();
        initialYRotation = t.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving && rigidbody.velocity.magnitude < maxSpeed)
        {
            // Apply driving force in the car's forward direction
            rigidbody.AddForce(transform.forward * maxSpeed, ForceMode.Acceleration);
            t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, initialYRotation, t.rotation.eulerAngles.z);

        }
    }

    IEnumerator DriveRandomTime()
    {
        float driveTime = Random.Range(minDriveTime, maxDriveTime);
        yield return new WaitForSeconds(driveTime);

        // When the random drive time is over, apply braking force to stop the car
        ApplyBrakes();
    }

    void ApplyBrakes()
    {
        // Apply braking force in the opposite direction of the car's current velocity
        Vector3 brakingForce = -rigidbody.velocity.normalized * brakeForce;
        rigidbody.AddForce(brakingForce, ForceMode.Acceleration);
        isDriving = false;
    }
}
