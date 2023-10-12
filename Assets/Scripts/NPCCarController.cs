using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCar : MonoBehaviour
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



    [Header("Car Parameters")]
    public float maxMotorTorque = 300f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 2000f;

    private float motorInput;
    private float steeringInput;
    private float brakeInput;

    private Rigidbody carRigidbody;
    public float destructYValue = -10f;  // Set the Y-value threshold for destruction


    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Input handling
        motorInput = 1f;

        if (transform.position.y < destructYValue)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        // Apply motor torque to the wheels
        frontLeftWheel.motorTorque = maxMotorTorque * motorInput;
        frontRightWheel.motorTorque = maxMotorTorque * motorInput;

        // Apply steering angle to the front wheels
        float steerAngle = maxSteeringAngle * steeringInput;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;

        // Apply braking force to all wheels
        if (brakeInput > 0f)
        {
            frontLeftWheel.brakeTorque = brakeForce;
            frontRightWheel.brakeTorque = brakeForce;
            rearLeftWheel.brakeTorque = brakeForce;
            rearRightWheel.brakeTorque = brakeForce;
        }
        else
        {
            frontLeftWheel.brakeTorque = 0f;
            frontRightWheel.brakeTorque = 0f;
            rearLeftWheel.brakeTorque = 0f;
            rearRightWheel.brakeTorque = 0f;
        }

        // Apply steering rotation to the wheel models
        float steerRotation = maxSteeringAngle * steeringInput;
        frontLeftWheelTransform.localRotation = Quaternion.Euler(-90f, 0, steerRotation);
        frontRightWheelTransform.localRotation = Quaternion.Euler(-90f, 0, steerRotation);

        

        //Update wheel transforms
        //UpdateWheelTransform(frontLeftWheel, frontLeftWheelTransform);
        //UpdateWheelTransform(frontRightWheel, frontRightWheelTransform);
        //UpdateWheelTransform(rearLeftWheel, rearLeftWheelTransform);
        //UpdateWheelTransform(rearRightWheel, rearRightWheelTransform);

    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}

