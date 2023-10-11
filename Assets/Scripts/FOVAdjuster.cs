using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVAdjuster : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Camera mainCamera;
    public float baseFOV = 73;
    public float maxSpeed = 100;
    public float maxFovIncrease = 0.3f;

    void Update() 
    {
        UpdateCameraFOV();
    }

    // Update is called once per frame
    private void UpdateCameraFOV()
    {
        float velocity = carRigidbody.velocity.magnitude;
        mainCamera.fieldOfView = baseFOV + maxFovIncrease * (velocity / maxSpeed);
    }   
}
