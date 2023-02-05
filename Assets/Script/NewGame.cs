using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewGame : MonoBehaviour
{
    public string ID;
    public GameObject optionsPanel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ID == "0")
            {
                ChangeScene();
            }
            else if (ID == "1")
            {
                OptionsOpen();
            }
            else
            {
                Application.Quit();
                Debug.Log("Quitter");
            }
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OptionsOpen()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
