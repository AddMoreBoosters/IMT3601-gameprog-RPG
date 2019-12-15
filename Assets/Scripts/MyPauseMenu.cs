﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MyPauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void PlayButtonNoise()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (audio != null)
        {
            audio.Play("ButtonClicked");
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        ScoreManager.gameFinished = true;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
