using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public string beginning = "Footy Starts In: ";
    public float seconds = 60;

    public Text text; // Assign your text component directly in the Inspector

    [Header("Flash Settings")]
    public PostProcessVolume postProcessVolume;
    public PostProcessProfile normalProfile;
    public PostProcessProfile flashProfile;
    
    private bool isFlashing = false;
    private float flashDuration = 0.4f; // Adjust the duration of the flash (in seconds)

    [Header("Screens")]
    public GameObject timerLoseScreen;

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

            // Check if the countdown is below 10 seconds to activate the flash effect
            if (seconds < 10)
            {
                if (!isFlashing)
                {
                    StartFlashEffect();
                }
            }
        }
        timerLoseScreen.SetActive(true);
    }

    private void StartFlashEffect()
    {
        StartCoroutine(FlashEffectCoroutine());
    }

    private IEnumerator FlashEffectCoroutine()
    {
        isFlashing = true;

        // Activate the Post-Processing Profile with the flash effect
        postProcessVolume.profile = flashProfile;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Switch back to the normal Post-Processing Profile
        postProcessVolume.profile = normalProfile;

        isFlashing = false;
    }
}
