using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCarController : MonoBehaviour
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

    public float initialVelocity = 20f;
    public float detectionRange = 30f;

    private Rigidbody carRigidbody;
    private bool ObstacleDetected = false;
    private Vector3 rayCollision = Vector3.zero;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        carRigidbody.velocity = transform.forward * initialVelocity; // give NPC cars an initial speed
    }

    private void Update()
    {
        var ray = new Ray(this.transform.TransformPoint(new Vector3(0f, 1f, 3f)), this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            ObstacleDetected = true;
            rayCollision = hit.point;
        } else {
            ObstacleDetected = false;
        }


        // Input handling
        if (ObstacleDetected || Mathf.Abs(carRigidbody.velocity.z) > 60f) {
            motorInput = 0f;
            brakeInput = 1f;
        } else {
            motorInput = 1f;
            brakeInput = 0f;
        }
        
        // When car falls off map, delete it
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

    void onDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rayCollision, 0.2f);
    }
}

