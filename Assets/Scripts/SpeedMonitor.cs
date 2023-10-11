using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMonitor : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public CarController carController;
    public Text speedText;
    
    private float updateInterval = 0.5f; // Update every half a second
    private float lastUpdateTime = 0;

    private void Update()
    {
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            // Calculate speed in kilometers per hour and round it to the nearest integer
            int speedKPH = Mathf.FloorToInt(carRigidbody.velocity.magnitude * 3.6f);

            // Update the speed text with the integer value
            speedText.text = "Speed: " + speedKPH + " km/h " + "Gear: " + carController.currentGear;

            // Update the last update time
            lastUpdateTime = Time.time;
        }
    }
}
