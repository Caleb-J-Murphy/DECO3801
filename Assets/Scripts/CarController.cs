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
    //public RectTransform steetingWheelTransform;

    [Header("Car Parameters")]
    public float maxMotorTorque = 300f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 2000f;

    private float motorInput;
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
        motorInput = Input.GetAxis("Vertical") + 1;
        steeringInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetAxis("Brake");
    }

    private void FixedUpdate()
    {
        // Apply motor torque to the wheels
        frontLeftWheel.motorTorque = maxMotorTorque * motorInput;
        frontRightWheel.motorTorque = maxMotorTorque * motorInput;

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

        //steetingWheelTransform.localRotation = Quaternion.Euler(0, 0, steerRotation * -2);
           
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

