using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Change 'P' to the desired key to trigger the pause.
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0; // Pauses the game by setting time scale to 0.
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resumes the game by setting time scale to 1.
        isPaused = false;
    }
}
