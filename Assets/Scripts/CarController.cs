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

    [Header("Steering Wheel")]
    public Transform steeringWheel;

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
    public float speedCap = 10.0f;  // Velocity at which acceleration is capped
    public bool isCrashed = false;
    public float steeringWheelRotationSpeed = 100f;

    private float totalSteeringWheelRotation = 0f;
    private float motorInput;
    private float currentVelocity;
    private float velocityFactor;
    private float steeringInput;
    private float brakeInput;

    private Rigidbody carRigidbody;
    public Renderer meshRenderer;
    public Material mirror;
    public Material mirrorCracked;

    [Header("Screens")]
    public GameObject loseScreen;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = Vector3.zero; // Adjust the center of mass if needed
    }

    private void Update()
    {
        if (!isCrashed) {
            // Input handling
            motorInput = 1f;
            steeringInput = Input.GetAxis("Horizontal");
            brakeInput = Input.GetAxis("Brake");

            currentVelocity = carRigidbody.velocity.magnitude;
            velocityFactor = 100f / (1f + Mathf.Pow(currentVelocity, 2));

            GetCurrentGear();
        }
    }

    private void RotateSteeringWheel()
    {
        float rotation = steeringInput * steeringWheelRotationSpeed * Time.deltaTime;

        if (isCrashed || (currentGear != "D" && currentGear != "R")) {
            return;
        }

        if (rotation != 0) {
            totalSteeringWheelRotation += rotation;
            totalSteeringWheelRotation = Mathf.Clamp(totalSteeringWheelRotation, -120f, 120f);
        } else {
            if (totalSteeringWheelRotation < 0) {
                totalSteeringWheelRotation += 1.5f * steeringWheelRotationSpeed * Time.deltaTime;
                totalSteeringWheelRotation = Mathf.Clamp(totalSteeringWheelRotation, -120f, 0f);
            } else if (totalSteeringWheelRotation > 0) {
                totalSteeringWheelRotation -= 1.5f * steeringWheelRotationSpeed * Time.deltaTime;
                totalSteeringWheelRotation = Mathf.Clamp(totalSteeringWheelRotation, 0f, 120f);
            }
        }

        steeringWheel.localRotation = Quaternion.Euler(0, totalSteeringWheelRotation, 0);
    }

    private void FixedUpdate()
    {
        switch (currentGear)
        {
            case "D":
                // Calculate acceleration based on current speed
                float acceleration = CalculateAcceleration();

                if (motorInput < 0.2 || currentVelocity > speedCap * 0.26) {
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

        RotateSteeringWheel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCrashed = true;
        currentGear = "N";

        // Find the "mirror" material and change it to "mirror_cracked"
        Material[] materials = meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.StartsWith(mirror.name.Split(' ')[0]))
            {
                materials[i] = mirrorCracked;
                break;
            }
        }

        meshRenderer.materials = materials;

        loseScreen.SetActive(true);

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
        currentVelocity = carRigidbody.velocity.magnitude;

        // Calculate acceleration using the inverse log function
        float inverseLogAcceleration = maxMotorTorque * Mathf.Log(currentVelocity + 1);

        // Cap the acceleration when reaching a certain speed
        if (currentVelocity > speedCap * 0.26)
        {
            Debug.Log("We hit a cap, CV = " + (currentVelocity * 0.26) + " --- Acc Cap: " + speedCap);
            inverseLogAcceleration = 0.0f;
        }

        // Calculate a deceleration factor as you approach the speedCap
        float decelerationFactor = 1 - (currentVelocity / speedCap);

        // Adjust the acceleration based on the deceleration factor
        float finalAcceleration = inverseLogAcceleration * decelerationFactor;

        return finalAcceleration;
    }

}
