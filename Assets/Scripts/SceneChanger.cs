using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int currentLevel;

    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource gameMusic;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene.StartsWith("lvl"))
        {
            string levelNum = currentScene.Substring(3);
            int.TryParse(levelNum, out currentLevel);
        }
        else if (currentScene == "MainMenu")
        {
            currentLevel = 1; // or 1 depending on your system
        }
    }

    public void goToLevel1()
    {
        GameObject.Find("gameMusic").GetComponent<AudioSource>().Play();
        GameObject.Find("mainMenuMusic").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("lvl1");
    }

    public void goToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }

    public void goToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void quit()
    {
        Debug.LogWarning("Quitting the game");
        Application.Quit();
    }

    public void nextLevel()
    {
        currentLevel++;
        if (currentLevel == 6)
        {
            try
            {

                GameObject.Find("gameMusic").GetComponent<AudioSource>().Pause();
            }
            catch
            {

            }
        }
        else if (currentLevel == 7)
        {
            try
            {
                GameObject.Find("gameMusic").GetComponent<AudioSource>().UnPause();
            }
            catch
            {

            }
        }
        else if (currentLevel == 9)
        {
            try
            {
                GameObject.Find("gameMusic").GetComponent<AudioSource>().Pause();
                GameObject.Find("goofyNewsMusic").GetComponent<AudioSource>().Play();
            }
            catch
            {

            }
        }
        else if (currentLevel == 10)
        {
            try
            {

                GameObject.Find("goofyNewsMusic").GetComponent<AudioSource>().Stop();
                GameObject.Find("gameMusic").GetComponent<AudioSource>().UnPause();
            }
            catch
            {

            }
            
        }
        if (currentLevel >= 11)
        {
            try
            {
                GameObject.Find("gameMusic").GetComponent<AudioSource>().Stop();
                GameObject.Find("mainMenuMusic").GetComponent<AudioSource>().Play();

            }
            catch
            {

            }
        }
        SceneManager.LoadScene("lvl" + currentLevel);
    }
}
