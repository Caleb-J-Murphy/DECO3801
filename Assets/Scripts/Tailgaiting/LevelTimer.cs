using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public string beginning = "Footy Starts In: ";
    public float seconds = 60;

    public Text text; // Assign your text component directly in the Inspector

    private void Start()
    {
        // Call a method to start the countdown
        StartCountdown();
    }

    private void StartCountdown()
    {
        // Ensure text is assigned before using it
        if (text != null)
        {
            // Update the text text to display the initial message
            text.text = beginning + seconds.ToString();

            // Start a coroutine to countdown the seconds
            StartCoroutine(CountdownCoroutine());
        }
        else
        {
            Debug.LogError("text component is not assigned. Please assign it in the Inspector.");
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (seconds > 0)
        {
            // Wait for one second
            yield return new WaitForSeconds(1f);

            // Decrease the seconds by 1
            seconds--;

            // Update the text text to display the new countdown value
            text.text = beginning + seconds.ToString();
        }
    }
}
