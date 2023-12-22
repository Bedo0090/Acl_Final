using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameObject Panel;

    private bool Paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;
        Paused = true;
        StarterAssetsInputs.SetCursorState(false);

        // Show the pause menu
        Panel.SetActive(true);
    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1f;
        Paused = false;
        StarterAssetsInputs.SetCursorState(true);

        // Hide the pause menu
        Panel.SetActive(false);
    }
    public void quit()
    {
        SceneManager.LoadScene("Main menu");
    }
}