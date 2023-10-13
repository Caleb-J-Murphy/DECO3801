using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Start() {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && isPaused)
        {
            ResumeGame();
            pauseMenu.SetActive(false);
        } 
        if (Input.GetKeyDown(KeyCode.U) && !isPaused) {
            Pause();
            pauseMenu.SetActive(true);
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
