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
    private bool ObstacleDetected = false;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        carRigidbody.velocity = transform.forward * 20f; // give NPC cars an initial speed
    }

    private void Update()
    {
        // Input handling
        if (ObstacleDetected || Mathf.Abs(carRigidbody.velocity.z) > 60f) {
            motorInput = 0f;
            brakeInput = 1f;
        } else {
            motorInput = 1f;
            brakeInput = 0f;
        }
        

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Apply motor torque to the wheels
        frontLeftWheel.motorTorque = maxMotorTorque * motorInput;
        frontRightWheel.motorTorque = maxMotorTorque * motorInput;

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
    }

    void OnTriggerEnter(Collider other)
    {
        ObstacleDetected = true;
    }

    void OnTriggerExit(Collider other)
    {
        ObstacleDetected = false;
    }
}

