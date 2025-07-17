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
        // Try to extract the number from the current scene name like "lvl2"
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene.StartsWith("lvl"))
        {
            string levelNum = currentScene.Substring(3);
            int.TryParse(levelNum, out currentLevel);
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
        Application.Quit();
        Debug.LogWarning("Quitting the game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    public void nextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("lvl" + currentLevel);
    }
}
