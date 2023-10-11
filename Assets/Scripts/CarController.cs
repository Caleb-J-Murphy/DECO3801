using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Front Wheel Transforms")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    //[Header("Steering Wheel Rec Transform")]
    //public RectTransform steeringWheelTransform;

    [Header("Car Parameters")]
    public float maxMotorTorque = 3000f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 2000f;
    public string currentGear = "P";
    public float accelerationSlowdownVelocity = 5.0f;  // Velocity at which acceleration starts slowing down
    public float accelerationCap = 10.0f;  // Velocity at which acceleration is capped

    private float motorInput;
    private float currentVelocity;
    private float velocityFactor;
    private float steeringInput;
    private float brakeInput;

    private Rigidbody carRigidbody;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = Vector3.zero; // Adjust the center of mass if needed
    }

    private void Update()
    {
        // Input handling
        motorInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetAxis("Brake");
        
        currentVelocity = carRigidbody.velocity.magnitude;
        GetCurrentGear();
    }

    private void FixedUpdate()
    {
        switch (currentGear)
        {
            case "D":
                // Calculate acceleration based on current speed
                float acceleration = CalculateAcceleration();

                if (motorInput < 0.2) {
                    acceleration = 0;
                    brakeInput = 0.5f;
                }
                
                frontLeftWheel.motorTorque = maxMotorTorque * motorInput * acceleration;
                frontRightWheel.motorTorque = maxMotorTorque * motorInput * acceleration;
                break;

            case "R":
                frontLeftWheel.motorTorque = maxMotorTorque * -1f * motorInput * velocityFactor;
                frontRightWheel.motorTorque = maxMotorTorque * -1f * motorInput * velocityFactor;
                break;

            case "P":
                brakeInput = 1; // full force brake
                break;

            case "N":
                frontLeftWheel.motorTorque = maxMotorTorque * 0;
                frontRightWheel.motorTorque = maxMotorTorque * 0;
                break;
        }

        frontLeftWheel.brakeTorque = brakeForce * brakeInput;
        frontRightWheel.brakeTorque = brakeForce * brakeInput;
        rearLeftWheel.brakeTorque = brakeForce * brakeInput;
        rearRightWheel.brakeTorque = brakeForce * brakeInput;

        // Apply steering angle to the front wheels
        float steerAngle = maxSteeringAngle * steeringInput;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;

        // Apply steering rotation to the wheel models
        float steerRotation = maxSteeringAngle * steeringInput;
        frontLeftWheelTransform.localRotation = Quaternion.Euler(-90f, 0, steerRotation);
        frontRightWheelTransform.localRotation = Quaternion.Euler(-90f, 0, steerRotation);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    private void GetCurrentGear()
    {
        if (Input.GetAxis("Drive") > 0f)
        {
            currentGear = "D";
        }
        else if (Input.GetAxis("Reverse") > 0f)
        {
            currentGear = "R";
        }
        else if (Input.GetAxis("Park") > 0f)
        {
            currentGear = "P";
        }
        else if (Input.GetAxis("Neutral") > 0f)
        {
            currentGear = "N";
        }
    }

    private float CalculateAcceleration()
    {
        // Define the parameters for the inverse logarithmic function
        float accelerationCap = 20.0f; // Maximum speed at which acceleration is capped

        // Calculate acceleration using the inverse log function
        float inverseLogAcceleration = maxMotorTorque / Mathf.Log(currentVelocity + 1);

        // Cap the acceleration when reaching a certain speed
        if (currentVelocity > accelerationCap)
        {
            inverseLogAcceleration = 0.0f;
        }
        
        // Calculate a deceleration factor as you approach the accelerationCap
        float decelerationFactor = 1 - (currentVelocity / accelerationCap);

        // Adjust the acceleration based on the deceleration factor
        float finalAcceleration = inverseLogAcceleration * decelerationFactor;

        return finalAcceleration;
    }

}
