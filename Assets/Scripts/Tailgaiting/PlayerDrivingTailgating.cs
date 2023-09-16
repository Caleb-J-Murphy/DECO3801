using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivingTailgating : MonoBehaviour
{
    
    public float maxSpeed = 10f; // Maximum speed of the car
    public float maxAcceleration = 2f; // Rate of acceleration
    public float maxDeceleration = 4f; // Rate of deceleration
    public float brakePower = 10f; // Braking power

    private float currentSpeed = 0f;

    private Rigidbody rigidbody;

    public float initialYRotation;
    
    
    private Transform t;
    private float motorInput;
    private float brakeInput;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Start driving immediately
        //rigidbody.velocity = transform.forward * maxSpeed;
        t = GetComponent<Transform>();
        initialYRotation = t.rotation.eulerAngles.y;

    }



    // Update is called once per frame
    void Update()
    {
        // Update the current speed
        Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        float currentSpeed = localVelocity.z; // Use local z-component for forward/backward speed
        Debug.Log("Current Speed: " + currentSpeed);
        // Get User input for acceleration and braking
        motorInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        // Calculate the acceleration force
        float accelerationForce = 0f;
        if (currentSpeed > maxSpeed)
        {
            accelerationForce = 0;
            //Set the current speed back to maxSpeed
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;

        }
        else if (motorInput > 0)
        {
            accelerationForce = Mathf.Lerp(0.3f, maxAcceleration, motorInput);
        }
        else
        {
            accelerationForce = 0;
        }

        // Calculate the braking force
        float brakingForce = brakeInput * brakePower;

        // Ensure braking doesn't make the car go backward
        if (currentSpeed <= 0f && brakeInput > 0f)
        {
            brakingForce = 0f; // Prevent backward braking
        }

        // Apply the acceleration and braking to the car
        rigidbody.AddRelativeForce(Vector3.forward * accelerationForce, ForceMode.Acceleration);
        rigidbody.AddRelativeForce(Vector3.forward * -brakingForce, ForceMode.Acceleration); // Apply forward braking force
    }




}
