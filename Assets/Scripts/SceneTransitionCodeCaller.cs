using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCodeCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToStartScene()
    {
        FindObjectOfType<SceneChanger>().goToMainMenu();
    }

    public void goTolvl1()
    {
        FindObjectOfType<SceneChanger>().goToLevel1();
    }

    public void rageQuit()
    {
        Application.Quit();
    }

    public void goToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
