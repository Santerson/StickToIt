using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Scence Change.
    public void goToLevel1()
    {
        currentLevel = 1;
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
        EditorApplication.ExitPlaymode();
    }
    public void nextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("lvl" + currentLevel);
    }
}
