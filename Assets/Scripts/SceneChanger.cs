using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int currentLevel;

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
            currentLevel = 0; // or 1 depending on your system
        }
    }

    public void goToLevel1()
    {
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
        SceneManager.LoadScene("lvl" + currentLevel);
    }
}
