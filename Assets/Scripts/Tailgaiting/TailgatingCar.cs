using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailgatingCar : MonoBehaviour
{
    public float minDriveTime = 5f;
    public float maxDriveTime = 10f;
    public float maxSpeed = 10f;

    public float maxAcceleration = 2f; // Rate of acceleration
    public float brakeForce = 10f; // Braking power

    private Rigidbody rb;
    public bool isDriving = true;


    //Get the initial y rotation and keep it there
    public float initialYRotation;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Start driving immediately
        //rigidbody.velocity = transform.forward * maxSpeed;
        StartCoroutine(DriveRandomTime());
        t = GetComponent<Transform>();
        initialYRotation = t.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
            float currentSpeed = localVelocity.z; // Use local z-component for forward/backward speed
            float accelerationForce = 0f;
            if (currentSpeed > maxSpeed)
            {
                accelerationForce = 0;
                //Set the current speed back to maxSpeed
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;

            }
            else
            {
                accelerationForce = Mathf.Lerp(0.3f, maxAcceleration, 1);
            }

            // Apply the acceleration
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * accelerationForce, ForceMode.Acceleration);

            // Ensure the car's rotation stays the same
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
        Vector3 brakingForce = -GetComponent<Rigidbody>().velocity.normalized * brakeForce;
        GetComponent<Rigidbody>().AddForce(brakingForce, ForceMode.Acceleration);
        isDriving = false;
    }
}
