using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public string beginning = "Footy Starts In: ";
    public float seconds = 60;

    public TextMeshProUGUI textMeshPro; // Assign your TextMeshPro component directly in the Inspector

    private void Start()
    {
        // Call a method to start the countdown
        StartCountdown();
    }

    private void StartCountdown()
    {
        // Update the TextMeshPro text to display the initial message
        textMeshPro.text = beginning + seconds.ToString();

        // Start a coroutine to countdown the seconds
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        while (seconds > 0)
        {
            // Wait for one second
            yield return new WaitForSeconds(1f);

            // Decrease the seconds by 1
            seconds--;

            // Update the TextMeshPro text to display the new countdown value
            textMeshPro.text = beginning + seconds.ToString();
        }

        // When the countdown is complete, you can perform any desired actions
        // For example, you can display a message or start the game.
    }
}
