using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("This is the Scence you can change to.")]
    [SerializeField] string Scence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Scence Change.
    public void LevelChange()
    {
        SceneManager.LoadScene(Scence);
    }
}
