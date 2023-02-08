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
    public float timer = 3f;
    private float reset;
    private bool collide;

    private void Start()
    {
        collide = false;
        reset = timer;
    }

    public void Update()
    {
        if (collide)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0)
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

            timer = reset;
            collide = false;
        }
        
        Debug.Log(Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collide = true;
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
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
