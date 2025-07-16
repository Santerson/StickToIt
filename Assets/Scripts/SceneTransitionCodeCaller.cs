using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionCodeCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToStartScene()
    {
        FindObjectOfType<SceneChanger>().goToMainMenu();
    }
}
