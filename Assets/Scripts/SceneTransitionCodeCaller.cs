using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCodeCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToStartScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void goTolvl1()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void rageQuit()
    {
        Application.Quit();
    }
}
