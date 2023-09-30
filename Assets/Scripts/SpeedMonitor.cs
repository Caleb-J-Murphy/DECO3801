using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMonitor : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Text speedText;

    private void Update()
    {
        // Calculate speed in kilometers per hour
        float speedKPH = carRigidbody.velocity.magnitude * 3.6f;

        // Update the speed text
        speedText.text = "Speed: " + speedKPH.ToString("F1") + " km/h";
    }
}
