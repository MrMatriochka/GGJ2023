using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private bool isOpen;

    private void Start()
    {
        isOpen = false;
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                ReturnPauseMenu();
                isOpen = true;
                Time.timeScale = 0.1f;
            }
            else
            {
                Resume();
                isOpen = false;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        isOpen = false;
    }
    
    public void GoOptions()
    {
        optionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnPauseMenu()
    {
        optionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
