using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVAdjuster : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() 
    {
        UpdateCameraFOV();
    }

    // Update is called once per frame
    private void UpdateCameraFOV()
    {
        float baseFOV = 85f;  // Set to your desired base FOV
        float velocity = carRigidbody.velocity.magnitude;
        float fovFactor = 1f;  // Set to control how much the FOV changes with velocity
        mainCamera.fieldOfView = baseFOV + (velocity * fovFactor);
    }   
}
